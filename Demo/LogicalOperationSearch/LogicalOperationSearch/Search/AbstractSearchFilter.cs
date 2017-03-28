using LogicalOperationSearch.Dao;
using LogicalOperationSearch.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalOperationSearch.Search
{
    public abstract class AbstractSearchFilter
    {
        /// <summary>
        /// Temporary table name
        /// </summary>
        private static string WORK_TEMPORARY_TABLE_NAME_FORMAT = "temp_patent_search_{0}";

        /// <summary>
        /// Table index
        /// </summary>
        private static int tableIndex = 0;

        /// <summary>
        /// Lock object
        /// </summary>
        private static readonly Object lockObj = new Object();

        /// <summary>
        /// Array temporary table names.
        /// </summary>
        private List<string> TemporaryTables = new List<string>();

        /// <summary>
        /// Default contructor
        /// </summary>
        public AbstractSearchFilter()
        {
        }

        /// <summary>
        /// Search execute
        /// </summary>
        /// <param name="sourceData">source data</param>
        /// <returns></returns>
        public abstract SearchFilterResult Search(SearchFilterResult sourceData);

        /// <summary>
        /// Create temp table.
        /// </summary>
        protected string createTempTable()
        {
            // Generate table index.
            int tableNum = increaseTableIndex();

            // Generate table name.
            string tableName = string.Format(WORK_TEMPORARY_TABLE_NAME_FORMAT, tableNum);

            // Add temporary table name into list
            // we will delete once time at the end searching process.
            TemporaryTables.Add(tableName);

            // Call function to create new table.
            new TempDao().CreateTempTable(tableName);

            return tableName;
        }

        /// <summary>
        /// Delete temp table.
        /// </summary>
        protected virtual void dropTemporaryTables()
        {
            if (TemporaryTables.Count > 0)
            {
                // Join all table names into a string.
                string tableNames = string.Join(",", TemporaryTables);
                new TempDao().DropTempTables(tableNames);
            }
        }

        /// <summary>
        /// Increase table number index
        /// </summary>
        /// <returns></returns>
        private int increaseTableIndex()
        {
            lock (lockObj)
            {
                if (tableIndex == Int32.MaxValue)
                {
                    tableIndex = 0;
                }else
                {
                    tableIndex++;
                }

                return tableIndex;
            }
        }  
    }
}
