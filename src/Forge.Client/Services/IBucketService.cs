using System.Collections.Generic;
using System.Threading.Tasks;
using Forge.Client.Models;

namespace Forge.Client.Services
{
    public interface IBucketService
    {
        Task<List<Bucket>> GetBuckets();
        Task<Bucket> CreateBucket(string bucketKey);
        Task DeleteBucket(string bucketKey);
        Task DeleteBucketObject(string bucketKey, string objectName);
    }
}