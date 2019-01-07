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
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Asn1.Sec;
using Org.BouncyCastle.Crypto.EC;

namespace ActiveLedgerLib
{
    public static class GenerateKeyPair
    {
        //key size 
        private const int RsaKeySize = 1024;


        //Method to generate RSA Key Pair

        #region RSA KeyPair Generation Method


        public static AsymmetricCipherKeyPair GetKeyPair(string type)
        {

            //bouncy Castle librray Method to generate RSA Key Pair
            if (type == "RSA")

            {

                SecureRandom secureRandom = new SecureRandom();
                var keyGenerationParameters = new KeyGenerationParameters(secureRandom, RsaKeySize);

                var keyPairGenerator = new RsaKeyPairGenerator();
                keyPairGenerator.Init(keyGenerationParameters);
                //returning keys
                AsymmetricCipherKeyPair keypair = keyPairGenerator.GenerateKeyPair();
              
                return keypair;
            }
            else
            {

                SecureRandom secureRandom = new SecureRandom();
                ECKeyPairGenerator gen = new ECKeyPairGenerator("ECDSA");
                //selecting the Secp256k1 curve
                DerObjectIdentifier oid = SecObjectIdentifiers.SecP256k1;
                X9ECParameters ecPs = CustomNamedCurves.GetByOid(oid);
                ECDomainParameters ec = new ECDomainParameters(
                     ecPs.Curve, ecPs.G, ecPs.N, ecPs.H, ecPs.GetSeed());
                ECKeyGenerationParameters ecP = new ECKeyGenerationParameters(ec, secureRandom);
                gen.Init(ecP);
                return gen.GenerateKeyPair();

            }


        }

        #endregion RSA KeyPair Generation Method

        //get public private keys from AsymmetricCipherKeyPair return by GetKeyPair
        #region getting keys
        //RSA Public Key
        public static RsaKeyParameters getRSAPublicKey(AsymmetricCipherKeyPair keyPair)
        {

            var pubKey = (RsaKeyParameters)(keyPair.Public);
            return pubKey;

        }
        //RSA Private Key
        public static RsaKeyParameters getRSAPrivateKey(AsymmetricCipherKeyPair keyPair)
        {
            var priKey = (RsaKeyParameters)(keyPair.Private);
            return priKey;

        }
        //EC Public Key
        public static ECKeyParameters getECPublicKey(AsymmetricCipherKeyPair keyPair)
        {

            var pubKey = (ECKeyParameters)(keyPair.Public);
            return pubKey;

        }
        //EC Private Key
        public static ECKeyParameters getECPrivateKey(AsymmetricCipherKeyPair keyPair)
        {
            var priKey = (ECKeyParameters)(keyPair.Private);
            return priKey;

        }

        #endregion getting keys

    }
}
