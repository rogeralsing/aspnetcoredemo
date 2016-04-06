using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AspNetCoreService.Services
{
    public class CouchDb
    {
        public CouchDb(string uri, string dbName)
        {
            Uri = uri;
            DbName = dbName;
        }

        public string Uri { get;}
        public string DbName { get; }

        public string GetDocumentUri(string id)
        {
            return Uri + "/" + DbName + "/" + id;
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