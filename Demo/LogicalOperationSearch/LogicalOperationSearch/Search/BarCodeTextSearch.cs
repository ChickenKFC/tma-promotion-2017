using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicalOperationSearch.Resource;
using LogicalOperationSearch.Data;
using LogicalOperationSearch.Dao;

namespace LogicalOperationSearch.Search
{
    public class BarCodeTextSearch : GenericTextSearchFilter
    {
        public BarCodeTextSearch(SearchTargets targetTable, string searchCondition)
            : base(targetTable, searchCondition)
        {
        }

        /// <summary>
        /// Search
        /// </summary>
        /// <param name="sourceData">previos search result</param>
        /// <returns></returns>
        public override SearchFilterResult Search(SearchFilterResult sourceData)
        {
            // Generate temporary tables.
            string tempTableName = createTempTable();

            string innerSQL = string.Empty;
            if (sourceData.HitCount != 0)
            {
                // Create SQL to merge before search result.
                innerSQL = string.Format(
                    @"INNER JOIN {0} source
                        ON source.book_id = bc.book_id", sourceData.TableName);
            }

            int hitCount =
                new BarCodeDao().SearchAndRegisterTemp(
                    tempTableName, TargetTableSearch, SearchCondition, innerSQL);

            // return result.
            return new SearchFilterResult()
            {
                HitCount = hitCount,
                TableName = tempTableName
            };
        }
    }
}
