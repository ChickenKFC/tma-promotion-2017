using LogicalOperationSearch.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalOperationSearch.Dao
{
    public abstract class AbstractDao
    {
        /// <summary>
        /// Database Manager class.
        /// </summary>
        protected DatabaseManager databaseManager;

        /// <summary>
        /// Constructor
        /// Using object to manage connection to database by thread.
        /// </summary>
        protected AbstractDao()
        {
            databaseManager = DatabaseManager.ThreadInstance;
        }
    }
}
