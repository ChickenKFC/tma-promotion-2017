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
    public class GenericTextSearchFilter : AbstractSearchFilter
    {
        /// <summary>
        /// Search target.
        /// </summary>
        protected SearchTargets SearchTarget;

        /// <summary>
        /// Search condition
        /// </summary>
        protected string SearchCondition;

        /// <summary>
        /// Target search table.
        /// </summary>
        protected string TargetTableSearch = string.Empty;

        /// <summary>
        /// Constructors.
        /// </summary>
        /// <param name="targetTable">target table</param>
        /// <param name="searchCondition">search condition</param>
        public GenericTextSearchFilter(SearchTargets targetTable, string searchCondition)
        {
            this.SearchTarget = targetTable;
            this.SearchCondition = searchCondition;

            // dertermine search column
            switch (SearchTarget)
            {
                case SearchTargets.Physical:
                    TargetTableSearch = "physical";
                    break;

                case SearchTargets.Mathematics:
                    TargetTableSearch = "mathematics";
                    break;

                case SearchTargets.Sciencetifics:
                    TargetTableSearch = "scientific";
                    break;

                case SearchTargets.Barcode:
                    TargetTableSearch = "bar_code";
                    break;

                default:
                    break;
            }
        }

        public override SearchFilterResult Search(SearchFilterResult sourceData)
        {
            throw new NotImplementedException();
        }
    }
}
