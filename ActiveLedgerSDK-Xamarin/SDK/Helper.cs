
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.OpenSsl;
using PCLStorage;
using System;
using System.IO;
using Xamarin.Forms;
namespace ActiveLedgerLib
{
    public static class Helper
    {

        // extract public key from keypair and returns as a string
        public static String GetPublicKey(AsymmetricCipherKeyPair keys)
        {
            TextWriter textWriter = new StringWriter();
            PemWriter pemWriter = new PemWriter(textWriter);
            pemWriter.WriteObject(keys.Public);
            pemWriter.Writer.Flush();
            return textWriter.ToString();

        }
        
        // extract private key from keypair and returns as a string
        public static String GetPrivateKey(AsymmetricCipherKeyPair keys)
        {
            TextWriter textWriter = new StringWriter();
            PemWriter pemWriter = new PemWriter(textWriter);
            pemWriter.WriteObject(keys.Private);
            pemWriter.Writer.Flush();
            return textWriter.ToString();

        }

        public static async void SaveKeyToFile(String key, String fileName)
        {
            try
            {
                IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFile file = await rootFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);

            System.Diagnostics.Debug.WriteLine("----LocalStorage: (" + rootFolder.Path + ")");
            System.Diagnostics.Debug.WriteLine("----Saved: (" + file.Path + ")");

            await file.WriteAllTextAsync(key);
        }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("----FileWritingExcep: (" + ex.Message + ")");
            }

}


        //json to string Converter
        public static string ConvertJsonToString(JObject json)
        {
            string jsonStr = JsonConvert.SerializeObject(json);
            return jsonStr;

        }
        //Covert String to byte Array
        public static byte[] ConvertStringToByteArray(string str)
        {
            // Create a UnicodeEncoder to convert between byte array and string.
           // ASCIIEncoding ByteConverter = new ASCIIEncoding();
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(str); //ByteConverter.GetBytes(str);
            return byteArray;

        }
        //convert  byte Array to base 64 string
        public static string ConvertByteArrayToBase64String(byte[] byteArray)
        {
            string str = Convert.ToBase64String(byteArray);
            return str;

        }



    }
}
