// See https://aka.ms/new-console-template for more information

using OpenTripModel.v5;
using OpenTripModel.Serialization;



// Create a sample Trip object
var trip = new Trip
{
    Id = Guid.NewGuid(),
    Name = "Hello trip ;-)",
    Status = TripStatus.InTransit,
};

// Initialize the serializer
IOtmSerializer serializer = new OtmSerializer();

// === String-Based Serialization ===
var json = serializer.Serialize(trip);
Console.WriteLine("Serialized JSON:\n" + json);

var tripFromString = serializer.Deserialize<Trip>(json);
Console.WriteLine($"\nDeserialized from string: Trip ID = {tripFromString.Id}");

// === Stream-Based Serialization ===
string path = "trip.json";
using (var fs = File.Create(path))
{
    serializer.SerializeToStream(trip, fs);
}

using (var fs = File.OpenRead(path))
{
    var tripFromStream = serializer.DeserializeFromStream<Trip>(fs);
    Console.WriteLine($"\nDeserialized from stream: Trip Name = {tripFromStream.Name}");
}
