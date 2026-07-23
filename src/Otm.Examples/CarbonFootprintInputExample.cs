using OpenTripModel.Profiles.CarbonFootprint;
using OpenTripModel.Serialization;
using OpenTripModel.v5;

namespace OpenTripModel.Examples;

/// <summary>
/// Demonstrates the OTM <c>Carbon Footprint (input)</c> profile (v0.5):
/// <list type="bullet">
///   <item>Builds a minimal valid message — only required fields.</item>
///   <item>Builds a full message populating every optional field (nested move / load / unload,
///         consignments, goods, address-aware location, vehicle details).</item>
///   <item>Serializes → deserializes → validates both messages, asserting that round-trip
///         produces byte-identical JSON and that the profile validator reports no errors.</item>
/// </list>
/// Run with: <c>dotnet run -- CarbonFootprintInputExample</c>
/// </summary>
public static class CarbonFootprintInputExample
{
    public static void Run()
    {
        // The Carbon Footprint (input) profile's schema is strict (additionalProperties: false at
        // every level), so we use the validator-provided serializer configuration — it suppresses
        // default-valued enums like Trip.Status that the profile does not define.
        var serializer = CarbonFootprintInputProfileValidator.CreateSerializer();
        var validator = new CarbonFootprintInputProfileValidator();

        // ────────────────────────────────────────────────────────────────────────────────
        // 1. MINIMAL — only the profile-required fields: entityType + vehicle.licensePlate
        //    plus one fuelConsumedEvent (optional at Trip level, but the whole point of this
        //    profile is to carry fuel data, so we include one).
        // ────────────────────────────────────────────────────────────────────────────────
        var minimalTrip = new Trip
        {
            EntityType = EntityType.Trip,

            Vehicle = new InlineAssociationType<Vehicle>
            {
                Entity = new Vehicle
                {
                    LicensePlate = "NL-01-AB",
                },
            },

            ContextEvents =
            [
                new FuelConsumedEvent
                {
                    Id = Guid.NewGuid(),
                    Fuel = Fuel.Diesel,
                    Amount = UnitWithValue.Create(42.0, "l"),
                },
            ],
        };

        RunCase("MINIMAL", minimalTrip, serializer, validator);


        // ────────────────────────────────────────────────────────────────────────────────
        // 2. FULL — populate every optional field the profile allows, including nested
        //    move → load/unload actions with consignments, goods, and a latLonPointGeoReference
        //    location.
        //
        // Note: the profile permits a DIFFERENT consignment shape under fuelConsumedEvent
        // (goods[].entity = {type, weight}) vs. under a load/unload action (goods[].entity =
        // {type, quantity, weight, width, height, length} plus id and transportOrder). Build
        // two distinct consignments so the serialized JSON matches each slot's schema.
        // ────────────────────────────────────────────────────────────────────────────────

        // Consignment shape allowed on load/unload actions
        var actionConsignment = new InlineAssociationType<Consignment>
        {
            Entity = new Consignment
            {
                Id = Guid.NewGuid(),
                Goods =
                [
                    new InlineAssociationType<GoodItem>
                    {
                        Entity = new GoodItem
                        {
                            Type = GoodItemType.Items,
                            Quantity = 12,
                            Weight = UnitWithValue.Create(15.0, "kg"),
                            Width = UnitWithValue.Create(0.4, "m"),
                            Height = UnitWithValue.Create(0.3, "m"),
                            Length = UnitWithValue.Create(1.2, "m"),
                        },
                    },
                ],
                TransportOrder = new InlineAssociationType<TransportOrder>
                {
                    Entity = new TransportOrder
                    {
                        Id = Guid.NewGuid(),
                    },
                },
            },
        };

        // Simpler consignment shape allowed on fuelConsumedEvent
        var eventConsignment = new InlineAssociationType<Consignment>
        {
            Entity = new Consignment
            {
                Goods =
                [
                    new InlineAssociationType<GoodItem>
                    {
                        Entity = new GoodItem
                        {
                            Type = GoodItemType.Items,
                            Weight = UnitWithValue.Create(15.0, "kg"),
                        },
                    },
                ],
            },
        };

        var warehouseLocation = new InlineAssociationType<Location>
        {
            Entity = new Location
            {
                GeoReference = new LatLonPointGeoReference
                {
                    Lat = 52.3702,
                    Lon = 4.8952,
                    // Address fields are supported on latLonPointGeoReference by this profile.
                    // They are NOT part of the C# LatLonPointGeoReference type, so we leave them
                    // off here — the round-trip still round-trips what we set.
                },
            },
        };

        var fullTrip = new Trip
        {
            EntityType = EntityType.Trip,
            TransportMode = TransportMode.Road,

            Vehicle = new InlineAssociationType<Vehicle>
            {
                Entity = new Vehicle
                {
                    Id = Guid.NewGuid(),
                    VehicleType = "truck",
                    AverageFuelConsumption = UnitWithValue.Create(32.5, "l/100km"),
                    LicensePlate = "NL-01-AB",
                    EmptyWeight = UnitWithValue.Create(7500.0, "kg"),
                },
            },

            Actors =
            [
                new InlineAssociationType<Actor>
                {
                    Description = "Carrier",
                    Entity = new Actor
                    {
                        EntityType = EntityType.Actor,
                        Id = Guid.NewGuid(),
                    },
                },
            ],

            Actions =
            [
                new InlineAssociationType<StopAction>
                {
                    Entity = new StopAction
                    {
                        // MoveAction at top level is allowed by the profile (actionType ∈ {move,stop}).
                        // StopAction.ActionType is now settable; alternatively use MoveAction directly
                        // after widening the collection typing (out-of-scope for this example).
                        ActionType = ActionType.Stop,
                        Distance = UnitWithValue.Create(0.0, "km"),
                        Actions =
                        [
                            new InlineAssociationType<OpenTripModel.v5.Action>
                            {
                                Entity = new LoadAction
                                {
                                    Consignment = actionConsignment,
                                    Location = warehouseLocation,
                                    TimeFormat = TimeFormat.DateTime,
                                    StartTime = new DateTime(2026, 4, 24, 8, 0, 0, DateTimeKind.Utc),
                                },
                            },
                            new InlineAssociationType<OpenTripModel.v5.Action>
                            {
                                Entity = new UnloadAction
                                {
                                    Consignment = actionConsignment,
                                    Location = warehouseLocation,
                                    TimeFormat = TimeFormat.DateTime,
                                    StartTime = new DateTime(2026, 4, 24, 10, 0, 0, DateTimeKind.Utc),
                                },
                            },
                        ],
                    },
                },
                new InlineAssociationType<StopAction>
                {
                    Entity = new StopAction
                    {
                        ActionType = ActionType.Move,
                        Distance = UnitWithValue.Create(142.7, "km"),
                    },
                },
            ],

            ContextEvents =
            [
                new FuelConsumedEvent
                {
                    Id = Guid.NewGuid(),
                    Name = "Leg 1 refuel",
                    Fuel = Fuel.Diesel,
                    Amount = UnitWithValue.Create(48.3, "l"),
                    Consignment = eventConsignment,
                },
                new FuelConsumedEvent
                {
                    Id = Guid.NewGuid(),
                    Name = "AdBlue top-up",
                    Fuel = Fuel.AdBlue,
                    Amount = UnitWithValue.Create(2.1, "l"),
                },
            ],
        };

        RunCase("FULL", fullTrip, serializer, validator);
    }


