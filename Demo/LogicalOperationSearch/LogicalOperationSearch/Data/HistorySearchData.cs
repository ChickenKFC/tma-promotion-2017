using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalOperationSearch.Data
{
    /// <summary>
    /// Search history
    /// </summary>
    public class HistorySearchData : VOBase
    {
        private int HistoryIdField;
        /// <summary>
        /// History ID Field
        /// </summary>
        public int HistoryId
        {
            get { return HistoryIdField; }
            set
            {
                if (HistoryIdField != value)
                {
                    HistoryIdField = value;
                    OnPropertyChanged("HistoryId");
                }
            }
        }

        private int SearchTypeField;
        /// <summary>
        /// Search type.
        /// 1. BOOK NAME
        /// 2. AUTHOR
        /// 3. PULISH DATE
        /// 4. BAR CODE
        /// </summary>
        public int SearchType
        {
            get { return SearchTypeField; }
            set
            {
                if (SearchTypeField != value)
                {
                    SearchTypeField = value;
                    OnPropertyChanged("SearchType");
                }
            }
        }

        private int SearchTargetField;
        /// <summary>
        /// Search target.
        /// </summary>
        public int SearchTarget
        {
            get { return SearchTargetField; }
            set
            {
                if (SearchTargetField != value)
                {
                    SearchTargetField = value;
                    OnPropertyChanged("SearchTarget");
                }
            }
        }

        private string TextSearchConditionField = string.Empty;
        /// <summary>
        /// TextSearch condition that using for search type (1,2,4)
        /// </summary>
        public string TextSearchCondition
        {
            get { return TextSearchConditionField; }
            set
            {
                if (TextSearchConditionField != value)
                {
                    TextSearchConditionField = value;
                    OnPropertyChanged("TextSearchCondition");
                }
            }
        }

        private DateTime? FromDateField;
        /// <summary>
        /// Datetime search condition
        /// </summary>
        public DateTime? FromDate
        {
            get { return FromDateField; }
            set
            {
                if (FromDateField != value)
                {
                    FromDateField = value;
                    OnPropertyChanged("FromDate");
                }
            }
        }

        private DateTime? ToDateField;
        /// <summary>
        /// Datetime search condition
        /// </summary>
        public DateTime? ToDate
        {
            get { return ToDateField; }
            set
            {
                if (ToDateField != value)
                {
                    ToDateField = value;
                    OnPropertyChanged("ToDate");
                }
            }
        }
    }
}
