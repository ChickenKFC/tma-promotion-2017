using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace LogicalOperationSearch.Db
{
    public abstract class DatabaseManager : IDisposable
    {
        /// <summary>
        /// Static thread.
        /// </summary>
        [ThreadStatic]
        private static DatabaseManager threadInstance;
        public static DatabaseManager ThreadInstance
        {
            get { return threadInstance; }
            set { threadInstance = value; }
        }

        /// <summary>
        /// Database connection
        /// </summary>
        protected DbConnection connection;

        /// <summary>
        /// Database transaction
        /// </summary>
        protected DbTransaction transaction;

        /// <summary>
        /// Release resource flag.
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Connection count
        /// </summary>
        private static int __currentConnectionCount = 0;

        /// <summary>
        /// Token string of previous sql query
        /// </summary>
        private string _defaultProcessToken;

        /// <summary>
        /// Return default process token.
        /// </summary>
        public string DefaultProcessToken
        {
            get
            {
                return _defaultProcessToken;
            }
        }

        #region Database Connection Info
        /// <summary>
        /// Database option
        /// </summary>
        private const string DATABASE_OPTION = @"CommandTimeout=0;MinPoolSize=0;MaxPoolSize=100;ConnectionLifeTime=60;TimeOut=5";

        /// <summary>
        /// Server address
        /// </summary>
        private const string SERVER_ADDRESS = @"127.0.0.1";

        /// <summary>
        /// Server port
        /// </summary>
        private const string SERVER_PORT = @"5432";

        /// <summary>
        /// User id
        /// </summary>
        private const string USER_ID = @"search_operator";

        /// <summary>
        /// Password
        /// </summary>
        private const string PASSWORD = @"NhatAnh#0110";

        /// <summary>
        /// Database name
        /// </summary>
        private const string DATABASE_NAME = @"search_operator";

        /// <summary>
        /// Max retry connection count
        /// </summary>
        private const int MAX_RETRY_COUNT = 2;

        /// <summary>
        /// Retry waiting time.
        /// </summary>
        private const int RETRY_WAITING_TIME = 10000;

        /// <summary>
        /// SQLとトークンを結合する際のフォーマットです。
        /// </summary>
        private const string CONCAT_TOKEN_AND_SQL_FORMAT = "--{0}\r\n{1}";

        /// <summary>
        /// CamelCase->アンダースコア区切りに変換するための正規表現です。
        /// プロパティ名からDBのカラム名を求める際に使用します。
        /// </summary>
        private readonly Regex CAMEL_CASE_REGEX = new Regex("([a-z])([A-Z])");

        /// <summary>
        /// CamelCase->アンダースコア区切りに変換するための置換文字です。
        /// プロパティ名からDBのカラム名を求める際に使用します。
        /// </summary>
        private const string UNDER_SCORE_REPLACE = "$1_$2";

        #endregion Database Connection Info

        /// <summary>
        /// Default contructor
        /// </summary>
        /// <returns>DatabaseManager instance</returns>
        public static DatabaseManager CreateInstance()
        {
            return CreateInstance(null);
        }

        /// <summary>
        /// Open connection and create instance
        /// </summary>
        /// <param name="defaultToken">Token</param>
        /// <returns>DatabaseManager Instance</returns>
        public static DatabaseManager CreateInstance(string defaultToken)
        {
            // Get connection information.
            String connectionString = 
                String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4}"
                                , SERVER_ADDRESS, SERVER_PORT, USER_ID, PASSWORD, DATABASE_NAME);

            DatabaseManager databaseManager = null;

            databaseManager = new NpgsqlDatabaseManager(connectionString);

            databaseManager._defaultProcessToken = defaultToken;

            return databaseManager;
        }

        /// <summary>
        /// コンストラクタです。
        /// </summary>
        /// <param name="config">データベース設定</param>
        protected DatabaseManager(string connectionString)
        {
            // Opening connection process
            for (int i = 0; i < MAX_RETRY_COUNT; i++)
            {
                try
                {
                    // Get connection and open database.
                    connection = new NpgsqlConnection(connectionString);
                    connection.Open();

                    break;
                }
                catch (System.Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();

                    connection.Dispose();
                    connection = null;

                    if (i < MAX_RETRY_COUNT - 1)
                    {
                        Thread.Sleep(RETRY_WAITING_TIME);
                        continue;
                    }

                    throw new Exception(@"Connection failed.", ex);
                }
            }

            // Increase connection count
            int currentConnectionCount = Interlocked.Increment(ref __currentConnectionCount);
        }

        /// <summary>
        /// Get connection
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        /// <returns>Connection</returns>
        protected abstract DbConnection GetConnection(string connectionString);

        /// <summary>
        /// Begin transaction
        /// </summary>
        public void BeginTransaction()
        {
            transaction = connection.BeginTransaction();
        }

        /// <summary>
        /// Commit transaction
        /// </summary>
        public void Commit()
        {
            transaction.Commit();
            transaction.Dispose();
            transaction = null;
        }

        /// <summary>
        /// Rollback transaction
        /// </summary>
        public void Rollback()
        {
            transaction.Rollback();
            transaction.Dispose();
            transaction = null;
        }

        /// <summary>
        /// Transaction started or not.
        /// </summary>
        public bool IsStartedTransaction
        {
            get
            {
                return (transaction != null);
            }
        }

        /// <summary>
        /// Discard the resource without sending an exception.
        /// </summary>
        public void DisposeQuietly()
        {
            Dispose();
        }

        /// <summary>
        /// Release resource.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Release resource
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (transaction != null)
                    {
                        transaction.Dispose();
                        transaction = null;
                    }

                    if (connection != null)
                    {
                        connection.Dispose();
                        connection = null;

                        //コネクション数をデクリメント
                        int currentConnectionCount = Interlocked.Decrement(ref __currentConnectionCount);
                    }
                }
                else
                {
                    if (connection != null)
                    {
                        //コネクション数をデクリメント。
                        //ファイナライザから呼ばれた場合、例外が発生すると、そのオブジェクトがGCされなくなり
                        //パフォーマンス低下を招くため、ログは出力せずデクリメントのみ行います。
                        Interlocked.Decrement(ref __currentConnectionCount);
                    }
                }
                disposed = true;
            }
        }

        /// <summary>
        /// Proceed to UPDATE/DELETE/INSERT
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <returns>Records number</returns>
        public int Update(string sql)
        {
            return Update(sql, new ParameterCollection());
        }

        /// <summary>
        /// UPDATE/INSERT/DELETEを実施します。
        /// </summary>
        /// <param name="sql">SQL文</param>
        /// <param name="parameters">埋め込むパラメータ</param>
        /// <returns>処理レコード数</returns>
        public int Update(string sql, ParameterCollection parameters)
        {
            return Update(sql, parameters, _defaultProcessToken);
        }

        /// <summary>
        /// UPDATE/INSERT/DELETEを実施します。
        /// </summary>
        /// <param name="sql">SQL文</param>
        /// <param name="parameters">埋め込むパラメータ</param>
        /// <param name="processToken">トークン</param>
        /// <returns>処理レコード数</returns>
        public int Update(string sql, ParameterCollection parameters, string processToken)
        {
            using (DbCommand command = CreateCommand(sql, processToken))
            {
                // パラメータを設定
                ApplyParameters(command, parameters);

                // コマンド実行
                int rowCount = command.ExecuteNonQuery();

                return rowCount;
            }
        }


        /// <summary>
        /// UPDATE/INSERT/DELETEを実施します。
        /// </summary>
        /// <param name="sql">SQL文</param>
        /// <param name="parameters">埋め込むパラメータ</param>
        /// <param name="processToken">トークン</param>
        /// <returns>処理レコード数</returns>
        public int Update(string sql, Dictionary<string, object> parameters, string processToken)
        {
            ParameterCollection collection =
                new ParameterCollection(parameters ?? new Dictionary<string, object>());

            return Update(sql, collection, processToken);
        }

        /// <summary>
        /// パラメータを設定します。
        /// </summary>
        /// <param name="command">コマンド</param>
        /// <param name="parameters">パラメータ</param>
        private void ApplyParameters(DbCommand command, ParameterCollection parameters)
        {
            foreach (KeyValuePair<string, object> parameter in parameters)
            {
                command.Parameters.Add(CreateParameter(parameter.Key, parameter.Value));
            }
        }

        /// <summary>
        /// パラメータを作成します。
        /// </summary>
        /// <param name="name">名前</param>
        /// <param name="value">値</param>
        /// <returns>パラメータ</returns>
        protected abstract DbParameter CreateParameter(string name, object value);

        /// <summary>
        /// DBコマンドを作成します。
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <param name="processToken">トークン</param>
        /// <returns>DBコマンド</returns>
        protected abstract DbCommand CreateCommand(string sql, string processToken);

        /// <summary>
        /// DBコマンドを作成します。
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <returns>DBコマンド</returns>
        protected DbCommand CreateCommand(string sql)
        {
            return CreateCommand(sql, _defaultProcessToken);
        }

        /// <summary>
        /// SELECT時のdelegateメソッドです。
        /// </summary>
        /// <param name="dataReader"></param>
        public delegate void SelectResultHandler(DbDataReader dataReader);

        /// <summary>
        /// 必要に応じて、型変換を行います。
        /// </summary>
        /// <param name="colValue">カラム</param>
        /// <returns>適切な型に変換したカラム</returns>
        protected virtual object Convert(object colValue)
        {
            if (colValue == DBNull.Value)
            {
                colValue = null;
            }

            return colValue;
        }

        /// <summary>
        /// SQLの先頭にトークンをコメントにして追加します。
        /// トークンがNULLまたは空文字の場合は、なにも追加しません。
        /// </summary>
        /// <param name="processToken">トークン</param>
        /// <param name="sql">SQL</param>
        /// <returns>SQL</returns>
        protected string AddProcessTokenToSql(string processToken, string sql)
        {
            if (!string.IsNullOrEmpty(processToken))
            {
                // 引数のトークンを追加
                return string.Format(CONCAT_TOKEN_AND_SQL_FORMAT, processToken, sql);
            }
            else
            {
                // トークンなし
                return sql;
            }
        }

        /// <summary>
        /// SELECTを実施します。
        /// 結果は、指定された型のリストとして返却します。
        /// 
        /// 結果が0件の場合は、0件のリストを返却します。
        /// (null を返却することはありません)
        /// 
        /// DBカラムと返却されるオブジェクトのプロパティのマッピングは下記のとおりです。
        /// ・ColumnNameAttributeが指定されている場合、そこで指定されたカラム名を使用
        /// ・ColumnNameAttributeが指定されていない場合、プロパティ名(CamelCase)を
        ///   アンダースコア区切りに変換し、すべて小文字としたものをカラム名として使用
        /// </summary>
        /// <typeparam name="T">返却するオブジェクトリストの型</typeparam>
        /// <param name="sql">SQL文</param>
        /// <returns>結果(リスト)</returns>
        public List<T> Select<T>(string sql)
            where T : new()
        {
            return Select<T>(sql, new ParameterCollection());
        }

        /// <summary>
        /// SELECTを実施します。
        /// 結果は、指定された型のリストとして返却します。
        /// </summary>
        /// <typeparam name="T">返却するオブジェクトリストの型</typeparam>
        /// <param name="sql">SQL文</param>
        /// <param name="parameters">埋め込むパラメータ</param>
        /// <returns>結果(リスト)</returns>
        public List<T> Select<T>(string sql, ParameterCollection parameters)
            where T : new()
        {
            return Select<T>(sql, parameters, _defaultProcessToken);
        }

        /// <summary>
        /// SELECTを実施します。
        /// 結果は指定された型のリストとして返却されます。
        /// 
        /// 結果が0件の場合は、0件のリストを返却します。
        /// (null を返却することはありません)
        /// 
        /// DBカラムと返却されるオブジェクトのプロパティのマッピングは下記のとおりです。
        /// ・ColumnNameAttributeが指定されている場合、そこで指定されたカラム名を使用
        /// ・ColumnNameAttributeが指定されていない場合、プロパティ名(CamelCase)を
        ///   アンダースコア区切りに変換し、すべて小文字としたものをカラム名として使用
        /// </summary>
        /// <typeparam name="T">返却するオブジェクトリストの型</typeparam>
        /// <param name="sql">SQL文</param>
        /// <param name="parameters">埋め込むパラメータ</param>
        /// <param name="processToken">トークン</param>
        /// <returns>結果(リスト)</returns>
        public List<T> Select<T>(string sql, ParameterCollection parameters, string processToken)
            where T : new()
        {
            // 返却するオブジェクトのプロパティ情報取得
            List<PropertyMapper> propertyMapper = GetPropertyMapper(typeof(T));

            // 結果格納用リスト
            List<T> resultList = new List<T>();

            // SELECTを実行し、結果は指定した型のオブジェクトにマッピング
            Select(sql, parameters, delegate (DbDataReader dataReader)
            {
                T rowObject = MappingProperty<T>(propertyMapper, dataReader);
                resultList.Add(rowObject);
            }, processToken);

            return resultList;
        }

        /// <summary>
        /// SELECTを実施します。
        /// 処理結果を1レコードずつSelectResultHandlerに渡し、処理を行わせます。
        /// </summary>
        /// <param name="sql">SQL文</param>
        /// <param name="parameters">埋め込むパラメータ</param>
        /// <param name="resultHandler">1レコード毎に処理するハンドラ</param>
        /// <param name="processToken">トークン</param>
        public void Select(string sql, ParameterCollection parameters, SelectResultHandler resultHandler, string processToken)
        {
            using (DbCommand command = CreateCommand(sql, processToken))
            {
                // パラメータを設定
                ApplyParameters(command, parameters);

                // コマンド実行
                int count = 0;
                using (DbDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        // 結果の1レコードずつ処理呼び出し
                        resultHandler(dataReader);
                        count++;
                    }
                }
            }
        }

        /// <summary>
        /// 1レコードの内容をマッピングします。
        /// </summary>
        /// <typeparam name="T">返却するオブジェクトの型</typeparam>
        /// <param name="propertyMapper">プロパティマッパーの一覧</param>
        /// <param name="dataReader">データソースリーダ</param>
        /// <returns>マッピングしたオブジェクト</returns>
        private T MappingProperty<T>(List<PropertyMapper> propertyMapper, DbDataReader dataReader) where T : new()
        {
            T rowObject = new T();
            foreach (PropertyMapper property in propertyMapper)
            {
                object colValue = Convert(dataReader[property.ColumnName]);
                if (colValue != null) // NULLの場合は設定しない(デフォルト値のまま)
                {
                    property.SetValue(rowObject, colValue);
                }
            }
            return rowObject;
        }

        /// <summary>
        /// プロパティとのマッピングを行うクラスです。
        /// </summary>
        private class PropertyMapper
        {
            /// <summary>
            /// マッピング対象のカラム名です。
            /// </summary>
            private readonly string columnName;

            /// <summary>
            /// マッピング対象のプロパティです。
            /// </summary>
            private readonly PropertyInfo property;

            /// <summary>
            /// コンバーターです。
            /// </summary>
            /// <reremarks>
            /// コンバートの必要がない場合は、null を設定します。
            /// </reremarks>
            private readonly TypeConverter converter;

            /// <summary>
            /// マッピング対象のカラム名を取得します。
            /// </summary>
            public string ColumnName
            {
                get
                {
                    return columnName;
                }
            }

            /// <summary>
            /// このクラスを構築します。
            /// </summary>
            /// <param name="columnName">マッピング対象のカラム名</param>
            /// <param name="property">マッピング対象のプロパティ</param>
            /// <param name="converter">コンバーター （コンバートの必要がない場合は null）</param>
            public PropertyMapper(string columnName, PropertyInfo property, TypeConverter converter)
            {
                this.property = property;
                this.columnName = columnName;
                this.converter = converter;
            }

            /// <summary>
            /// プロパティに値を設定します。
            /// </summary>
            /// <param name="obj">オブジェクト</param>
            /// <param name="value">設定する値</param>
            public void SetValue(object obj, object value)
            {
                if (converter != null)
                {
                    value = converter.ConvertFrom(value);
                }

                property.SetValue(obj, value, null);
            }
        }

        /// <summary>
        /// 指定された型のプロパティとDBカラム名のマッピングを取得します。
        /// </summary>
        /// <param name="objType">プロパティ情報を取得する型</param>
        /// <returns>型のプロパティとDBカラム名のマッピング</returns>
        private List<PropertyMapper> GetPropertyMapper(Type objType)
        {
            PropertyInfo[] properties = objType.GetProperties();

            // プロパティ情報とDBカラム名のマッピング
            List<PropertyMapper> propertyMapper = new List<PropertyMapper>(properties.Length);

            foreach (PropertyInfo propertyInfo in properties)
            {
                // NotMapped 属性がついているプロパティは、マッピング対象外
                if (Attribute.IsDefined(propertyInfo, typeof(NotMappedAttribute)))
                {
                    continue;
                }

                Attribute attribute =
                    Attribute.GetCustomAttribute(propertyInfo, typeof(ColumnNameAttribute));
                string columnName = null;
                if (attribute != null)
                {
                    // ColumnName属性が指定されていた場合はそれをカラム名として使用
                    columnName = ((ColumnNameAttribute)attribute).Name;
                }
                else
                {
                    // ColumnName属性が指定されていない場合には、プロパティ名を
                    // CamelCaseからスペース区切りに変換
                    columnName = CAMEL_CASE_REGEX.Replace(propertyInfo.Name, UNDER_SCORE_REPLACE).ToLower();

                    // Identification 属性が指定されている型の場合、カラム名に "_id" を付与する
                    // (プロパティ名に、Id を省略できる)
                    Type underlyingType =
                        Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                    if (Attribute.IsDefined(underlyingType, typeof(IdentificationAttribute), false))
                    {
                        columnName += "_id";
                    }
                }

                propertyMapper.Add(
                    new PropertyMapper(columnName, propertyInfo, GetConverter(propertyInfo.PropertyType)));
            }

            return propertyMapper;
        }

        /// <summary>
        /// 型コンバーターを取得します。
        /// </summary>
        /// <param name="type">型コンバーター取得対象の型</param>
        /// <returns>型コンバーター (対象の型に TypeConverter 属性が指定されていない場合は null)</returns>
        private TypeConverter GetConverter(Type type)
        {
            // Nullable<T> 型の場合、T をコンバーター取得対象にする
            type = Nullable.GetUnderlyingType(type) ?? type;

            if (Attribute.IsDefined(type, typeof(TypeConverterAttribute), false))
            {
                return TypeDescriptor.GetConverter(type);
            }

            return null;
        }

        /// <summary>
        /// SELECTを実施します。
        /// 結果は、指定された型のリストとして、先頭のカラムのみ返却します。
        /// 
        /// 結果が0件の場合は、0件のリストを返却します。
        /// (null を返却することはありません)
        /// </summary>
        /// <typeparam name="T">返却するオブジェクトリストの型</typeparam>
        /// <param name="sql">SQL文</param>
        /// <returns>結果(リスト)</returns>
        public List<T> SelectFirstColumn<T>(string sql)
        {
            return SelectFirstColumn<T>(sql, new ParameterCollection());
        }

        /// <summary>
        /// SELECTを実施します。
        /// 結果は、指定された型のリストとして、先頭のカラムのみ返却します。
        /// 
        /// 結果が0件の場合は、0件のリストを返却します。
        /// (null を返却することはありません)
        /// </summary>
        /// <typeparam name="T">返却するオブジェクトリストの型</typeparam>
        /// <param name="sql">SQL文</param>
        /// <param name="parameters">埋め込むパラメータ</param>
        /// <returns>結果(リスト)</returns>
        public List<T> SelectFirstColumn<T>(string sql, ParameterCollection parameters)
        {
            return SelectFirstColumn<T>(sql, parameters, _defaultProcessToken);
        }

        /// <summary>
        /// SELECTを実施し、先頭のカラムのみを取得します。
        /// 結果は指定された型のリストとして返却されます。
        /// </summary>
        /// <typeparam name="T">返却するオブジェクトリストの型</typeparam>
        /// <param name="sql">SQL文</param>
        /// <param name="parameters">埋め込むパラメータ</param>
        /// <param name="processToken">トークン</param>
        /// <returns>結果(リスト)</returns>
        public List<T> SelectFirstColumn<T>(string sql, ParameterCollection parameters, string processToken)
        {
            // 結果格納用リスト
            List<T> resultList = new List<T>();
            TypeConverter converter = GetConverter(typeof(T));

            // SELECTを実行し、結果の先頭のカラムを取得
            Select(sql, parameters, delegate (DbDataReader dataReader)
            {
                object colValue = Convert(dataReader[0]);

                if (colValue != null && converter != null)
                {
                    resultList.Add((T)converter.ConvertFrom(colValue));
                }
                else
                {
                    resultList.Add((T)colValue);
                }
            }, processToken);

            return resultList;
        }

        /// <summary>
        /// SELECTを実施します。
        /// 結果は、指定された型で、先頭のカラムのみ返却します。
        /// 
        /// 結果が0件の場合は、<typeparamref name="T"/>により処理が異なります。
        /// ・<typeparamref name="T"/> が参照型か NULL 許容型の場合、null を返却します。
        /// ・<typeparamref name="T"/> が値型の場合、NullReferenceException をスローします。
        /// </summary>
        /// <typeparam name="T">返却するオブジェクトの型</typeparam>
        /// <param name="sql">SQL文</param>
        /// <param name="parameters">埋め込むパラメータ</param>
        /// <returns>結果</returns>
        public T SelectFirstColumnOne<T>(string sql, Dictionary<string, object> parameters)
        {
            ParameterCollection collection =
                new ParameterCollection(parameters ?? new Dictionary<string, object>());

            return SelectFirstColumnOne<T>(sql, collection);
        }

        /// <summary>
        /// SELECTを実施します。
        /// 結果は、指定された型で、先頭のカラムのみ返却します。
        /// 
        /// 結果が0件の場合は、<typeparamref name="T"/>により処理が異なります。
        /// ・<typeparamref name="T"/> が参照型か NULL 許容型の場合、null を返却します。
        /// ・<typeparamref name="T"/> が値型の場合、NullReferenceException をスローします。
        /// </summary>
        /// <typeparam name="T">返却するオブジェクトの型</typeparam>
        /// <param name="sql">SQL文</param>
        /// <param name="parameters">埋め込むパラメータ</param>
        /// <returns>結果</returns>
        public T SelectFirstColumnOne<T>(string sql, ParameterCollection parameters)
        {
            using (DbCommand command = CreateCommand(sql))
            {
                // パラメータを設定
                ApplyParameters(command, parameters);

                object result = Convert(command.ExecuteScalar());

                TypeConverter converter = GetConverter(typeof(T));

                if (result != null && converter != null)
                {
                    return (T)converter.ConvertFrom(result);
                }
                else
                {
                    return (T)result;
                }
            }
        }

        /// <summary>
        /// SELECTを実施します。
        /// 結果は、指定された型で、先頭のカラムのみ返却します。
        /// 
        /// 結果が0件の場合は、<typeparamref name="T"/>により処理が異なります。
        /// ・<typeparamref name="T"/> が参照型か NULL 許容型の場合、null を返却します。
        /// ・<typeparamref name="T"/> が値型の場合、NullReferenceException をスローします。
        /// </summary>
        /// <typeparam name="T">返却するオブジェクトの型</typeparam>
        /// <param name="sql">SQL文</param>
        /// <returns>結果</returns>
        public T SelectFirstColumnOne<T>(string sql)
        {
            return SelectFirstColumnOne<T>(sql, new ParameterCollection());
        }
    }
}
