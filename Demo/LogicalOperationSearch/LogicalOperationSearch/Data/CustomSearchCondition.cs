using LogicalOperationSearch.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalOperationSearch.Data
{
    class CustomSearchCondition
    {
        /// <summary>
        /// List of search history.
        /// </summary>
        public List<HistorySearchData> SearchConditionList { get; set; }

        /// <summary>
        /// Search Operator
        /// </summary>
        public LogicalOperator Operator { get; set; }
    }
}
