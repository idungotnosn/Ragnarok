using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ragnarok.amazonhttp
{
    class Authentication
    {
        private String accessKeyId;
        public String AccessKeyId
        {
            get { return accessKeyId; }
            set { accessKeyId = value; }
        }
        private String secretAccessKey;
        public String SecretAccessKey
        {
            get { return secretAccessKey; }
            set { secretAccessKey = value; }
        }
        private String merchantId;
        public String MerchantId
        {
            get { return merchantId; }
            set { merchantId = value; }
        }
        private String marketplaceId;
        public String MarketplaceId
        {
            get { return marketplaceId; }
            set { marketplaceId = value; }
        }
        private String serviceUrl;
        public String ServiceUrl
        {
            get { return serviceUrl; }
            set { serviceUrl = value; }
        }
    }
}
