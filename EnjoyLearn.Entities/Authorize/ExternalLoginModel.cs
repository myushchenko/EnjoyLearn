using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnjoyLearn.Models.Authorize
{
    public class ExternalLoginModel
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
        public IDictionary<string, object> ExtraData { get; set; }
    }
}
