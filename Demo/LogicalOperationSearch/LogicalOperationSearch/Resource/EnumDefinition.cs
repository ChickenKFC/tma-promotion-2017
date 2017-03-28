using System;
using System.Runtime.Serialization;

namespace LogicalOperationSearch.Resource
{
    /// <summary>
    /// Search type
    /// </summary>
    public enum SearchTypes
    {
        /// <summary>
        /// Book name
        /// </summary>
        BookName = 0,

        /// <summary>
        /// Author name
        /// </summary>
        Author = 1,

        /// <summary>
        /// Release date
        /// </summary>
        PulishDate = 2,

        /// <summary>
        /// Barcode
        /// </summary>
        Barcode = 3
    }

    /// <summary>
    /// Search target
    /// </summary>
    public enum SearchTargets
    {
        /// <summary>
        /// Math table
        /// </summary>
        Mathematics = 0,

        /// <summary>
        /// Physical table
        /// </summary>
        Physical = 1,

        /// <summary>
        /// Sciencetifics table
        /// </summary>
        Sciencetifics = 2,

        /// <summary>
        /// Barcode table
        /// </summary>
        Barcode = 3
    }

    public enum LogicalOperator
    {
        /// <summary>
        /// AND operator
        /// </summary>
        AND = 0,

        /// <summary>
        /// OR operator
        /// </summary>
        OR = 1,

        /// <summary>
        /// NOT operator
        /// </summary>
        NOT = 2,

        /// <summary>
        /// Not set
        /// </summary>
        NONE = 3
    }
}
