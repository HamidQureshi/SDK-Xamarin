/*
 * MIT License (MIT)
 * Copyright (c) 2018
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */
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
        public static JObject GetBasicTxJson(String territoriality, JObject tx, Nullable<bool> selfSign, string sigs)
        {
            JObject transaction = new JObject();
            if(territoriality != null)
            {
                transaction.Add("$territoriality", territoriality);
            }

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
