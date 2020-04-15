using PluginSupport;

namespace Base64EncodingPlugin
{
    [ExtensionAttribute(Extension = "B64")]
    public class Base64Encoding : IPlugin
    {
        public string Decode(string encoded)
        {
            return System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(encoded));
        }

        public string Encode(string raw)
        {
            return System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(raw));
        }
    }
}
