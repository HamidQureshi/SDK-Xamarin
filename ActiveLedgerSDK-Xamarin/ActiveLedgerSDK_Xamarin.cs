using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ActiveLedgerSDK_Xamarin
{
    public class ActiveLedgerSDK_Xamarin : ContentPage
    {
        public ActiveLedgerSDK_Xamarin()
        {
          
        }

        public void generateAndOnboardKeys()
        {
            String KeyType = "EC";


            ActiveLedgerLib.SDKPreferences.setConnection("http", "testnet-uk.activeledger.io", "5260");


            AsymmetricCipherKeyPair keypair = ActiveLedgerLib.GenerateKeyPair.GetKeyPair(KeyType);

            ActiveLedgerLib.Helper.SaveKeyToFile(ActiveLedgerLib.Helper.GetPrivateKey(keypair), "privatekey.pem");
            ActiveLedgerLib.Helper.SaveKeyToFile(ActiveLedgerLib.Helper.GetPublicKey(keypair), "pkey.pem");


            JObject json = ActiveLedgerLib.GenerateTxJson.GetTxJsonForOnboardingKeys(keypair, KeyType);

            string json_str = ActiveLedgerLib.Helper.ConvertJsonToString(json);

            var response = ActiveLedgerLib.MakeRequest.makeRequestAsync(ActiveLedgerLib.SDKPreferences.url, json_str);


            response.Wait();

            if (response.Result.Content != null)
            {
                //reading response from Active Ledger
                var responseContent = response.Result.Content.ReadAsStringAsync();

                //writing response on the text box                  
                System.Diagnostics.Debug.WriteLine("--Response--" + responseContent.Result);

            }


        }

    }
}
