using System;
using System.Text.Json;

namespace OpenTripModel.Utilities
{
    /// <summary>
    /// Provides methods for validating JSON input against syntax and model types.
    /// </summary>
    public static class JsonValidationHelper
    {
        /// <summary>
        /// Validates that JSON is well-formed and deserializable into type T.
        /// </summary>
        public static JsonValidationResult ValidateJson<T>(string json)
        {
            try
            {
                JsonSerializer.Deserialize<T>(json);
                return JsonValidationResult.Success();
            }
            catch (JsonException ex)
            {
                return JsonValidationResult.Failure($"Deserialization error: {ex.Message}", ex);
            }
            catch (NotSupportedException ex)
            {
                return JsonValidationResult.Failure($"Unsupported type: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                return JsonValidationResult.Failure($"Unexpected error: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Validates that JSON is syntactically correct.
        /// </summary>
        public static JsonValidationResult ValidateSyntax(string json)
        {
            try
            {
                using (var jsonDocument = JsonDocument.Parse(json))
                    return JsonValidationResult.Success();
            }
            catch (JsonException ex)
            {
                return JsonValidationResult.Failure($"Malformed JSON: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                return JsonValidationResult.Failure($"Unexpected error during parsing: {ex.Message}", ex);
            }
        }
    }
}
