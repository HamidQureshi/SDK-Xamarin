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
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace ActiveLedgerLib
{
    public static class GenerateSignature
    {
        //Bouncy castle method to generate RSA signature using SHA256WithRSA algorithm
        #region GetSignatureRSA Method

       internal static byte[] GetSignatureRSA(byte[] plainText, RsaKeyParameters privateKey)
        {

            var signer = SignerUtilities.GetSigner("SHA256WithRSA");
            signer.Init(true, privateKey);
            signer.BlockUpdate(plainText, 0, plainText.Length);
            //returning generated signature
            return signer.GenerateSignature();
        }
        #endregion GetSignatureRSA Method

        //Bouncy castle method to generate EC signature using SHA256WithECDSA algorithm
        #region GetSignatureEC Method

       public static byte[] GetSignatureEC(byte[] plainText, ECKeyParameters privateKey)
        {

            var signer = SignerUtilities.GetSigner("SHA256WithECDSA");
            signer.Init(true, privateKey);
            signer.BlockUpdate(plainText, 0, plainText.Length);
            //returning generated signature
            return signer.GenerateSignature();
        }

        #endregion GetSignatureEC Method
    }
}
