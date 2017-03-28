using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalOperationSearch.Dao
{
    class BookNameDao : AbstractDao
    {
        /// <summary>
        /// Search and register search result into temporary tables.
        /// </summary>
        /// <param name="tempTable">Temp table name</param>
        /// <param name="targetColumn">Target column</param>
        /// <param name="customSearchSQL">Merge result with previous search</param>
        /// <returns>Hit count</returns>
        public int SearchAndRegisterTemp(string tempTable, string tableName,
            string searchCondition, string customSearchSQL)
        {
            return databaseManager.Update(
              string.Format(@"
                INSERT INTO {0} (book_id, book_name)
                SELECT 
                  des.book_id,
                  des.book_name
                FROM {1} des
                  {2}
                WHERE des.book_name LIKE '%{3}%'",
                tempTable,
                tableName,
                customSearchSQL,
                searchCondition));
        }
    }
}
