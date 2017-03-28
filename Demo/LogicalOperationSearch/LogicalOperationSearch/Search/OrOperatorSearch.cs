using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicalOperationSearch.Data;
using LogicalOperationSearch.Db;

namespace LogicalOperationSearch.Search
{
    public class OrOperatorSearch : AbstractOperatorSearchFilter
    {
        /// <summary>
        /// OR Operator's constructor
        /// </summary>
        /// <param name="param"></param>
        public OrOperatorSearch(List<AbstractSearchFilter> param)
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
            SearchFilterResult operatorResult = new SearchFilterResult();

            // Loop on each SearchCondition and Search.
            foreach (AbstractSearchFilter filter in SearchParams)
            {
                SearchFilterResult tempResult = filter.Search(sourceData);

                if (tempResult.HitCount > 0)
                {
                    // Combine with previous search result.
                    operatorResult = CombineResult(operatorResult, tempResult);
                }
            }

            return operatorResult;
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

            // Proceed to combine.
            string unionSQL = string.Empty;

            if (leftResult.HitCount > 0)
            {
                // Create UNION SQL.
                unionSQL = string.Format(@"
                                UNION
                                  SELECT
                                    book_id,
                                    book_name
                                  FROM
                                    {0}", leftResult.TableName);
            }

            // Combine SQL
            string combineSQL =
                string.Format(@"
                        INSERT INTO {0} (book_id, book_name)
                            SELECT
                              book_id,
                              book_name
                            FROM
                              {1}
                            {2}",
                    tmpTableName,
                    rightResult.TableName,
                    unionSQL);

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
