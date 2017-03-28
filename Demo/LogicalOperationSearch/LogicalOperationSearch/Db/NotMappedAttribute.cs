using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalOperationSearch.Db
{
    /// <summary>
    /// カラムマッピングの対象外であることを表明する属性です。
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NotMappedAttribute: Attribute
    {
    }
}
