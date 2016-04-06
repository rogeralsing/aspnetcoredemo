using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AspNetCoreService.Services
{
    public class CouchDb
    {
        public CouchDb(string uri, string dbName)
        {
            _uri = uri;
            _dbName = dbName;
        }

        private readonly string _uri;
        private readonly string _dbName;

        public string GetDocumentUri(string id)
        {
            return $"{_uri}/{_dbName}/{id}";
        }

        public async Task<dynamic> GetDocument(string id)
        {
            using (var client = new HttpClient())
            {
                var res = await client.GetAsync(GetDocumentUri(id));
                var body = await res.Content.ReadAsStringAsync();
                dynamic document = JsonConvert.DeserializeObject(body);
                return document;
            }
        }

        public async Task PutDocument(string name, dynamic document)
        {
            using (var client = new HttpClient())
            {
                var current = await GetDocument(name);
                document._rev = current._rev;
                var json = JsonConvert.SerializeObject(document);
                var content = new StringContent(json);
                await client.PutAsync(GetDocumentUri(name), content);
            }
        }
    }
}