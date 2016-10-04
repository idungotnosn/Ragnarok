using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ragnarok.model;
using Ragnarok.parser;
using System.IO;
using System.Xml;


namespace Ragnarok
{
    class AmazonReportParser
    {
        public static ICollection<AmazonOrder> parseOrderListFromReport(Stream reportStream)
        {
            ParsingRulesInfo parseRules = getParsingRulesInfo();
            using (StreamReader reader = new StreamReader(reportStream))
            {
                if (reader.Peek() >= 0) { 
                    String[] headerNames =  reader.ReadLine().Split('\t');
                    return getAmazonOrdersFromStream(parseRules, headerNames, reader);
                }
            }
            return new List<AmazonOrder>();
        }

        private static ICollection<AmazonOrder> getAmazonOrdersFromStream(ParsingRulesInfo parsingRules, String[] headerNames, StreamReader reader)
        {
            Dictionary<String, AmazonOrder> amazonOrderDictionary = new Dictionary<String, AmazonOrder>();
            int identifierIndex = findIndexOfIdentifier(parsingRules, headerNames);
            while (reader.Peek() >= 0)
            {
                String[] rowValues = reader.ReadLine().Split('\t');
                if (rowValues.Length != headerNames.Length)
                {
                    Console.WriteLine("Row values did not have same length as header values!");
                    Console.ReadLine();
                }

                String rowIdentifier = rowValues[identifierIndex];
                AmazonOrder currentOrder;
                if (!amazonOrderDictionary.ContainsKey(rowIdentifier))
                {
                    currentOrder = new AmazonOrder();
                    for (int i = 0; i < headerNames.Length; i++)
                    {
                        String headerName = headerNames[i];
                        if (!parsingRules.columnNameHasRule(headerName))
                        {
                            continue;
                        }
                        if (!parsingRules.isColumnNameForOrderItem(headerName))
                        {
                            ParsingRule parsingRule = parsingRules.getParsingRule(headerName);
                            currentOrder.putValueByParsingType(headerName, rowValues[i], parsingRules.getParsingRule(headerName).ParsingType);
                        }
                    }
                    amazonOrderDictionary.Add(rowIdentifier, currentOrder);
                }
                else
                {
                    currentOrder = amazonOrderDictionary[rowIdentifier];
                }
                AmazonOrderItem orderItem = new AmazonOrderItem();
                for (int i = 0; i < headerNames.Length; i++) {
                    String headerName = headerNames[i];
                    if (!parsingRules.columnNameHasRule(headerName))
                    {
                        continue;
                    }
                    if (parsingRules.isColumnNameForOrderItem(headerName))
                    {
                        orderItem.putValueByParsingType(headerName, rowValues[i], parsingRules.getParsingRule(headerName).ParsingType);
                    }
                }
                currentOrder.addOrderItem(orderItem);
            }
            return amazonOrderDictionary.Values;
        }

        private static ParsingRulesInfo getParsingRulesInfo()
        {
            ParsingRulesInfo result = new ParsingRulesInfo();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(File.ReadAllText("config/AmazonColumnMappings.xml"));
            foreach (XmlNode row in xmlDoc.SelectNodes("//amazon-column"))
            {
                String columnName = row.SelectSingleNode("amazon-column-name").InnerText;
                String orderItemSpecific = row.SelectSingleNode("order-item-specific").InnerText;
                String identifier = row.SelectSingleNode("identifier").InnerText;
                String type = row.SelectSingleNode("type").InnerText;
                ParsingRule parsingRule = new ParsingRule(columnName, type, orderItemSpecific.Equals("true"), identifier.Equals("true"));
                result.addParsingRule(parsingRule);
            }
            return result;
        }

        private static int findIndexOfIdentifier(ParsingRulesInfo rulesInfo, String[] headerNames)
        {
            for (int i = 0; i < headerNames.Length; i++)
            {
                if (rulesInfo.IdentifierColumn.Equals(headerNames[i]))
                {
                    return i;
                }
            }
            return -1;
        }

    }
}
