using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalOperationSearch.Data
{
    public class SearchFilterResult
    {
        /// <summary>
        /// Temporary table name.
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// Hit Count
        /// </summary>
        public int HitCount { get; set; }
    }
}
