using OpenTripModel.Profiles.CBS;
using OpenTripModel.v5;

namespace OpenTripModel.Examples;

public static class TripErrorsExample
{
    // dotnet run -- ActionsExample
    public static void Run()
    {
        // 1. Build a Trip
        var trip = new Trip
        {
            Id = Guid.NewGuid(),
            Status = TripStatus.Completed,
            TransportMode = TransportMode.Road,
            EntityType = EntityType.Trip,

            Vehicle = new InlineAssociationType<Vehicle>
            {
                Entity = new Vehicle
                {
                    Id = Guid.NewGuid(),
                    // LicensePlate = "NL-01-AB",
                    VehicleType = "truck",
                    AverageFuelConsumption = UnitWithValue.Create(32.5, "l/100km"),
                }
            },

            Actors =
            [
                new InlineAssociationActorType
                {
                    Roles = ["carrier"],
                    Entity = new Actor
                    {
                        Id = Guid.NewGuid(),
                        // Name = "Logistics BV",
                        ContactDetails =
                        [
                            new ContactDetail
                            {
                                Type = ContactDetailType.Email,
                                Value = "info@logistics.nl"
                            }
                        ],
                        Locations =
                        [
                            new InlineAssociationType<Location>
                            {
                                Entity = new Location
                                {
                                    GeoReference = new AddressGeoReference
                                    {
                                        // Street               = "Prinsengracht",
                                        // HouseNumber          = "263",
                                        // PostalCode           = "1016 GV",
                                        // City                 = "Amsterdam",
                                        // Country              = "NL"
                                    },
                                }
                            }
                        ]
                    }
                }
            ],

            Actions =
            [
                new InlineAssociationType<StopAction>
                {
                    Entity = new StopAction
                    {
                        Id = Guid.NewGuid(),
                        ActionType = ActionType.AttachTransportEquipment,
                        Lifecycle = Lifecycle.Actual,
                        // StartTime = DateTime.UtcNow.AddMinutes(5),
                        EndTime = DateTime.UtcNow.AddMinutes(15),
                        Location = new InlineAssociationType<Location>
                        {
                            Entity = new Location
                            {
                                Name = "Warehouse Amsterdam",
                                Type = LocationType.Customer,
                                // GeoReference = new LatLonPointGeoReference
                                // {
                                //     Lat = 52.3602,
                                //     Lon = 4.8752
                                // }
                            }
                        }
                    }
                }
            ]
        };


        // 2. Validate Trip
        var cbsProfileValidator = new CbsProfileValidator();

        var validationResult = cbsProfileValidator.Validate(trip);


        // 3. Pretty-print validation errors
        Console.WriteLine(validationResult.ToString());

        // 4. Fix Errors
        trip.Vehicle.Entity.LicensePlate = "NL-01-AB";

        trip.Actors[0].Entity.Name = "Logistics BV";

        trip.Actions[0].Entity.StartTime = DateTime.UtcNow.AddMinutes(5);

        trip.Actors[0].Entity.Locations[0].Entity.GeoReference.Street = "Prinsengracht";
        trip.Actors[0].Entity.Locations[0].Entity.GeoReference.HouseNumber = "263";
        trip.Actors[0].Entity.Locations[0].Entity.GeoReference.City = "Amsterdam";
        trip.Actors[0].Entity.Locations[0].Entity.GeoReference.Country = "NL";
    }
}