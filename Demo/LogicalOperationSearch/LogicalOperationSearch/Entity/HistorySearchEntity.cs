using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalOperationSearch.Entity
{
    public class HistorySearchEntity
    {
        /// <summary>
        /// History search id
        /// </summary>
        public int SearchHistoryId { get; set; }

        /// <summary>
        /// Search target table.
        /// </summary>
        public int SearchTargetTable { get; set; }

        /// <summary>
        /// Search target column
        /// </summary>
        public int SearchTargetColumn { get; set; }

        /// <summary>
        /// Search condition text.
        /// </summary>
        public string SearchCondition { get; set; }

        /// <summary>
        /// From date
        /// </summary>
        public DateTime? FromDate { get; set; }

        /// <summary>
        /// To Date
        /// </summary>
        public DateTime? ToDate { get; set; }

        /// <summary>
        /// Delete flag.
        /// </summary>
        public bool DeleteFlag { get; set; }
    }
}
