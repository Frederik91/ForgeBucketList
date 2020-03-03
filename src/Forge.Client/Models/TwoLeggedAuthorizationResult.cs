using System;
using System.Collections.Generic;
using System.Text;

namespace Forge.Client.Models
{
    public class TwoLeggedAuthorizationResult
    {
        public string AccessToken { get; set; }
        public string TokenType { get; set; }
        public int ExpiresIn { get; set; }
    }
}
