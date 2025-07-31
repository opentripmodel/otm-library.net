using System;
using System.IO;
using System.Reflection;
using System.Text.Json.Nodes;
using NJsonSchema;
using OpenTripModel.v5;
using Yaml2JsonNode;
using YamlDotNet.RepresentationModel;
using JsonSchema = Json.Schema.JsonSchema;

namespace OpenTripModel.Profiles.CBS.Schema;

internal static class CbsSchemaProvider
{
    private const string ResourceName = "Otm.Profile.Profiles.CBS.Schema.schema";

    private static readonly Lazy<JsonNode> _schema = new(LoadYamlAsJsonNode);

    /// <summary>Returns the cached JSON-Schema instance.</summary>
    public static JsonNode PhysicalSchema => _schema.Value;

    public static JsonSchema CreateCustomJsonSchema()
    {
        var njs = NJsonSchema.JsonSchema.FromJsonAsync(PhysicalSchema.ToJsonString()).Result;

        // Tweak 1: /vehicle
        // https://github.com/opentripmodel/otm5-change-requests/issues/108
        var vehicleItemSchema = njs.Properties["vehicle"].Item;

        // Register the object schema in Definitions so that $ref can find it
        vehicleItemSchema!.Parent = njs;
        njs.Definitions["vehicle"] = vehicleItemSchema;


        // Replace subschema: vehicle array => vehicle object
        var vehicleProp = new JsonSchemaProperty
        {
            Type = JsonObjectType.Object,
            Reference = vehicleItemSchema,
        };

        njs.Properties["vehicle"] = vehicleProp;


        // Tweak 2: /actors/n/entity/locations
        var actorItemSchema   = njs.Properties["actors"].Item;
        var entitySchema      = actorItemSchema!.Properties["entity"];
        var locationsProp     = entitySchema.Properties["locations"];
        var locationObjSchema = locationsProp.ActualSchema;

        // Register the object schema in Definitions so that $ref can find it
        locationObjSchema.Parent = njs;
        njs.Definitions["Location"] = locationObjSchema;

        // Replace “object” property with “array”
        var locationsArrayProp = new JsonSchemaProperty
        {
            Type = JsonObjectType.Array,
            Item = locationObjSchema
        };

        entitySchema.Properties["locations"] = locationsArrayProp;


        // Tweak 3: /actors/n/entity/contactDetails/n/type
        var contactTypeSchema = njs
            .Properties["actors"].Item?
            .Properties["entity"]
            .Properties["contactDetails"].Item?
            .Properties["type"];

        contactTypeSchema!.Enumeration.Clear();

        // Fill in enum with proper string values
        foreach (var name in Enum.GetNames(typeof(ContactDetailType)))
        {
            // "MobilePhone" -> "mobilePhone"
            var camel = char.ToLowerInvariant(name[0]) + name.Substring(1);
            contactTypeSchema.Enumeration.Add(camel);
        }


        njs.SchemaVersion = "https://json-schema.org/draft/2020-12/schema";

        var finalSchema = JsonSchema.FromText(
            njs
                .ToJson()
                .Replace("http://json-schema.org/draft-04/schema#", "https://json-schema.org/draft/2020-12/schema")
        );

        return finalSchema;
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

        var jsonNode = yamlStream.Documents[0].RootNode.ToJsonNode();

        return jsonNode;
    }

}
