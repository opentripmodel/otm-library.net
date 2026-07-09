using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using OpenTripModel.v5;

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
                TypeInfoResolver = new DefaultJsonTypeInfoResolver
                {
                    Modifiers = { SkipUnsetLifecycles }
                },
                Converters =
                {
                    new JsonStringEnumConverter(JsonNamingPolicy.CamelCase),
                    new OtmEventJsonConverter(),
                }
            };
        }


        /// <summary>
        /// <see cref="Lifecycle"/> starts at 1, so an unset (default) lifecycle holds the
        /// undefined value 0 and would be serialized as a number instead of an enum string.
        /// Omit such properties to keep the produced JSON valid OTM.
        /// </summary>
        private static void SkipUnsetLifecycles(JsonTypeInfo typeInfo)
        {
            foreach (var property in typeInfo.Properties)
            {
                if (property.PropertyType == typeof(Lifecycle))
                    property.ShouldSerialize = (_, value) => value is Lifecycle lifecycle && Enum.IsDefined(lifecycle);
            }
        }
    }
}
