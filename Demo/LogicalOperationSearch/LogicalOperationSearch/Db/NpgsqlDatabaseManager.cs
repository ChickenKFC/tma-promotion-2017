using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalOperationSearch.Db
{
    public class NpgsqlDatabaseManager : DatabaseManager
    {
        public NpgsqlDatabaseManager(string connectionString) 
            : base(connectionString)
        {
        }

        /// <summary>
        /// Get connection
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        /// <returns>Connection</returns>
        protected override DbConnection GetConnection(string connectionString)
        {
            return new NpgsqlConnection(connectionString);
        }

        /// <summary>
        /// DBコマンドを作成します。
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <param name="processToken">トークン</param>
        /// <returns>DBコマンド</returns>
        protected override DbCommand CreateCommand(string sql, string processToken)
        {
            return new NpgsqlCommand(this.AddProcessTokenToSql(processToken, sql), (NpgsqlConnection)connection);
        }

        /// <summary>
        /// パラメータを作成します。
        /// </summary>
        /// <param name="name">名前</param>
        /// <param name="value">値</param>
        /// <returns>パラメータ</returns>
        protected override DbParameter CreateParameter(string name, object value)
        {
            return new NpgsqlParameter(name, value);
        }

        /// <summary>
        /// 必要に応じて、型変換を行います。
        /// </summary>
        /// <param name="colValue">カラム</param>
        /// <returns>適切な型に変換したカラム</returns>
        protected override object Convert(object colValue)
        {
            NpgsqlTypes.NpgsqlDate[] npgsqlDateArray = colValue as NpgsqlTypes.NpgsqlDate[];
            if (npgsqlDateArray != null)
            {
                return Array.ConvertAll<NpgsqlTypes.NpgsqlDate, DateTime>(npgsqlDateArray, date => (DateTime)date);
            }

            return base.Convert(colValue);
        }
    }
}
