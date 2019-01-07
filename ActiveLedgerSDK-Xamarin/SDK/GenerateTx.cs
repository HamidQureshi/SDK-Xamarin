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
