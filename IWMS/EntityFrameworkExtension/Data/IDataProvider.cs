using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace EntityFrameworkExtension.Data
{
    public partial interface IDataProvider
    {
        void InitializeDatabase();
        DbParameter GetParameter();

        /// <summary>
        /// Gets a value indicating whether this data provider supports backup
        /// </summary>
        bool BackupSupported { get; }

        /// <summary>
        /// Gets a maximum length of the data for HASHBYTES functions, returns 0 if HASHBYTES function is not supported
        /// </summary>
        int SupportedLengthOfBinaryHash { get; }
    }
}
