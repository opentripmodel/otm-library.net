using OpenTripModel.v5;
using OpenTripModel.Profiles.CarbonFootprint;
using OpenTripModel.Serialization;

namespace OpenTripModel.Examples;

/// <summary>
/// Shows how a TMS builds an OTM trip as input for a CO₂ calculation platform, following the
/// OTM Carbon Footprint profile (CO₂ Calculation Input): a road trip with a load and unload
/// stop, cargo details and the energy consumption of the vehicle over the trip period.
/// </summary>
public static class CarbonFootprintInputExample
{
    public static void Run()
    {
        var trip = BuildTrip();

        // Serialize to OTM JSON - this is the message sent to the CO₂ calculation platform
        var serializer = new OtmSerializer();
        Console.WriteLine(serializer.Serialize(trip));

        // Validate against the Carbon Footprint input profile before sending
        var validator = new CarbonFootprintInputProfileValidator();
        var result = validator.Validate(trip);

        Console.WriteLine();
        Console.WriteLine(result);
    }

    public static Trip BuildTrip()
    {
        var loadLocation = new InlineAssociationType<Location>
        {
            Entity = new Location
            {
                Name = "DC Utrecht",
                AdministrativeReference = new AdministrativeReference
                {
                    Street = "Laan van Voorburg",
                    HouseNumber = "12",
                    PostalCode = "3542 AB",
                    City = "Utrecht",
                    Country = "NL"
                }
            }
        };

        var unloadLocation = new InlineAssociationType<Location>
        {
            Entity = new Location
            {
                Name = "Winkel Amersfoort",
                AdministrativeReference = new AdministrativeReference
                {
                    Street = "Stationsplein",
                    HouseNumber = "1",
                    PostalCode = "3818 LE",
                    City = "Amersfoort",
                    Country = "NL"
                }
            }
        };

        var consignment = new InlineAssociationType<Consignment>
        {
            Entity = new Consignment
            {
                Id = Guid.NewGuid(),
                Description = "Retail goederen",
                Goods = new List<InlineAssociationType<GoodItem>>
                {
                    new()
                    {
                        Entity = new GoodItem
                        {
                            Description = "Pallets retail goederen",
                            Quantity = 18, // laadeenheid: pallet
                            GrossWeight = UnitWithValue.Create(12500, "kg")
                        }
                    }
                }
            }
        };

        return new Trip
        {
            Id = Guid.NewGuid(),
            Name = "Rit 2026-045123",
            EntityType = EntityType.Trip,
            Status = TripStatus.Completed,
            TransportMode = TransportMode.Road,

            Vehicle = new InlineAssociationType<Vehicle>
            {
                Entity = new Vehicle
                {
                    Id = Guid.NewGuid(),
                    VehicleType = "truck",
                    LicensePlate = "12-ABC-3",
                    Fuel = Fuel.Diesel,
                    EmissionStandard = EmissionStandard.Euro6
                }
            },

            Actors = new List<InlineAssociationType<Actor>>
            {
                new()
                {
                    Description = "Opdrachtgever",
                    Entity = new Actor { Id = Guid.NewGuid(), Name = "Retailer B.V." }
                }
            },

            Actions = new List<InlineAssociationType<StopAction>>
            {
                new()
                {
                    Entity = new StopAction
                    {
                        SequenceNr = 1,
                        Lifecycle = Lifecycle.Realized,
                        StartTime = new DateTime(2026, 6, 15, 7, 30, 0, DateTimeKind.Utc),
                        EndTime = new DateTime(2026, 6, 15, 8, 15, 0, DateTimeKind.Utc),
                        Location = loadLocation,
                        Actions = new List<InlineAssociationType<v5.Action>>
                        {
                            new()
                            {
                                Entity = new v5.Action
                                {
                                    ActionType = ActionType.Load,
                                    Lifecycle = Lifecycle.Realized,
                                    Consignment = consignment
                                }
                            }
                        }
                    }
                },
                new()
                {
                    Entity = new StopAction
                    {
                        SequenceNr = 2,
                        Lifecycle = Lifecycle.Realized,
                        StartTime = new DateTime(2026, 6, 15, 9, 5, 0, DateTimeKind.Utc),
                        EndTime = new DateTime(2026, 6, 15, 9, 45, 0, DateTimeKind.Utc),
                        Location = unloadLocation,
                        Actions = new List<InlineAssociationType<v5.Action>>
                        {
                            new()
                            {
                                Entity = new v5.Action
                                {
                                    ActionType = ActionType.Unload,
                                    Lifecycle = Lifecycle.Realized,
                                    Consignment = consignment
                                }
                            }
                        }
                    }
                }
            },

            ContextEvents = new List<Event>
            {
                new FuelConsumedEvent
                {
                    Id = Guid.NewGuid(),
                    Lifecycle = Lifecycle.Realized,
                    Fuel = Fuel.Diesel,
                    Amount = UnitWithValue.Create(23.4, "l"),
                    EmissionFactorStandard = "CO2emissiefactoren.nl",
                    PeriodStart = new DateTime(2026, 6, 15, 7, 30, 0, DateTimeKind.Utc),
                    PeriodEnd = new DateTime(2026, 6, 15, 9, 45, 0, DateTimeKind.Utc)
                }
            }
        };
    }
}
