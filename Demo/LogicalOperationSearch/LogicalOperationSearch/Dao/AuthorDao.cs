using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalOperationSearch.Dao
{
    public class AuthorDao : AbstractDao
    {
        /// <summary>
        /// Search and register search result into temporary tables.
        /// </summary>
        /// <param name="tempTable">Temp table name</param>
        /// <param name="searchCondition">Search condition</param>
        /// <param name="customSearchSQL">Merge result with previous search</param>
        /// <returns>Hit count</returns>
        public int SearchAndRegisterTemp(string tempTable, string tableName,
            string searchCondition, string customSearchSQL)
        {
            return databaseManager.Update(
              string.Format(@"
                INSERT INTO {0} (book_id, book_name)
                SELECT 
                  bks.book_id,
                  target.book_name
                FROM books bks
                  INNER JOIN {1} target
                    ON target.book_id = bks.book_id
                  {2}
                WHERE bks.author LIKE '%{3}%'",
                tempTable,
                tableName,
                customSearchSQL,
                searchCondition));
        }
    }
}
