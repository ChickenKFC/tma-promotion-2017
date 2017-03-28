using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalOperationSearch.Db
{
    public class ParameterCollection : IEnumerable<KeyValuePair<string, object>>
    {
        /// <summary>
        /// NULL文字です。
        /// </summary>
        private const char NULL_CHAR = '\u0000';

        /// <summary>
        /// NULL文字に対して置き換える文字です。
        /// </summary>
        private const char NULL_REPLACE_CHAR = ' '; // 半角スペース

        /// <summary>
        /// パラメータです。
        /// </summary>
        private Dictionary<string, object> parameters = new Dictionary<string, object>();

        /// <summary>
        /// 格納されているパラメータの数を取得します。
        /// </summary>
        public int Count
        {
            get
            {
                return parameters.Count;
            }
        }

        /// <summary>
        /// パラメータコレクションを構築します。
        /// </summary>
        public ParameterCollection()
        {
        }

        /// <summary>
        /// パラメータコレクションを構築します。
        /// </summary>
        /// <param name="param">基となるパラメータ</param>
        public ParameterCollection(params ParameterCollection[] param)
        {
            foreach (KeyValuePair<string, object> pair in param.SelectMany(p => p.parameters))
            {
                parameters.Add(pair.Key, pair.Value);
            }
        }

        /// <summary>
        /// パラメータコレクションを構築します。
        /// </summary>
        /// <param name="param">基となるパラメータ</param>
        internal ParameterCollection(Dictionary<string, object> param)
        {
            foreach (KeyValuePair<string, object> pair in param)
            {
                string str = pair.Value as string;
                if (str != null)
                {
                    Add(pair.Key, str);
                    continue;
                }

                IList<string> strList = pair.Value as IList<string>;
                if (strList != null)
                {
                    AddInternal(pair.Key, strList);
                    continue;
                }

                // 変換せずにパラメータとして使用する
                // (基になる型への変換は、互換性のため行わない)
                parameters.Add(pair.Key, pair.Value);
            }
        }

        /// <summary>
        /// パラメータを追加します。
        /// </summary>
        /// <param name="key">パラメータのキー</param>
        /// <param name="value">パラメータの値</param>
        public void Add(string key, string value)
        {
            if (value == null)
            {
                parameters.Add(key, value);
                return;
            }

            parameters.Add(key, ReplaceNull(value));
        }

        /// <summary>
        /// パラメータを追加します。
        /// </summary>
        /// <param name="key">パラメータのキー</param>
        /// <param name="value">パラメータの値</param>
        public void Add(string key, string[] value)
        {
            AddInternal(key, value);
        }

        /// <summary>
        /// パラメータを追加します。
        /// </summary>
        /// <param name="key">パラメータのキー</param>
        /// <param name="value">パラメータの値</param>
        public void Add(string key, List<string> value)
        {
            AddInternal(key, value);
        }

        /// <summary>
        /// パラメータを追加します。
        /// </summary>
        /// <param name="key">パラメータのキー</param>
        /// <param name="value">パラメータの値</param>
        private void AddInternal(string key, IList<string> value)
        {
            if (value == null)
            {
                parameters.Add(key, value);
                return;
            }

            parameters.Add(
                key, value.Select(e => e != null ? ReplaceNull(e) : null).ToList());
        }

        /// <summary>
        /// パラメータを追加します。
        /// </summary>
        /// <typeparam name="T">パラメータの型</typeparam>
        /// <param name="key">パラメータのキー</param>
        /// <param name="value">パラメータの値</param>
        public void Add<T>(string key, T value) where T : struct, IConvertible
        {
            // プリミティブ型か DateTime 型の場合は、そのままパラメータとして使用可能
            Type type = typeof(T);
            if (type.IsPrimitive || type == typeof(DateTime))
            {
                parameters.Add(key, value);
                return;
            }

            parameters.Add(key, Convert.ChangeType(value, value.GetTypeCode()));
        }

        /// <summary>
        /// パラメータを追加します。
        /// </summary>
        /// <typeparam name="T">パラメータの型</typeparam>
        /// <param name="key">パラメータのキー</param>
        /// <param name="value">パラメータの値</param>
        public void Add<T>(string key, Nullable<T> value) where T : struct, IConvertible
        {
            // プリミティブ型か DateTime 型の場合は、そのままパラメータとして使用可能
            Type type = typeof(T);
            if (type.IsPrimitive || type == typeof(DateTime))
            {
                parameters.Add(key, value);
                return;
            }

            parameters.Add(key, ConvertUnderlyingType(value));
        }

        /// <summary>
        /// パラメータを追加します。
        /// </summary>
        /// <typeparam name="T">パラメータの型</typeparam>
        /// <param name="key">パラメータのキー</param>
        /// <param name="value">パラメータの値</param>
        public void Add<T>(string key, T[] value) where T : struct, IConvertible
        {
            AddInternal(key, value);
        }

        /// <summary>
        /// パラメータを追加します。
        /// </summary>
        /// <typeparam name="T">パラメータの型</typeparam>
        /// <param name="key">パラメータのキー</param>
        /// <param name="value">パラメータの値</param>
        public void Add<T>(string key, List<T> value) where T : struct, IConvertible
        {
            AddInternal(key, value);
        }

        /// <summary>
        /// パラメータを追加します。
        /// </summary>
        /// <typeparam name="T">パラメータの型</typeparam>
        /// <param name="key">パラメータのキー</param>
        /// <param name="value">パラメータの値</param>
        private void AddInternal<T>(string key, IList<T> value) where T : struct, IConvertible
        {
            // プリミティブ型か DateTime 型か null の場合は、そのままパラメータとして使用可能
            Type type = typeof(T);
            if (type.IsPrimitive || type == typeof(DateTime) || value == null)
            {
                parameters.Add(key, value);
                return;
            }

            // 基になる型を取得
            Type underlyingType = GetUnderlyingType(type);

            // 基になる型の配列に変換
            Array array = Array.CreateInstance(underlyingType, value.Count);
            for (int i = 0; i < value.Count; i++)
            {
                array.SetValue(Convert.ChangeType(value[i], underlyingType), i);
            }

            parameters.Add(key, array);
        }

        /// <summary>
        /// パラメータを追加します。
        /// </summary>
        /// <typeparam name="T">パラメータの型</typeparam>
        /// <param name="key">パラメータのキー</param>
        /// <param name="value">パラメータの値</param>
        public void Add<T>(string key, Nullable<T>[] value) where T : struct, IConvertible
        {
            AddInternal(key, value);
        }

        /// <summary>
        /// パラメータを追加します。
        /// </summary>
        /// <typeparam name="T">パラメータの型</typeparam>
        /// <param name="key">パラメータのキー</param>
        /// <param name="value">パラメータの値</param>
        public void Add<T>(string key, List<Nullable<T>> value) where T : struct, IConvertible
        {
            AddInternal(key, value);
        }

        /// <summary>
        /// パラメータを追加します。
        /// </summary>
        /// <typeparam name="T">パラメータの型</typeparam>
        /// <param name="key">パラメータのキー</param>
        /// <param name="value">パラメータの値</param>
        private void AddInternal<T>(string key, IList<Nullable<T>> value) where T : struct, IConvertible
        {
            // プリミティブ型か DateTime 型か null の場合は、そのままパラメータとして使用可能
            Type type = typeof(T);
            if (type.IsPrimitive || type == typeof(DateTime) || value == null)
            {
                parameters.Add(key, value);
                return;
            }

            // 基になる型を取得
            Type underlyingType = GetUnderlyingType(type);

            // Nullable でラップ
            Type nullableUnderlyingType = typeof(Nullable<>).MakeGenericType(underlyingType);

            // Nullable でラップした基になる型の配列に変換
            Array array = Array.CreateInstance(nullableUnderlyingType, value.Count);
            for (int i = 0; i < value.Count; i++)
            {
                array.SetValue(ConvertUnderlyingType(value[i]), i);
            }

            parameters.Add(key, array);
        }

        /// <summary>
        /// 対象の型の基になる型に、要素を変換します。
        /// </summary>
        /// <typeparam name="T">要素の型</typeparam>
        /// <param name="element">要素</param>
        /// <returns>基になる型に変換した値</returns>
        private object ConvertUnderlyingType<T>(Nullable<T> element) where T : struct
        {
            if (element.HasValue)
            {
                IConvertible convertible = (IConvertible)element.Value;

                return Convert.ChangeType(convertible, convertible.GetTypeCode());
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 対象の型の基になる型を取得します。
        /// </summary>
        /// <param name="type">対象の型</param>
        /// <returns>基になる型</returns>
        private Type GetUnderlyingType(Type type)
        {
            if (type.IsEnum)
            {
                return type.GetEnumUnderlyingType();
            }
            else
            {
                IdentificationAttribute attribute =
                    (IdentificationAttribute)Attribute.GetCustomAttribute(type, typeof(IdentificationAttribute));
                if (attribute != null)
                {
                    return attribute.UnderlyingType;
                }
            }

            throw new ArgumentException("基になる型を取得できませんでした。Type=[" + type.FullName + "]");
        }

        /// <summary>
        /// 文字列中の NULL 文字を置換します。
        /// </summary>
        /// <param name="str">文字列</param>
        /// <returns>NULL 文字を置換した文字列</returns>
        private string ReplaceNull(string str)
        {
            if (str.IndexOf(NULL_CHAR) != -1)
            {
                return str.Replace(NULL_CHAR, NULL_REPLACE_CHAR);
            }

            return str;
        }

        /// <summary>
        /// コレクションを反復処理する列挙子を返します。
        /// </summary>
        /// <returns>コレクションを反復処理するために使用できる IEnumerator&lt;T&gt;</returns>
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return parameters.GetEnumerator();
        }

        /// <summary>
        /// コレクションを反復処理する列挙子を返します。
        /// </summary>
        /// <returns>コレクションを反復処理するために使用できる IEnumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return parameters.GetEnumerator();
        }
    }
}
