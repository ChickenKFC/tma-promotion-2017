using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalOperationSearch.Dao
{
    public class BarCodeDao : AbstractDao
    {
        /// <summary>
        /// Search and register search result into temporary tables.
        /// </summary>
        /// <param name="tempTable">Temp table name</param>
        /// <param name="tableName"></param>
        /// <param name="barCode"></param>
        /// <param name="customSearchSQL">Merge result with previous search</param>
        /// <returns>Hit count</returns>
        public int SearchAndRegisterTemp(string tempTable, string tableName,
            string barCode, string customSearchSQL)
        {
            return databaseManager.Update(
              string.Format(@"
                INSERT INTO {0} (book_id, book_name)
                SELECT 
                  bc.book_id,
                  target.book_name
                FROM barcodes bc
                  INNER JOIN {1} target
                    ON target.book_id = bc.book_id
                  {2}
                WHERE bc.bar_code LIKE '%{3}%'",
                tempTable,
                tableName,
                customSearchSQL,
                barCode));
        }
    }
}
