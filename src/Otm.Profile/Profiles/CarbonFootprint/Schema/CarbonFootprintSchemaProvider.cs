using System;
using System.IO;
using System.Reflection;
using System.Text.Json.Nodes;
using Yaml2JsonNode;
using YamlDotNet.RepresentationModel;
using JsonSchema = Json.Schema.JsonSchema;

namespace OpenTripModel.Profiles.CarbonFootprint.Schema;

internal static class CarbonFootprintSchemaProvider
{
    private const string InputResourceName  = "Otm.Profile.Profiles.CarbonFootprint.Schema.input-schema";
    private const string ResultResourceName = "Otm.Profile.Profiles.CarbonFootprint.Schema.result-schema";

    private static readonly Lazy<JsonSchema> _inputSchema  = new(() => LoadSchema(InputResourceName));
    private static readonly Lazy<JsonSchema> _resultSchema = new(() => LoadSchema(ResultResourceName));


    /// <summary>The schema for the CO₂ Calculation Input sub-profile.</summary>
    public static JsonSchema InputSchema => _inputSchema.Value;


    /// <summary>The schema for the CO₂ Calculation Result sub-profile.</summary>
    public static JsonSchema ResultSchema => _resultSchema.Value;


    private static JsonSchema LoadSchema(string resourceName)
    {
        var jsonNode = LoadYamlAsJsonNode(resourceName);

        return JsonSchema.FromText(jsonNode.ToJsonString());
    }

    private static JsonNode LoadYamlAsJsonNode(string resourceName)
    {
        var asm = Assembly.GetExecutingAssembly();

        using var stream = asm.GetManifestResourceStream(resourceName)
                           ?? throw new InvalidOperationException($"Embedded schema not found: {resourceName}");

        using var reader = new StreamReader(stream);

        var yaml = reader.ReadToEnd();

        var yamlStream = new YamlStream();
        yamlStream.Load(new StringReader(yaml));

        return yamlStream.Documents[0].RootNode.ToJsonNode();
    }
}
