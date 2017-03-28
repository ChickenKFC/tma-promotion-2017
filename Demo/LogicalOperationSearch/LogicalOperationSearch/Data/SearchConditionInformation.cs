using LogicalOperationSearch.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalOperationSearch.Data
{
    public class SearchConditionInformation
    {
        /// <summary>
        /// Search type
        /// </summary>
        public SearchTypes SearchType { get; set; }
        
        /// <summary>
        /// Search target
        /// </summary>
        public SearchTargets SearchTarget { get; set; } 

        /// <summary>
        /// From date
        /// </summary>
        public DateTime? FromDate { get; set; }

        /// <summary>
        /// To date
        /// </summary>
        public DateTime? ToDate { get; set; }

        /// <summary>
        /// Text condition
        /// </summary>
        public string TextCondition { get; set; }
    }
}
