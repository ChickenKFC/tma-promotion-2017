using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalOperationSearch.Entity
{
    public class BookEntity
    {
        /// <summary>
        /// Book ID
        /// </summary>
        public int BookId { get; set; }

        /// <summary>
        /// Book's Name
        /// </summary>
        public string BookName { get; set; }

        /// <summary>
        /// Book's Author
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Book's Pulish Date
        /// </summary>
        public DateTime PulishDate { get; set; }

        /// <summary>
        /// Short description
        /// </summary>
        public string SortDescription { get; set; }

        /// <summary>
        /// Book shelf
        /// </summary>
        public int BookShelf { get; set; }
    }
}
