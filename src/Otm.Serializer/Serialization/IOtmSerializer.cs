using System.IO;

namespace OpenTripModel.Serialization
{
    /// <summary>
    /// Interface for serializing and deserializing OTM entities to and from JSON.
    /// Supports both string-based and stream-based operations for memory efficiency.
    /// </summary>
    public interface IOtmSerializer
    {
        /// <summary>
        /// Serializes an object to a JSON string using default options.
        /// </summary>
        /// <typeparam name="T">The type of the object to serialize.</typeparam>
        /// <param name="entity">The object to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        System.String Serialize<T>(T entity);



        /// <summary>
        /// Deserializes a JSON string to an object.
        /// </summary>
        /// <typeparam name="T">The target type of the object.</typeparam>
        /// <param name="json">The JSON string input.</param>
        /// <returns>The deserialized object.</returns>
        T Deserialize<T>(System.String json);



        /// <summary>
        /// Serializes an object to a stream in JSON format.
        /// Optimized for low-memory scenarios.
        /// </summary>
        /// <typeparam name="T">The type of the object to serialize.</typeparam>
        /// <param name="entity">The object to serialize.</param>
        /// <param name="outputStream">The target output stream.</param>
        void SerializeToStream<T>(T entity, Stream outputStream);



        /// <summary>
        /// Deserializes JSON from a stream into an object.
        /// </summary>
        /// <typeparam name="T">The target type of the object.</typeparam>
        /// <param name="inputStream">The input stream containing JSON.</param>
        /// <returns>The deserialized object.</returns>
        T DeserializeFromStream<T>(Stream inputStream);
    }
}


// test