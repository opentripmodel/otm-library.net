using System;
using System.IO;
using System.Text.Json;

namespace OpenTripModel.Serialization
{
    /// <summary>
    /// Default implementation of the OtmSerializer interface using System.Text.Json.
    /// Provides support for both string and stream-based JSON (de)serialization.
    /// </summary>
    public class OtmSerializer : IOtmSerializer
    {
        private readonly JsonSerializerOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="OtmSerializer"/> class.
        /// </summary>
        /// <param name="options">Optional custom JSON serializer options. If null, default options are used.</param>
        public OtmSerializer(JsonSerializerOptions options = null)
        {
            _options = options ?? JsonOptionsFactory.CreateDefault();
        }

        /// <inheritdoc />
        public System.String Serialize<T>(T entity)
        {
            return JsonSerializer.Serialize(entity, _options);
        }

        /// <inheritdoc />
        public T Deserialize<T>(System.String json)
        {
            return JsonSerializer.Deserialize<T>(json, _options);
        }

        /// <inheritdoc />
        public void SerializeToStream<T>(T entity, Stream outputStream)
        {
            if (outputStream == null) throw new ArgumentNullException(nameof(outputStream));

            JsonSerializer.Serialize(outputStream, entity, _options);
        }

        /// <inheritdoc />
        public T DeserializeFromStream<T>(Stream inputStream)
        {
            if (inputStream == null) throw new ArgumentNullException(nameof(inputStream));

            return JsonSerializer.Deserialize<T>(inputStream, _options);
        }
    }
}