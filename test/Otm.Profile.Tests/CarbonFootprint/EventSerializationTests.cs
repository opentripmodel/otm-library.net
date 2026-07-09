using OpenTripModel.Serialization;
using OpenTripModel.v5;
using System.Text.Json;
using Xunit;

namespace Otm.Profile.Tests.CarbonFootprint;

public class EventSerializationTests
{
    private readonly OtmSerializer _serializer = new();

    [Fact]
    public void EnergyConsumptionEvent_InContextEvents_RoundTrips()
    {
        var trip = TestTripFactory.ValidInputTrip();

        var json = _serializer.Serialize(trip);
        var deserialized = _serializer.Deserialize<Trip>(json);

        var energyEvent = Assert.IsType<EnergyConsumptionEvent>(Assert.Single(deserialized.ContextEvents));
        Assert.Equal(Fuel.Diesel, energyEvent.EnergySource);
        Assert.Equal(23.4, energyEvent.Quantity.Value);
        Assert.Equal("l", energyEvent.Quantity.Unit);
        Assert.Equal("CO2emissiefactoren.nl", energyEvent.EmissionFactorStandard);
        Assert.NotNull(energyEvent.PeriodStart);
        Assert.NotNull(energyEvent.PeriodEnd);
    }

    [Fact]
    public void EmissionEvent_InContextEvents_RoundTrips()
    {
        var trip = TestTripFactory.ValidResultTrip();

        var json = _serializer.Serialize(trip);
        var deserialized = _serializer.Deserialize<Trip>(json);

        var emissionEvent = Assert.IsType<EmissionEvent>(Assert.Single(deserialized.ContextEvents));
        Assert.Equal(75.2, emissionEvent.TotalEmission.Value);
        Assert.Equal("kgCO2e", emissionEvent.TotalEmission.Unit);
        Assert.Equal("GLEC 3.1", emissionEvent.CalculationMethod.Framework);
        Assert.Equal(DistanceAllocationMethod.Sfd, emissionEvent.CalculationMethod.DistanceAllocationMethod);
        Assert.Equal(2, emissionEvent.CalculationMethod.DataTypeDistribution.Count);
    }

    [Fact]
    public void EventType_IsSerializedAsDiscriminator()
    {
        var trip = TestTripFactory.ValidInputTrip();

        var json = _serializer.Serialize(trip);

        Assert.Contains("\"eventType\": \"energyConsumptionEvent\"", json);
    }

    [Fact]
    public void UnknownEventType_ThrowsJsonException()
    {
        const string json = """
            {
                "contextEvents": [ { "eventType": "somethingElse" } ]
            }
            """;

        Assert.Throws<JsonException>(() => _serializer.Deserialize<Trip>(json));
    }
}
