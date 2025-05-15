# 🧩 Otm.Model

**Core entity definitions for the Open Trip Model (OTM), implemented in clean, idiomatic C# classes.**  
This project provides the data structures used across the OTM ecosystem, including serialization libraries, APIs, and tools.

---

## 📦 Features

- ✅ Strongly typed C# representations of OTM entities
- ✅ Follows OTM standards and naming conventions
- ✅ Designed for compatibility with `System.Text.Json`
- ✅ Lightweight: no external dependencies
- ✅ Enum support and extensible nested models

---

## 🧱 Example Entities

### `Trip.cs`

```csharp
public class Trip
{
    public string Id { get; set; }
    public string Name { get; set; }
}
```


---

## 🛠️ Usage

This project is designed to be referenced by:

- `Otm.Serializer` — for JSON serialization
- Web APIs or ASP.NET services
- CLI tools or desktop apps
- Background jobs or integrations

Add reference:

```bash
dotnet add reference ../Otm.Model/Otm.Model.csproj
```

---

## 🧪 Testing

This project contains only data models. Testing is typically done in consuming layers like:

- `Otm.Serializer.Tests`
- Integration or contract test suites

---

## 📄 License

This project is open source and licensed under the [MIT License](LICENSE).

---

## 🔗 Related Projects

- [`Otm.Serializer`](https://github.com/your-org/Otm.Serializer): Serialization layer for JSON and stream-based I/O
