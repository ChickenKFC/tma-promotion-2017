using LogicalOperationSearch.Dao;
using LogicalOperationSearch.Data;
using LogicalOperationSearch.Db;
using LogicalOperationSearch.Entity;
using LogicalOperationSearch.Resource;
using LogicalOperationSearch.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalOperationSearch.Handler
{
    public class SearchHandler
    {
        /// <summary>
        /// Search execute
        /// </summary>
        /// <param name="searchCondition">Search information</param>
        /// <returns>Search result</returns>
        public List<BookEntity> SearchBook(SearchConditionInformation searchCondition)
        {
            List<BookEntity> bookInfos = new List<BookEntity>();
            List<AbstractSearchFilter> filters = new List<AbstractSearchFilter>();

            // Dertermine search type.
            // dertermine search column
            filters.Add(CreateSearchFilter(searchCondition));

            SearchFilterResult result = new SearchFilterResult();

            // Search
            if (filters.Count > 0)
            {
                result = filters[0].Search(new SearchFilterResult());
            }

            // Join and get full book info.
            if (result.HitCount != 0)
            {
                bookInfos = new BookDao().GetBookFromSearchResult(result.TableName);
            }

            // Register search history.
            new HistorySearchDao().RegisterSearchHistory(searchCondition);

            // Database commit
            DatabaseManager.ThreadInstance.Commit();
            DatabaseManager.ThreadInstance.BeginTransaction();

            return bookInfos;
        }

        /// <summary>
        /// Create search condition from search info.
        /// </summary>
        /// <param name="searchCondition"></param>
        /// <returns></returns>
        public static AbstractSearchFilter CreateSearchFilter(SearchConditionInformation searchCondition)
        {
            // Dertermine search type.
            // dertermine search column
            switch (searchCondition.SearchType)
            {
                case SearchTypes.Author:
                    return new AuthorTextSearch(searchCondition.SearchTarget, searchCondition.TextCondition);

                case SearchTypes.BookName:
                    return new BookNameTextSearch(searchCondition.SearchTarget, searchCondition.TextCondition);

                case SearchTypes.Barcode:
                    return new BarCodeTextSearch(searchCondition.SearchTarget, searchCondition.TextCondition);

                case SearchTypes.PulishDate:
                    return new PulishDateSearchFilter(searchCondition.SearchTarget, searchCondition.FromDate ?? DateTime.Now, searchCondition.ToDate ?? DateTime.Now);

                default:
                    throw new Exception("Search type is not valid.");
            }
        }
    }
}