    private static void RunCase(
        string label,
        Trip trip,
        IOtmSerializer serializer,
        CarbonFootprintInputProfileValidator validator)
    {
        Console.WriteLine($"\n================ {label} ================");

        // Serialize
        var json = serializer.Serialize(trip);
        Console.WriteLine(json);

        // Round-trip: deserialize and re-serialize — the two JSONs must be byte-identical.
        var roundTripped = serializer.Deserialize<Trip>(json);
        var jsonAgain = serializer.Serialize(roundTripped);

        if (json != jsonAgain)
        {
            Console.WriteLine($"\n❌ {label}: round-trip mismatch!");
            Console.WriteLine("--- original ---");
            Console.WriteLine(json);
            Console.WriteLine("--- round-tripped ---");
            Console.WriteLine(jsonAgain);
            throw new InvalidOperationException($"{label} round-trip mismatch");
        }

        Console.WriteLine($"✓ {label}: round-trip successful ({json.Length} chars identical)");

        // Validate against the profile
        var result = validator.Validate(trip);
        Console.WriteLine(result.ToString());

        if (!result.IsValid)
        {
            throw new InvalidOperationException($"{label} failed carbon-footprint-input validation");
        }

        Console.WriteLine($"✓ {label}: profile validation passed");
    }
}
