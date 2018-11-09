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
