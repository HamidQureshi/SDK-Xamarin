
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Crypto;

namespace ActiveLedgerLib
{
   public class GenerateTx
    {
        //genarating Transaction to pass active ledger

        #region generateTx Methods
       internal static JObject GetTxForBoarding(AsymmetricCipherKeyPair keypair, string keyType)
        {
            //declaring json objects
            JObject tx = new JObject();
            JObject i = new JObject();
            JObject identity = new JObject();
            //reading public key from File
            string publicKeyText = ActiveLedgerLib.Helper.GetPublicKey(keypair);
            //adding values in json object
            tx.Add("$contract", "onboard");
            tx.Add("$namespace", "default");
            tx.Add("$i", i);
            i.Add("identity", identity);
            identity.Add("publicKey", publicKeyText);
            if (keyType == "RSA")
            {
                identity.Add("type", "rsa");

            }
            else
            {
                identity.Add("type", "secp256k1");
            }

            return tx;


        }
        //Base Transaction tx Method
        public static JObject GetBasicTx(string nameSpace, string contract, string entry, JObject i, JObject r, JObject o)
        {
            JObject tx = new JObject();

            if (nameSpace != null)
            {
                tx.Add("$namespace", nameSpace);
            }
            if (contract != null)
            {
                tx.Add("$contract", contract);

            }
            if (entry != null)
            {
                tx.Add("$entry", entry);
            }

            if (i != null)
            {
                tx.Add("$i", i);
            }
            if (r != null)
            {
                tx.Add("$r", r);
            }
            if (o != null)
            {
                tx.Add("$o", o);
            }

            return tx;

        }
        #endregion generateTx Methods
    }
}
