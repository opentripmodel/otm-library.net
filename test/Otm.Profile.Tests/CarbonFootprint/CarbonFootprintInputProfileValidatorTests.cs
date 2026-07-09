using OpenTripModel.Profiles.CarbonFootprint;
using OpenTripModel.v5;
using OpenTripModel.Validation;
using Xunit;

namespace Otm.Profile.Tests.CarbonFootprint;

public class CarbonFootprintInputProfileValidatorTests
{
    private readonly CarbonFootprintInputProfileValidator _validator = new();

    [Fact]
    public void ValidTrip_IsValid()
    {
        var trip = TestTripFactory.ValidInputTrip();

        var result = _validator.Validate(trip);

        Assert.True(result.IsValid, result.ToString());
    }

    [Fact]
    public void MissingVehicle_IsInvalid()
    {
        var trip = TestTripFactory.ValidInputTrip();
        trip.Vehicle = null;

        var result = _validator.Validate(trip);

        Assert.False(result.IsValid);
        Assert.Contains(result.Messages, m => m.Code == ValidationCode.Required && m.Message.Contains("vehicle"));
    }

    [Fact]
    public void MissingLicensePlate_IsInvalid()
    {
        var trip = TestTripFactory.ValidInputTrip();
        trip.Vehicle.Entity.LicensePlate = null;

        var result = _validator.Validate(trip);

        Assert.False(result.IsValid);
        Assert.Contains(result.Messages, m => m.Code == ValidationCode.Required && m.Message.Contains("licensePlate"));
    }

    [Fact]
    public void MissingEnergyConsumptionEvent_IsInvalid()
    {
        var trip = TestTripFactory.ValidInputTrip();
        trip.ContextEvents = new List<Event>();

        var result = _validator.Validate(trip);

        Assert.False(result.IsValid);
        Assert.Contains(result.Messages, m => m.Path.Contains("contextEvents"));
    }

    [Fact]
    public void EnergyEventWithoutQuantity_IsInvalid()
    {
        var trip = TestTripFactory.ValidInputTrip();
        var energyEvent = TestTripFactory.ValidEnergyConsumptionEvent();
        energyEvent.Quantity = null;
        trip.ContextEvents = new List<Event> { energyEvent };

        var result = _validator.Validate(trip);

        Assert.False(result.IsValid);
        Assert.Contains(result.Messages, m => m.Code == ValidationCode.Required && m.Message.Contains("quantity"));
    }

    [Fact]
    public void MissingPostalCode_IsInvalid()
    {
        var trip = TestTripFactory.ValidInputTrip();
        trip.Actions[0].Entity.Location.Entity.AdministrativeReference.PostalCode = null;

        var result = _validator.Validate(trip);

        Assert.False(result.IsValid);
        Assert.Contains(result.Messages, m => m.Code == ValidationCode.Required && m.Message.Contains("postalCode"));
    }

    [Fact]
    public void MissingLoadAction_IsInvalid()
    {
        var trip = TestTripFactory.ValidInputTrip();

        // Turn the load action into a second unload: no stop carries a load action anymore
        trip.Actions[0].Entity.Actions[0].Entity.ActionType = ActionType.Unload;

        var result = _validator.Validate(trip);

        Assert.False(result.IsValid);
        Assert.Contains(result.Messages, m => m.Path.Contains("actions"));
    }

    [Fact]
    public void MissingStopStartTime_IsInvalid()
    {
        var trip = TestTripFactory.ValidInputTrip();
        trip.Actions[0].Entity.StartTime = null;

        var result = _validator.Validate(trip);

        Assert.False(result.IsValid);
        Assert.Contains(result.Messages, m => m.Code == ValidationCode.Required && m.Message.Contains("startTime"));
    }
}
