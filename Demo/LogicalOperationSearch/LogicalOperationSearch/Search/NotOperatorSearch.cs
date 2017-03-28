using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicalOperationSearch.Data;
using LogicalOperationSearch.Db;

namespace LogicalOperationSearch.Search
{
    public class NotOperatorSearch : AbstractOperatorSearchFilter
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="param"></param>
        public NotOperatorSearch(List<AbstractSearchFilter> param)
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
            SearchFilterResult operateResult = new SearchFilterResult();

            if (SearchParams.Count > 0)
            {
                // Search first items.
                operateResult = SearchParams.First().Search(sourceData);
                SearchParams.RemoveAt(0);

                if (operateResult.HitCount > 0)
                {
                    // Loop on each SearchCondition and Search.
                    foreach (AbstractSearchFilter filter in SearchParams)
                    {
                        SearchFilterResult tempResult = filter.Search(sourceData);

                        if (tempResult.HitCount > 0)
                        {
                            // Combine with previous search result.
                            operateResult = CombineResult(operateResult, tempResult);
                        }
                    }
                }
            }

            return operateResult;
        }

        /// <summary>
        /// Combine with previous search result.
        /// </summary>
        /// <param name="leftResult">left result</param>
        /// <param name="rightResult">right result</param>
        /// <returns>combine result</returns>
        private SearchFilterResult CombineResult(
            SearchFilterResult leftResult, SearchFilterResult rightResult)
        {
            // create temp table to store combine result.
            string tmpTableName = createTempTable();
            int hitCount = 0;

            string unionSQL = string.Empty;

            // Combine SQL
            string combineSQL =
                string.Format(@"
                        INSERT INTO {0} (book_id, book_name)
                            SELECT
                              book_id,
                              book_name
                            FROM
                              {1}
                            EXCEPT ALL
                            SELECT
                              book_id,
                              book_name
                            FROM
                              {2}",
                    tmpTableName,
                    leftResult.TableName,
                    rightResult.TableName);

            // Combine.
            hitCount = DatabaseManager.ThreadInstance.Update(combineSQL);

            return
                new SearchFilterResult()
                {
                    HitCount = hitCount,
                    TableName = tmpTableName
                };
        }
    }
}
