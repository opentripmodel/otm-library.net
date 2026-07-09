using OpenTripModel.Serialization;
using OpenTripModel.v5;
using System.Text.Json;
using Xunit;

namespace Otm.Profile.Tests.CarbonFootprint;

public class EventSerializationTests
{
    private readonly OtmSerializer _serializer = new();

    [Fact]
    public void FuelConsumedEvent_InContextEvents_RoundTrips()
    {
        var trip = TestTripFactory.ValidInputTrip();

        var json = _serializer.Serialize(trip);
        var deserialized = _serializer.Deserialize<Trip>(json);

        var fuelEvent = Assert.IsType<FuelConsumedEvent>(Assert.Single(deserialized.ContextEvents));
        Assert.Equal(Fuel.Diesel, fuelEvent.Fuel);
        Assert.Equal(23.4, fuelEvent.Amount.Value);
        Assert.Equal("l", fuelEvent.Amount.Unit);
        Assert.Equal("CO2emissiefactoren.nl", fuelEvent.EmissionFactorStandard);
        Assert.NotNull(fuelEvent.PeriodStart);
        Assert.NotNull(fuelEvent.PeriodEnd);
    }

    [Fact]
    public void EmissionEvent_InContextEvents_RoundTrips()
    {
        var trip = TestTripFactory.ValidResultTrip();

        var json = _serializer.Serialize(trip);
        var deserialized = _serializer.Deserialize<Trip>(json);

        var emissionEvent = Assert.IsType<EmissionEvent>(Assert.Single(deserialized.ContextEvents));
        Assert.Equal(75.2, emissionEvent.Amount.Value);
        Assert.Equal("kgCO2e", emissionEvent.Amount.Unit);
        Assert.Equal("GLEC 3.1", emissionEvent.CalculationMethod.Framework);
        Assert.Equal(DistanceAllocationMethod.Sfd, emissionEvent.CalculationMethod.DistanceAllocationMethod);
        Assert.Equal(2, emissionEvent.CalculationMethod.DataTypeDistribution.Count);
    }

    [Fact]
    public void EventType_IsSerializedAsDiscriminator()
    {
        var trip = TestTripFactory.ValidInputTrip();

        var json = _serializer.Serialize(trip);

        Assert.Contains("\"eventType\": \"fuelConsumedEvent\"", json);
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
