using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalOperationSearch.Dao
{
    public class TempDao : AbstractDao
    {
        /// <summary>
        /// Create temporary table.
        /// </summary>
        /// <param name="tableName">Temporary table name</param>
        public void CreateTempTable(string tableName)
        {
            databaseManager.Update(
                string.Format(@"
                    DROP TABLE IF EXISTS {0};
                    CREATE TEMP TABLE {0} 
                      (
                        book_id  integer NOT NULL,
                        book_name varchar NOT NULL
                      )", tableName));
        }

        /// <summary>
        /// Drop all temporary tables
        /// </summary>
        /// <param name="tableNames"></param>
        public void DropTempTables(string tableNames)
        {
            databaseManager.Update(
                string.Format(@"
                  DROP TABLE IF EXISTS {0}", tableNames));
        }
    }
}
