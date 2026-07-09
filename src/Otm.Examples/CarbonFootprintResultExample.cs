using OpenTripModel.v5;
using OpenTripModel.Profiles.CarbonFootprint;
using OpenTripModel.Serialization;

namespace OpenTripModel.Examples;

/// <summary>
/// Shows how a CO₂ calculation platform returns the calculated emissions of a trip to the
/// carrier, following the OTM Carbon Footprint profile (CO₂ Calculation Result): the trip
/// carries an emission context event with the total CO₂e emission, the emission intensity
/// and full transparency on the calculation method.
/// </summary>
public static class CarbonFootprintResultExample
{
    public static void Run()
    {
        var trip = BuildTrip();

        // Serialize to OTM JSON - this is the message returned by the CO₂ calculation platform
        var serializer = new OtmSerializer();
        Console.WriteLine(serializer.Serialize(trip));

        // Validate against the Carbon Footprint result profile
        var validator = new CarbonFootprintResultProfileValidator();
        var result = validator.Validate(trip);

        Console.WriteLine();
        Console.WriteLine(result);
    }

    public static Trip BuildTrip()
    {
        return new Trip
        {
            Id = Guid.NewGuid(),
            Name = "Rit 2026-045123",
            EntityType = EntityType.Trip,
            Status = TripStatus.Completed,
            TransportMode = TransportMode.Road,

            ContextEvents = new List<Event>
            {
                new EmissionEvent
                {
                    Id = Guid.NewGuid(),
                    Lifecycle = Lifecycle.Realized,
                    Amount = UnitWithValue.Create(75.2, "kgCO2e"),
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
                }
            }
        };
    }
}
