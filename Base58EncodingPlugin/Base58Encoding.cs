using PluginSupport;
using SimpleBase;

namespace Base58EncodingPlugin
{
    [ExtensionAttribute(Extension = "B58")]
    public class Base58Encoding : IPlugin
    {

        public string Decode(string encoded)
        {
            return System.Text.Encoding.UTF8.GetString(Base58.Bitcoin.Decode(encoded).ToArray());
        }

        public string Encode(string raw)
        {
            return Base58.Bitcoin.Encode(System.Text.Encoding.UTF8.GetBytes(raw));
        }
    }
}
