using System;
using System.Collections.Generic;
using System.Text;

namespace Forge.Client.Models
{
    public class ForgeClientArgs
    {
        public string BaseUrl { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Scope { get; set; }
    }
}
