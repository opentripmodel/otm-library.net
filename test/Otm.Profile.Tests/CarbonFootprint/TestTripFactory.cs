using OpenTripModel.v5;

namespace Otm.Profile.Tests.CarbonFootprint;

/// <summary>
/// Builds trips that comply with the Carbon Footprint profile; tests mutate them to
/// create invalid variants.
/// </summary>
internal static class TestTripFactory
{
    public static Trip ValidInputTrip()
    {
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
                new() { Entity = new Actor { Id = Guid.NewGuid(), Name = "Retailer B.V." } }
            },
            Actions = new List<InlineAssociationType<StopAction>>
            {
                Stop(1, ActionType.Load, "3542 AB"),
                Stop(2, ActionType.Unload, "3818 LE")
            },
            ContextEvents = new List<Event> { ValidEnergyConsumptionEvent() }
        };
    }

    public static Trip ValidResultTrip()
    {
        return new Trip
        {
            Id = Guid.NewGuid(),
            Name = "Rit 2026-045123",
            EntityType = EntityType.Trip,
            TransportMode = TransportMode.Road,
            ContextEvents = new List<Event> { ValidEmissionEvent() }
        };
    }

    public static EnergyConsumptionEvent ValidEnergyConsumptionEvent()
    {
        return new EnergyConsumptionEvent
        {
            Id = Guid.NewGuid(),
            EnergySource = Fuel.Diesel,
            Quantity = UnitWithValue.Create(23.4, "l"),
            EmissionFactorStandard = "CO2emissiefactoren.nl",
            PeriodStart = new DateTime(2026, 6, 15, 7, 30, 0, DateTimeKind.Utc),
            PeriodEnd = new DateTime(2026, 6, 15, 9, 45, 0, DateTimeKind.Utc)
        };
    }

    public static EmissionEvent ValidEmissionEvent()
    {
        return new EmissionEvent
        {
            Id = Guid.NewGuid(),
            TotalEmission = UnitWithValue.Create(75.2, "kgCO2e"),
            EmissionIntensity = UnitWithValue.Create(62.1, "gCO2e/tkm"),
            PeriodStart = new DateTime(2026, 6, 15, 7, 30, 0, DateTimeKind.Utc),
            PeriodEnd = new DateTime(2026, 6, 15, 9, 45, 0, DateTimeKind.Utc),
            CalculationMethod = new EmissionCalculationMethod
            {
                Framework = "GLEC 3.1",
                IsoReference = "ISO 14083:2023",
                DistanceAllocationMethod = DistanceAllocationMethod.Sfd,
                Granularity = EmissionDataGranularity.Day,
                DataClassification = EmissionDataClassification.Gold,
                DataTypeDistribution = new List<EmissionDataShare>
                {
                    new() { DataType = EmissionDataType.Primary, Share = 0.85 },
                    new() { DataType = EmissionDataType.Default, Share = 0.15 }
                }
            }
        };
    }

    private static InlineAssociationType<StopAction> Stop(int sequenceNr, ActionType nestedActionType, string postalCode)
    {
        return new InlineAssociationType<StopAction>
        {
            Entity = new StopAction
            {
                SequenceNr = sequenceNr,
                Lifecycle = Lifecycle.Realized,
                StartTime = new DateTime(2026, 6, 15, 7, 30, 0, DateTimeKind.Utc).AddHours(sequenceNr),
                Location = new InlineAssociationType<Location>
                {
                    Entity = new Location
                    {
                        AdministrativeReference = new AdministrativeReference
                        {
                            PostalCode = postalCode,
                            Country = "NL"
                        }
                    }
                },
                Actions = new List<InlineAssociationType<OpenTripModel.v5.Action>>
                {
                    new()
                    {
                        Entity = new OpenTripModel.v5.Action
                        {
                            ActionType = nestedActionType,
                            Lifecycle = Lifecycle.Realized,
                            Consignment = new InlineAssociationType<Consignment>
                            {
                                Entity = new Consignment
                                {
                                    Goods = new List<InlineAssociationType<GoodItem>>
                                    {
                                        new()
                                        {
                                            Entity = new GoodItem
                                            {
                                                Quantity = 18,
                                                GrossWeight = UnitWithValue.Create(12500, "kg")
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        };
    }
}
