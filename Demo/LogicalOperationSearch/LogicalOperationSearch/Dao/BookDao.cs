using LogicalOperationSearch.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalOperationSearch.Dao
{
    public class BookDao : AbstractDao
    {
        public List<BookEntity> GetBookFromSearchResult(string targetTable)
        {
            return databaseManager.Select<BookEntity>(
                string.Format(@"
                  SELECT
                    bks.book_id,
                    temp.book_name,
                    bks.author,
                    bks.pulish_date,
                    bks.book_shelf,
                    sd.sort_description
                  FROM
                    books bks
                    INNER JOIN {0} temp
                      ON temp.book_id = bks.book_id
                    INNER JOIN sort_description sd
                      ON bks.book_id = sd.book_id", targetTable));
        }
    }
}
