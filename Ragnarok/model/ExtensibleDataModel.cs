using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ragnarok.parser;

namespace Ragnarok.model
{
    class ExtensibleDataModel
    {
        protected Dictionary<String, String> columnStringMappings;
        protected Dictionary<String, decimal> columnDecimalMappings;
        protected Dictionary<String, int> columnIntegerMappings;

        public ExtensibleDataModel()
        {
            this.columnDecimalMappings = new Dictionary<String, decimal>();
            this.columnStringMappings = new Dictionary<String, String>();
            this.columnIntegerMappings = new Dictionary<String, int>();
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
            this.columnStringMappings.Add(key, value);
        }
        public void putDecimalValue(String key, decimal value)
        {
            this.columnDecimalMappings.Add(key, value);
        }
        public void putIntegerValue(String key, int value)
        {
            this.columnIntegerMappings.Add(key, value);
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
