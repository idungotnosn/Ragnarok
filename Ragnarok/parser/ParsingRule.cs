using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ragnarok.parser
{
    class ParsingRule
    {
        private String amazonColumnName;
        public string AmazonColumnName    // the Name property
        {
            get
            {
                return amazonColumnName;
            }
        }

        private ParsingType parsingType;
        public ParsingType ParsingType    // the Name property
        {
            get
            {
                return parsingType;
            }
        }

        private bool isOrderItem;
        public bool IsOrderItem    // the Name property
        {
            get
            {
                return isOrderItem;
            }
        }

        private bool isIdentifier;
        public bool IsIdentifier
        {
            get
            {
                return isIdentifier;
            }
        }

        private const String STRING_TYPE = "string";
        private const String DECIMAL_TYPE = "decimal";
        private const String INTEGER_TYPE = "integer";

        public ParsingRule(String amazonColumnName, String type, bool isOrderItem, bool isIdentifier)
        {
            this.amazonColumnName = amazonColumnName;
            this.isIdentifier = isIdentifier;
            this.isOrderItem = isOrderItem;
            if (type.Equals(STRING_TYPE))
            {
                this.parsingType = ParsingType.StringType;
            }
            else if (type.Equals(DECIMAL_TYPE))
            {
                this.parsingType = ParsingType.DecimalType;
            }
            else if (type.Equals(INTEGER_TYPE))
            {
                this.parsingType = ParsingType.IntegerType;
            }
        }

    }
}
