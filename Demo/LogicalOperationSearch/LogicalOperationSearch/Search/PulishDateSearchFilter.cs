using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicalOperationSearch.Data;
using LogicalOperationSearch.Resource;
using LogicalOperationSearch.Dao;

namespace LogicalOperationSearch.Search
{
    class PulishDateSearchFilter : AbstractSearchFilter
    {
        /// <summary>
        /// Search target table.
        /// </summary>
        private SearchTargets SearchTarget;

        /// <summary>
        /// From
        /// </summary>
        private DateTime From;

        /// <summary>
        /// To
        /// </summary>
        private DateTime To;

        /// <summary>
        /// Target table name
        /// </summary>
        private string TargetTableName;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="targetTable"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public PulishDateSearchFilter(SearchTargets targetTable, DateTime from, DateTime to)
        {
            SearchTarget = targetTable;
            From = from;
            To = to;


            // dertermine search column
            switch (SearchTarget)
            {
                case SearchTargets.Physical:
                    TargetTableName = "physical";
                    break;

                case SearchTargets.Mathematics:
                    TargetTableName = "mathematics";
                    break;

                case SearchTargets.Sciencetifics:
                    TargetTableName = "scientific";
                    break;

                case SearchTargets.Barcode:
                    TargetTableName = "bar_code";
                    break;

                default:
                    break;
            }
        }

        public override SearchFilterResult Search(SearchFilterResult sourceData)
        {
            // Generate table name.
            string tempTable = createTempTable();

            string innerSQL = string.Empty;
            if (sourceData.HitCount != 0)
            {
                // Create SQL to merge before search result.
                innerSQL = string.Format(
                    @"INNER JOIN {0} source
                        ON source.book_id = bks.book_id", sourceData.TableName);
            }

            int hitCount =
                new PulishDateDao().SearchAndRegisterTemp(
                    tempTable, TargetTableName, From, To, innerSQL);

            // return result.
            return new SearchFilterResult()
            {
                HitCount = hitCount,
                TableName = tempTable
            };

        }
    }
}
