using System.Configuration;


namespace ActiveLedgerLib
{
    public static class SDKPreferences
    {
        public static string url;

        public static void setConnection(string protocol, string address, string port)
        {
            url = protocol + "://" + address + ":" + port;
        }

    }
}
