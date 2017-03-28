using LogicalOperationSearch.Data;
using LogicalOperationSearch.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalOperationSearch.Dao
{
    public class HistorySearchDao: AbstractDao
    {
        /// <summary>
        /// Register history search into database.
        /// </summary>
        /// <param name="searchCondition"></param>
        public void RegisterSearchHistory(SearchConditionInformation searchCondition)
        {
            string insertSQL = string.Empty;

            if (searchCondition.FromDate == null
                || searchCondition.ToDate == null)
            {
                insertSQL = string.Format(@"
                    INSERT INTO search_history
                      (
                        search_target_table,
                        search_target_column,
                        search_condition,
                        delete_flag
                      )
                      VALUES
                      (
                        {0}, {1}, '{2}', FALSE
                      )", 
                      (int)searchCondition.SearchTarget,
                      (int)searchCondition.SearchType,
                      searchCondition.TextCondition);
            }
            else
            {
                insertSQL = string.Format(@"
                    INSERT INTO search_history
                      (
                        search_target_table,
                        search_target_column,
                        search_condition,
                        from_date,
                        to_date,
                        delete_flag
                      )
                      VALUES
                      (
                        {0}, {1}, '{2}', '{3}'::date, '{4}'::date, FALSE
                      )
                    ",
                    (int)searchCondition.SearchTarget,
                    (int)searchCondition.SearchType,
                    searchCondition.TextCondition,
                    searchCondition.FromDate,
                    searchCondition.ToDate);
            }

            databaseManager.Update(insertSQL);
        }

        /// <summary>
        /// Get history search.
        /// </summary>
        /// <returns></returns>
        public List<HistorySearchEntity> GetHistorySearch ()
        {
            return databaseManager.Select<HistorySearchEntity>(
                @"SELECT
                    *
                  FROM
                    search_history
                  WHERE
                    delete_flag = FALSE");
        }

        /// <summary>
        /// Delete specific hsitory
        /// </summary>
        /// <param name="historySearchId"></param>
        public void DeleteHistorySearch (int historySearchId)
        {
            databaseManager.Update(
                string.Format(@"UPDATE
                    search_history
                  SET 
                    delete_flag = TRUE
                  WHERE
                    search_history_id = {0}",
                historySearchId));
        }
    }
}
