using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicalOperationSearch.Data;

namespace LogicalOperationSearch.Search
{
    public class AndSearchOperator : AbstractOperatorSearchFilter
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="param"></param>
        public AndSearchOperator(List<AbstractSearchFilter> param) 
            : base(param)
        {
        }

        /// <summary>
        /// Execute search.
        /// </summary>
        /// <param name="sourceData"></param>
        /// <returns></returns>
        public override SearchFilterResult Search(SearchFilterResult sourceData)
        {
            foreach (AbstractSearchFilter filter in SearchParams)
            {
                // 検索を実施する
                sourceData = filter.Search(sourceData);

                // AND演算なので、検索結果がない場合は、次の検索を行ったら、意味がない
                if (sourceData.HitCount == 0)
                {
                    break;
                }
            }

            // 結果返却
            return sourceData;
        }
    }
}
