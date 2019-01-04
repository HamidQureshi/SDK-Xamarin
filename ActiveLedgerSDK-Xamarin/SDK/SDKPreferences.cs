using System.Configuration;


namespace ActiveLedgerLib
{
    public static class SDKPreferences
    {
        public static string url;

        // this method can be used for transaction endpoint on the ledger
        public static void setConnection(string protocol, string address, string port)
        {
            url = protocol + "://" + address + ":" + port;
        }

    }
}
