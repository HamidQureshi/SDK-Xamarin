using System.Text;
using System.Threading.Tasks;
using System.Net.Http;


namespace ActiveLedgerLib
{
    public static class MakeRequest
    {
        public static HttpResponseMessage Response;

        //posting the Async resquest to active Ledger
        #region makeRequestAsync Method
        public static async Task<HttpResponseMessage> makeRequestAsync(string endpoint, string json)
        {


            using (var client = new HttpClient())
            {
                 // var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(json));
               var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                //getting response 
            
               Response = await client.PostAsync(endpoint,httpContent).ConfigureAwait(false);


            };

            return Response;

        }

 

        #endregion makeRequestAsync Method
    }
}

