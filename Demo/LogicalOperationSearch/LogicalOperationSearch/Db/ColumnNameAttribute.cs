using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalOperationSearch.Db
{
    public class ColumnNameAttribute : Attribute
    {
        private string _name;
        /// <summary>
        /// カラム名です。
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
        }

        /// <summary>
        /// コンストラクタです。
        /// </summary>
        /// <param name="name">カラム名</param>
        public ColumnNameAttribute(string name)
        {
            _name = name;
        }
    }
}
