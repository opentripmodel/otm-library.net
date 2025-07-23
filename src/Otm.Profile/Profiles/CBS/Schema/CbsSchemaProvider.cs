using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Json.Schema;
using Yaml2JsonNode;
using YamlDotNet.RepresentationModel;


namespace OpenTripModel.Profiles.CBS.Schema
{
    internal static class CbsSchemaProvider
    {
        private const string ResourceName = "Otm.Profile.Profiles.CBS.Schema.schema";

        private static readonly Lazy<JsonSchema> _schema = new(LoadSchema);

        /// <summary>Returns the cached JSON-Schema instance.</summary>
        public static JsonSchema Schema => _schema.Value;

        /// <summary>Returns a tweaked version of the CBS schema with custom vehicle object.</summary>
        public static JsonSchema GetSchemaWithVehicleAsObject()
        {
            var originalSchema = Schema;
            var originalProps = originalSchema.GetKeyword<PropertiesKeyword>()?.Properties ?? new Dictionary<string, JsonSchema>();

            // Get the current vehicle array schema and extract its items schema
            var vehicleArraySchema = originalProps["vehicle"];
            var vehicleObjectSchema = vehicleArraySchema.GetKeyword<ItemsKeyword>()?.SingleSchema;

            if (vehicleObjectSchema == null)
                throw new InvalidOperationException("Could not extract vehicle object schema from array items");

            // Replace vehicle property with the object schema instead of array
            var modifiedProps = originalProps
                .Select(p => p.Key == "vehicle" ? (p.Key, vehicleObjectSchema) : (p.Key, p.Value))
                .ToArray();

            return new JsonSchemaBuilder()
                .Type(SchemaValueType.Object)
                .Properties(modifiedProps)
                .Required(originalSchema.GetKeyword<RequiredKeyword>()?.Properties.ToArray() ?? Array.Empty<string>())
                .AdditionalProperties(originalSchema.GetKeyword<AdditionalPropertiesKeyword>()?.Schema ?? true)
                .Build();
        }


        public static JsonSchema GetSchemaWithMultipleLocations()
        {
            var schemaWithVehicleFixed = GetSchemaWithVehicleAsObject();
            var props = schemaWithVehicleFixed.GetKeyword<PropertiesKeyword>()?.Properties ??
                        new Dictionary<string, JsonSchema>();

            // Get actors array schema
            var actorsSchema = props["actors"];
            var actorItemSchema = actorsSchema.GetKeyword<ItemsKeyword>()?.SingleSchema;
            if (actorItemSchema == null)
                throw new InvalidOperationException("Could not extract actor object schema from array items");

            // Get the actor's properties, then get the entity schema
            var actorProps = actorItemSchema.GetKeyword<PropertiesKeyword>()?.Properties;
            var entitySchema = actorProps?["entity"];
            if (entitySchema == null)
                throw new InvalidOperationException("Could not find entity property in actor schema");

            // Now get the entity's properties to find locations
            var entityProps = entitySchema.GetKeyword<PropertiesKeyword>()?.Properties;
            var locationsObjectSchema = entityProps?["locations"];
            if (locationsObjectSchema == null)
                throw new InvalidOperationException("Could not find locations property in entity schema");

            // Create array schema with the current locations object as items
            var locationsArraySchema = new JsonSchemaBuilder()
                .Type(SchemaValueType.Array)
                .Items(locationsObjectSchema)
                .Build();

            // Rebuild entity schema with modified locations
            var modifiedEntityProps = entityProps
                .Select(p => p.Key == "locations" ? (p.Key, locationsArraySchema) : (p.Key, p.Value))
                .ToArray();

            var modifiedEntitySchema = new JsonSchemaBuilder()
                .Type(SchemaValueType.Object)
                .Properties(modifiedEntityProps)
                .Required(entitySchema.GetKeyword<RequiredKeyword>()?.Properties.ToArray() ?? Array.Empty<string>())
                .AdditionalProperties(entitySchema.GetKeyword<AdditionalPropertiesKeyword>()?.Schema ?? true)
                .Build();

            // Rebuild actor schema with modified entity
            var modifiedActorProps = actorProps
                .Select(p => p.Key == "entity" ? (p.Key, modifiedEntitySchema) : (p.Key, p.Value))
                .ToArray();

            var modifiedActorSchema = new JsonSchemaBuilder()
                .Type(SchemaValueType.Object)
                .Properties(modifiedActorProps)
                .Required(actorItemSchema.GetKeyword<RequiredKeyword>()?.Properties.ToArray() ?? Array.Empty<string>())
                .AdditionalProperties(actorItemSchema.GetKeyword<AdditionalPropertiesKeyword>()?.Schema ?? true)
                .Build();

            // Rebuild actors array with modified item schema
            var modifiedActorsSchema = new JsonSchemaBuilder()
                .Type(SchemaValueType.Array)
                .Items(modifiedActorSchema)
                .Build();

            // Rebuild root schema with modified actors
            var modifiedRootProps = props
                .Select(p => p.Key == "actors" ? (p.Key, modifiedActorsSchema) : (p.Key, p.Value))
                .ToArray();

            return new JsonSchemaBuilder()
                .Type(SchemaValueType.Object)
                .Properties(modifiedRootProps)
                .Required(schemaWithVehicleFixed.GetKeyword<RequiredKeyword>()?.Properties.ToArray() ??
                          Array.Empty<string>())
                .AdditionalProperties(schemaWithVehicleFixed.GetKeyword<AdditionalPropertiesKeyword>()?.Schema ?? true)
                .Build();
        }

        private static JsonSchema LoadSchema()
        {
            var asm = Assembly.GetExecutingAssembly();

            using var stream = asm.GetManifestResourceStream(ResourceName)
                               ?? throw new InvalidOperationException($"Embedded schema not found: {ResourceName}");


            using var reader = new StreamReader(stream);

            var yaml = reader.ReadToEnd();

            var yamlStream = new YamlStream();
            yamlStream.Load(new StringReader(yaml));

            var jsonNode = yamlStream.Documents[0].RootNode.ToJsonNode();

            var cbsJsonSchema = JsonSchema.FromText(jsonNode!.ToJsonString());

            return cbsJsonSchema;
        }
    }
}
