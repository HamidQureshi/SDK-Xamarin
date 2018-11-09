using Newtonsoft.Json.Linq;
using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto;

namespace ActiveLedgerLib
{
    public static class GenerateTxJson
    {

        //creating  json structure to hit Active Ledger
        #region Json structure for onBoarding keys

        public static JObject GetTxJsonForOnboardingKeys( AsymmetricCipherKeyPair keypair, string keyType)
        {

            JObject json = new JObject();
            JObject sigsIdentity = new JObject();
            JObject tx = GenerateTx.GetTxForBoarding(keypair ,keyType);
            json.Add("$selfsign", true);
            string tx_str = Helper.ConvertJsonToString(tx);
            //converting transaction in to byte Array
            byte[] originalData = Helper.ConvertStringToByteArray(tx_str);
            //signing the transaction
            if (keyType == "RSA")
            {
                RsaKeyParameters priKey = (RsaKeyParameters)keypair.Private;
                byte[] signedData = GenerateSignature.GetSignatureRSA(originalData, priKey);
                sigsIdentity.Add("identity", Helper.ConvertByteArrayToBase64String(signedData));
            }
            else
            {
                ECKeyParameters priECKey = (ECKeyParameters)keypair.Private;
                byte[] signedData = GenerateSignature.GetSignatureEC(originalData, priECKey);
                sigsIdentity.Add("identity", Helper.ConvertByteArrayToBase64String(signedData));
            }
            
            json.Add("$sigs", sigsIdentity);
            json.Add("$tx", tx);
            return json;
        }
        #endregion Json Structure for onBoarding keys

   

        //Base Transaction Json
        public static JObject GetBasicTxJson(JObject tx, Nullable<bool> selfSign, string sigs)
        {
            JObject transaction = new JObject();
            transaction.Add("$tx", tx);
            if (selfSign != null)
            {
                transaction.Add("$selfsign", selfSign);
            }
            if (sigs != null)
            {
                transaction.Add("$sigs", sigs);
            }

            return transaction;

        }
    }
}
