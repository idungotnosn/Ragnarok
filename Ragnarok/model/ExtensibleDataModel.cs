using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ragnarok.parser;

namespace Ragnarok.model
{
    public class ExtensibleDataModel
    {
        protected Dictionary<String, String> columnStringMappings;
        protected Dictionary<String, decimal> columnDecimalMappings;
        protected Dictionary<String, int> columnIntegerMappings;

        public Dictionary<String, String> ColumnStringMappings
        {
            get
            {
                return this.columnStringMappings;
            }
        }
        public Dictionary<String, decimal> ColumnDecimalMappings
        {
            get
            {
                return this.columnDecimalMappings;
            }
        }
        public Dictionary<String, int> ColumnIntegerMappings
        {
            get
            {
                return this.columnIntegerMappings;
            }
        }

        public ExtensibleDataModel()
        {
            this.columnDecimalMappings = new Dictionary<String, decimal>();
            this.columnStringMappings = new Dictionary<String, String>();
            this.columnIntegerMappings = new Dictionary<String, int>();
        }

        public bool containsColumnName(String columnName){
            return this.ColumnDecimalMappings.ContainsKey(columnName) || this.ColumnIntegerMappings.ContainsKey(columnName) || this.ColumnStringMappings.ContainsKey(columnName);
        }

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, decimal> entry in columnDecimalMappings)
            {
                sb.Append(entry.Key + " = " + entry.Value + "\n");
            }
            return sb.ToString();
        }

        public void putValueByParsingType(String key, String value, ParsingType parsingType)
        {
            if (parsingType == ParsingType.DecimalType)
            {
                this.putDecimalValue(key, Decimal.Parse(value));
            }
            else if (parsingType == ParsingType.IntegerType)
            {
                this.putIntegerValue(key, int.Parse(value));
            }
            else if (parsingType == ParsingType.StringType)
            {
                this.putStringValue(key, value);
            }
        }

        public void putStringValue(String key, String value)
        {
            if(this.columnStringMappings.ContainsKey(key)){
                this.columnStringMappings[key] += " " + value;
            }
            else
            {
                this.columnStringMappings[key] = value;
            }
        }
        public void putDecimalValue(String key, decimal value)
        {
            this.columnDecimalMappings[key] = value;
        }
        public void putIntegerValue(String key, int value)
        {
            this.columnIntegerMappings[key] = value;
        }
        public String getStringValue(String key)
        {
            return this.columnStringMappings[key];
        }
        public int getIntegerValue(String key)
        {
            return this.columnIntegerMappings[key];
        }
        public decimal getDecimalValue(String key)
        {
            return this.columnDecimalMappings[key];
        }
    }
}
