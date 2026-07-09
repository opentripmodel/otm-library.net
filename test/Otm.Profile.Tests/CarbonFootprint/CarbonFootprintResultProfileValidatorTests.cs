using OpenTripModel.Profiles.CarbonFootprint;
using OpenTripModel.v5;
using OpenTripModel.Validation;
using Xunit;

namespace Otm.Profile.Tests.CarbonFootprint;

public class CarbonFootprintResultProfileValidatorTests
{
    private readonly CarbonFootprintResultProfileValidator _validator = new();

    [Fact]
    public void ValidTrip_IsValid()
    {
        var trip = TestTripFactory.ValidResultTrip();

        var result = _validator.Validate(trip);

        Assert.True(result.IsValid, result.ToString());
    }

    [Fact]
    public void MissingEmissionEvent_IsInvalid()
    {
        var trip = TestTripFactory.ValidResultTrip();
        trip.ContextEvents = new List<Event>();

        var result = _validator.Validate(trip);

        Assert.False(result.IsValid);
        Assert.Contains(result.Messages, m => m.Path.Contains("contextEvents"));
    }

    [Fact]
    public void MissingTotalEmission_IsInvalid()
    {
        var trip = TestTripFactory.ValidResultTrip();
        var emissionEvent = TestTripFactory.ValidEmissionEvent();
        emissionEvent.TotalEmission = null;
        trip.ContextEvents = new List<Event> { emissionEvent };

        var result = _validator.Validate(trip);

        Assert.False(result.IsValid);
        Assert.Contains(result.Messages, m => m.Code == ValidationCode.Required && m.Message.Contains("totalEmission"));
    }

    [Fact]
    public void MissingCalculationMethod_IsInvalid()
    {
        var trip = TestTripFactory.ValidResultTrip();
        var emissionEvent = TestTripFactory.ValidEmissionEvent();
        emissionEvent.CalculationMethod = null;
        trip.ContextEvents = new List<Event> { emissionEvent };

        var result = _validator.Validate(trip);

        Assert.False(result.IsValid);
        Assert.Contains(result.Messages, m => m.Code == ValidationCode.Required && m.Message.Contains("calculationMethod"));
    }

    [Fact]
    public void CalculationMethodWithoutFramework_IsInvalid()
    {
        var trip = TestTripFactory.ValidResultTrip();
        var emissionEvent = TestTripFactory.ValidEmissionEvent();
        emissionEvent.CalculationMethod.Framework = null;
        trip.ContextEvents = new List<Event> { emissionEvent };

        var result = _validator.Validate(trip);

        Assert.False(result.IsValid);
        Assert.Contains(result.Messages, m => m.Code == ValidationCode.Required && m.Message.Contains("framework"));
    }

    [Fact]
    public void DataTypeShareAboveOne_IsInvalid()
    {
        var trip = TestTripFactory.ValidResultTrip();
        var emissionEvent = TestTripFactory.ValidEmissionEvent();
        emissionEvent.CalculationMethod.DataTypeDistribution[0].Share = 1.5;
        trip.ContextEvents = new List<Event> { emissionEvent };

        var result = _validator.Validate(trip);

        Assert.False(result.IsValid);
        Assert.Contains(result.Messages, m => m.Path.Contains("dataTypeDistribution"));
    }
}
