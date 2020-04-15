namespace PluginSupport
{
    /// <summary>
    /// An interface for user defined plugins
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// Encodes raw stream
        /// </summary>
        /// <param name="raw"></param>
        /// <returns></returns>
        string Encode(string raw);


        /// <summary>
        /// Decodes stream that was previously encoded
        /// </summary>
        /// <param name="encoded"></param>
        /// <returns></returns>
        string Decode(string encoded);

    }
}
