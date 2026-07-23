using System;
using System.IO;
using System.Reflection;
using System.Text.Json.Nodes;
using Yaml2JsonNode;
using YamlDotNet.RepresentationModel;
using JsonSchema = Json.Schema.JsonSchema;

namespace OpenTripModel.Profiles.CarbonFootprint.Schema;

/// <summary>
/// Loads the embedded YAML schema for the OTM <c>Carbon Footprint (input)</c> profile and
/// materializes it into a <see cref="JsonSchema"/> instance. The schema is loaded lazily and
/// cached for the lifetime of the process.
/// </summary>
internal static class CarbonFootprintInputSchemaProvider
{
    private const string ResourceName = "Otm.Profile.Profiles.CarbonFootprint.Schema.schema";

    private static readonly Lazy<JsonNode> _schema = new(LoadYamlAsJsonNode);

    /// <summary>
    /// Returns the cached <see cref="JsonNode"/> representation of the physical YAML schema.
    /// </summary>
    public static JsonNode PhysicalSchema => _schema.Value;


    /// <summary>
    /// Builds a <see cref="JsonSchema"/> ready for evaluation. The schema is loaded from the
    /// embedded YAML resource and parsed as-is; no structural tweaks are applied (unlike the
    /// CBS profile, which rewrites legacy array/object mismatches).
    /// </summary>
    public static JsonSchema CreateCustomJsonSchema()
    {
        return JsonSchema.FromText(PhysicalSchema.ToJsonString());
    }


    private static JsonNode LoadYamlAsJsonNode()
    {
        var asm = Assembly.GetExecutingAssembly();

        using var stream = asm.GetManifestResourceStream(ResourceName)
                           ?? throw new InvalidOperationException($"Embedded schema not found: {ResourceName}");

        using var reader = new StreamReader(stream);

        var yaml = reader.ReadToEnd();

        var yamlStream = new YamlStream();
        yamlStream.Load(new StringReader(yaml));

        return yamlStream.Documents[0].RootNode.ToJsonNode();
    }
}
