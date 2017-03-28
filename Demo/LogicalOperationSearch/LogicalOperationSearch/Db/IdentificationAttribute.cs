using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalOperationSearch.Db
{
    /// <summary>
    /// ID であることを表明する属性です。
    /// </summary>
    [AttributeUsage(AttributeTargets.Struct)]
    public class IdentificationAttribute : Attribute
    {
        /// <summary>
        /// IDの基になる型です。
        /// </summary>
        private readonly Type underlyingType;

        /// <summary>
        /// IDの基になる型を取得します。
        /// </summary>
        public Type UnderlyingType
        {
            get
            {
                return underlyingType;
            }
        }

        /// <summary>
        /// この属性を構築します。
        /// </summary>
        /// <param name="underlyingType">IDの基になる型</param>
        public IdentificationAttribute(Type underlyingType)
        {
            this.underlyingType = underlyingType;
        }
    }
}
