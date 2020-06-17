using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleMassMailing.Data
{
    public class DataRow
    {
        public DataRow(string row)
        {
            row = row.Trim();
            if (row.StartsWith("#"))
            {
                IsDisabled = true;
                row = row.Substring(1);
            }

            var items = row.Split(';');

            // Email = First element
            EMail = items[0].Trim();

            // Parameters
            Parameters = new Dictionary<string, string>();
            foreach (var item in items.Skip(1))
            {
                var keyValue = item.Split('=');
                Parameters.Add(keyValue[0].Trim(), keyValue[1].Trim());
            }
        }

        public string EMail { get; private set; }
        public Dictionary<string, string> Parameters { get; private set; }
        public bool IsDisabled { get; private set; }
        public bool IsEnabled => !IsDisabled;
    }
}
