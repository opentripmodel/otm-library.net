using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenTripModel.Serialization
{
    /// <summary>
    /// Factory for creating preconfigured JsonSerializerOptions.
    /// </summary>
    public static class JsonOptionsFactory
    {
        /// <summary>
        /// Creates a default JsonSerializerOptions instance with:
        /// - CamelCase naming policy
        /// - Indented output
        /// - Enum serialization as camelCase strings
        /// </summary>
        /// <returns>Preconfigured <see cref="JsonSerializerOptions"/> object.</returns>
        public static JsonSerializerOptions CreateDefault()
        {
            return new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault,
                WriteIndented = true,
                Converters =
                {
                    new JsonStringEnumConverter(JsonNamingPolicy.CamelCase),
                }
            };
        }
    }
}
