using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicalOperationSearch.Data;

namespace LogicalOperationSearch.Search
{
    public abstract class AbstractOperatorSearchFilter : AbstractSearchFilter
    {
        /// <summary>
        /// Search params
        /// </summary>
        protected List<AbstractSearchFilter> SearchParams { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="param"></param>
        public AbstractOperatorSearchFilter(List<AbstractSearchFilter> param)
        {
            this.SearchParams = param;
        }
    }
}
