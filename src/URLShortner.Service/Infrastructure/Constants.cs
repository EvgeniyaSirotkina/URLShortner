namespace URLShortner.Service.Infrastructure
{
    /// <summary>
    /// Constants values that are often used.
    /// </summary>
    public class Constants
    {
        public const string UNKNOWN = "Unknown";

        /// <summary>
        /// Hypertext transfer protocols.
        /// </summary>
        public class Protocol
        {
            public const string HTTP = "http://";

            public const string HTTPS = "https://";
        }

        /// <summary>
        /// Difference for symbol definition for ASCII Table.
        /// </summary>
        public class ASCIITable
        {
            public const int ADDITIVE_GET_NUMBER = 48;

            public const int ADDITIVE_GET_CHARACTER = 87;
        }
    }
}
