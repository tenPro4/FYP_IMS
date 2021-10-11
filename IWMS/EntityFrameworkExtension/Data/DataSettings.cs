using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkExtension.Data
{
    public partial class DataSettings
    {
        public DataSettings()
        {
            RawDataSettings = new Dictionary<string, string>();
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public DataProviderType DataProvider { get; set; }

        public string DataConnectionString { get; set; }

        public IDictionary<string, string> RawDataSettings { get; }

        [JsonIgnore]
        public bool IsValid => DataProvider != DataProviderType.Unknown && !string.IsNullOrEmpty(DataConnectionString);

    }
}
