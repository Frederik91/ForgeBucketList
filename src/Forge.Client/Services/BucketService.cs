using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Forge.Client.Models;
using Newtonsoft.Json;
using RestSharp;

namespace Forge.Client.Services
{
    public class BucketService : IBucketService
    {
        private readonly IClientWrapper _clientWrapper;

        public BucketService(IClientWrapper clientWrapper)
        {
            _clientWrapper = clientWrapper;
        }

        public async Task<List<Bucket>> GetBuckets()
        {
            var request = new RestRequest("/oss/v2/buckets?region=EMEA", Method.GET);
            var result = await _clientWrapper.Execute<BucketListResult>(request);
            return result?.Items ?? new List<Bucket>();
        }

        public async Task<Bucket> CreateBucket(string bucketKey)
        {
            var args = new {
                bucketKey,
                policyKey = "persistent"
            };
            var json = JsonConvert.SerializeObject(args);
            var request = new RestRequest("/oss/v2/buckets", Method.POST);
            request.AddParameter("application/json", json, ParameterType.RequestBody);

            var bucket = await _clientWrapper.Execute<Bucket>(request);
            return bucket;
        }

        public Task DeleteBucket(string bucketKey)
        {
            var request = new RestRequest($"/oss/v2/buckets/{bucketKey}", Method.DELETE);
            return _clientWrapper.Execute(request);
        }

        public Task DeleteBucketObject(string bucketKey, string objectName)
        {
            var request = new RestRequest($"/oss/v2/buckets/{bucketKey}/objects/{objectName}", Method.DELETE);
            return _clientWrapper.Execute(request);
        }
    }
}
