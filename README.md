# OTM .NET Toolkit

OpenTripModel .NET provides concise OTM types and utilities for C#, helping teams standardize message flows with
partners that agree on OTM profiles.

**Define how your data translates to OTM** using lightweight model types, **validate** to catch missing or invalid
profile-required fields, and **publish** via your existing transport stack (request/response APIs, file-based transfers,
publish/subscribe messaging, and other established patterns).

## About OTM

**Open Trip Model (OTM)** is an open specification that defines a data model for exchanging real-time logistics trip
information between systems.

**Read more:** [OTM v5.7 API & Data Model](https://otm-api-spec.redocly.app/api/5.7/otm).

## **How this library relates to OTM**

This .NET toolkit helps you define the translation from your domain to OTM using lightweight types from the
`OpenTripModel.Model`
namespace, **validate** messages against the agreed profile via `OpenTripModel.Profile` (pass/fail with reasons),
and **serialize** to OTM-style JSON via `OpenTripModel.Serializer` (camelCase properties, string enums, sensible null
handling).

## NuGet Packages

| **Package**                | **Latest Version**                                                                                                                                                                                                 | **About**                                                                                                                                                                                                                                                                                                                       |
|:---------------------------|:-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|:--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `OpenTripModel.Model`      | [![NuGet](https://img.shields.io/nuget/v/OpenTripModel.Model?logo=nuget&label=NuGet&color=blue)](https://www.nuget.org/packages/OpenTripModel.Model/ "Download OpenTripModel.Model from NuGet.org")                | C# type definitions for [OpenTripModel v5 entities](https://opentripmodel.org/docs/fundamentals/entities). Facilitates integration with OTM-compliant systems by providing ready-to-use data structures that match the specification's entity model                                                                             |
| `OpenTripModel.Profile`    | [![NuGet](https://img.shields.io/nuget/v/OpenTripModel.Profile?logo=nuget&label=NuGet&color=blue)](https://www.nuget.org/packages/OpenTripModel.Profile/ "Download OpenTripModel.Profile from NuGet.org")          | **Validate** messages against a chosen **[Profile](https://opentripmodel.org/docs/profiles/)**; checks **required fields**, **value constraints** (e.g., string lengths, patterns, enums), and flags **extra/unknown fields** when disallowed to ensure profile compliance before exchange. **Returns pass/fail with reasons**. |
| `OpenTripModel.Serializer` | [![NuGet](https://img.shields.io/nuget/v/OpenTripModel.Serializer?logo=nuget&label=NuGet&color=blue)](https://www.nuget.org/packages/OpenTripModel.Serializer/ "Download OpenTripModel.Serializer from NuGet.org") | JSON serialization aligned with OTM conventions based on public examples (property naming, enum casing, null handling), ensuring consistent payloads when serializing messages.                                                                                                                                                 |

## 🚀 Quickstart

To use OpenTripModel .NET, you create OTM entities using the Model types and validate them against a profile.
A profile is a set of context-specific rules (required fields, constraints) that ensure your messages are compatible
with your logistics partners.

To get started, first add the [`OpenTripModel.Model`](https://www.nuget.org/packages/OpenTripModel.Model/) package to
your project by running the following command:

```sh
dotnet add package OpenTripModel.Model
```

You can create a minimal OTM Trip using the Model types as shown below:

```cs
using OpenTripModel.v5;

var trip = new Trip
{
    Id = Guid.NewGuid(),
    EntityType = EntityType.Trip,
    TransportMode = TransportMode.Road,

    Vehicle = new InlineAssociationType<Vehicle>
    {
        Entity = new Vehicle
        {
            Id = Guid.NewGuid(),
            VehicleType = "truck",
            LicensePlate = "NL-01-AB"
            // rest of properties...
        }
    },

    Actions =
    [
        new InlineAssociationType<StopAction>
        {
            Entity = new StopAction
            {
                Id = Guid.NewGuid(),
                ActionType = ActionType.Pickup,
                Lifecycle = Lifecycle.Planned,
                StartTime = DateTime.UtcNow,
                Location = new InlineAssociationType<Location>
                {
                    Entity = new Location
                    {
                        Name = "Warehouse Amsterdam",
                        GeoReference = new AddressGeoReference { City = "Amsterdam", Country = "NL" }
                        // rest of properties...
                    }
                }
            }
        }
    ]
};
```

### Profile validation

Install the [`OpenTripModel.Profile`](https://www.nuget.org/packages/OpenTripModel.Profile/) package and run validation
to get message feedback before publishing.

```sh
dotnet add package OpenTripModel.Profile
```

Profiles declare required fields, cardinality, and value/format limits. Run validation to get immediate feedback (
pass/fail with reasons):

```cs
using OpenTripModel.Profiles.CBS;

var validator = new CbsProfileValidator(); // choose the desired profile (CBS / TransportOrder / other)
var result = validator.Validate(trip);

// Investigate validation errors
Console.WriteLine(validationResult.ToString());

// After reviewing validation result, fix any reported issues within your message structure
```

### Serialize & publish

Install the [`OpenTripModel.Serializer`](https://www.nuget.org/packages/OpenTripModel.Serializer/) package and emit
OTM-style JSON, then publish via your existing transport (APIs, file exchange, messaging).

```sh
dotnet add package OpenTripModel.Serializer
```

Once installed, use the OtmSerializer to convert your OTM entities to JSON with proper formatting (camelCase properties,
string enums, etc.):

```cs
var otmSerializer = new OtmSerializer();

var json = otmSerializer.Serialize(trip);
// send via your transport stack (request/response APIs, file-based transfers, publish/subscribe messaging, etc.)
```

## 📌 Versioning

We use a form of Semantic Versioning (SemVer) with the change being that we incorporate the OTM schema version as the
first two digits of the release:

```txt
OTM-MAJOR.OTM-MINOR.FEATURE.PATCH
```

| Change Type         | Version Example   | Description                                                      |
|---------------------|-------------------|------------------------------------------------------------------|
| Breaking changes    | 5.7.0.0 → 5.8.0.0 | Incompatible API change                                          |
| New features        | 5.7.1.0 → 5.7.2.0 | Backwards-compatible enhancements (e.g., adding a new validator) |
| Bug fixes / patches | 5.7.1.0 → 5.7.1.1 | Backwards-compatible bug fix (e.g., fixing the validator)        |

> **Rule:** keep all `OpenTripModel.*` packages pinned to the **same OTM stream** (e.g., `5.7.*`) across your solution.

## 🤝 Contributing

Thanks for your interest in improving **OpenTripModel .NET**! We welcome issues and pull requests from everyone.

## License

This library is licensed under the [MIT License](LICENSE). You are free to use, modify, and distribute this library in
your projects, provided you include the original license text.
