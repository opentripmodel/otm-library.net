using System.Collections.Generic;
using System.Linq;
using Json.Pointer;
using Json.Schema;

namespace OpenTripModel.Profile.Internal;

internal static class JsonSchemaEnumValues
{
    /// <summary>
    /// Returns the permitted values of the <c>enum</c> keyword found at
    /// <paramref name="schemaPointer"/> inside <paramref name="root"/>.
    /// </summary>
    /// <param name="root">
    ///   The root <see cref="JsonSchema"/> used for validation.
    /// </param>
    /// <param name="schemaPointer">
    ///   JSON-Pointer fragment (no leading <c>#</c>), e.g.
    ///   <c>/properties/actors/items/properties/entity/properties/…/type</c>
    /// </param>
    /// <returns>
    ///   An ordered list of the enum’s values as JSON strings
    ///   (e.g. <c>"Email"</c>, <c>"1"</c>, <c>"false"</c>).
    ///   Empty when the pointer doesn't resolve to an <c>enum</c>.
    /// </returns>
    public static IReadOnlyList<string> Get(JsonSchema root, string schemaPointer)
    {
        var pointer = JsonPointer.Parse(schemaPointer);

        var opts = EvaluationOptions.Default;

        var target = ((IBaseDocument)root).FindSubschema(pointer, opts);
        if (target is null || !target.TryGetKeyword<EnumKeyword>(out var enumKw))
            return new List<string>();

        return enumKw.Values
            .Select(node => node.ToJsonString()) // "\"Email\"", "1", "false"
            .ToList();
    }
}
