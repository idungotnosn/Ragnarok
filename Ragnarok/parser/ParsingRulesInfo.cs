using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ragnarok.parser
{
    class ParsingRulesInfo
    {
        Dictionary<String, ParsingRule> parsingRules;
        private String identifierColumn;
        public String IdentifierColumn
        {
            get
            {
                return identifierColumn;
            }
        }

        public ParsingRulesInfo()
        {
            this.parsingRules = new Dictionary<String, ParsingRule>();
            identifierColumn = null;
        }

        public void addParsingRule(ParsingRule parsingRule)
        {
            if (parsingRule.IsIdentifier)
            {
                identifierColumn = parsingRule.AmazonColumnName;
            }
            this.parsingRules[parsingRule.AmazonColumnName] = parsingRule;
        }

        public ParsingRule getParsingRule(String key)
        {
            return this.parsingRules[key];
        }

        public bool columnNameHasRule(String columnName)
        {
            return this.parsingRules.ContainsKey(columnName);
        }

        public bool isColumnNameForOrderItem(String columnName){
            return this.parsingRules[columnName].IsOrderItem;
        }



    }
}
