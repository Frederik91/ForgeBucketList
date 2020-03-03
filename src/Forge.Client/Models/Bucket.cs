using System;
using System.Collections.Generic;
using System.Text;

namespace Forge.Client.Models
{
    public class Bucket
    {
        public string BucketKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public string PolicyKey { get; set; }
    }
}
