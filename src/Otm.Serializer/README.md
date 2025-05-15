# 📦 OpenTripModel.Serializer

**A lightweight, high-performance .NET library for JSON serialization and deserialization of OTM (Open Trip Model) entities.**  
Supports both string- and stream-based operations and integrates smoothly with your own model layer.

---

## 🚀 Features

- ✅ Serialize and deserialize to/from `string` and `Stream`
- ✅ Enum support as **camelCase strings**
- ✅ Customizable via `JsonSerializerOptions`
- ✅ Designed for **memory efficiency**
- ✅ Simple validation helpers with detailed error reporting
- 🔧 (Optional) JSON Schema validation support via [JsonSchema.Net](https://github.com/gregsdennis/json-everything)

---

## 📁 Folder Structure

```
src/
├── Serialization/
│   ├── IOtmSerializer.cs        # Interface for the serializer
│   ├── OtmSerializer.cs         # Default implementation
│   └── JsonOptionsFactory.cs    # Shared default settings
│
└── Utilities/
    ├── JsonValidationHelper.cs  # Syntax & model validation helpers
    └── JsonValidationResult.cs  # Structured validation result object
```

---

## 🛠️ Installation

Install via NuGet (once published):

```bash
dotnet add package OpenTripModel.Serializer
```

Or reference the project directly if using it in a multi-project solution.

---

## 📦 Dependencies

- [.NET 8+ or .NET Standard 2.1+](https://dotnet.microsoft.com/)
- `System.Text.Json` (built-in)

Optional:
- `Json.Schema` (for schema validation)

---

## ✨ Usage Example

### Simple Serialization

```csharp
using OpenTripModel.Serializer.Serialization;
using OpenTripModel.Models; // from your model project

var trip = new Trip { Id = "T1", Status = TripStatus.Planned };

var serializer = new OtmJsonSerializer();
string json = serializer.Serialize(trip);
Trip deserialized = serializer.Deserialize<Trip>(json);
```

---

### Stream-Based (Memory Efficient)

```csharp
using var stream = File.Create("trip.json");
serializer.SerializeToStream(trip, stream);

using var input = File.OpenRead("trip.json");
var loadedTrip = serializer.DeserializeFromStream<Trip>(input);
```

---

### JSON Validation

```csharp
var result = JsonValidationHelper.ValidateJson<Trip>(json);
if (!result.IsValid)
{
    Console.WriteLine(result.ErrorMessage);
}
```

---


## 📄 License

This project is open source and licensed under the [MIT License](LICENSE).

---

## 🤝 Contributing

Contributions are welcome! Please:
- Follow clean code practices.
- Add unit tests for new features.
- Keep `System.Text.Json` as the base dependency (no Newtonsoft.Json).

---

## 🔗 Related Projects

- [`OpenTripModel.Model`](https://github.com/your-org/OpenTripModel.Model): Core OTM entity definitions.
