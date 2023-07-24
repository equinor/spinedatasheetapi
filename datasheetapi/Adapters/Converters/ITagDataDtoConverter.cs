using System.Text.Json;
using System.Text.Json.Serialization;

namespace datasheetapi;
public class ITagDataDtoConverter : JsonConverter<ITagDataDto>
{
    public override ITagDataDto? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return new TagDataDto();
    }

    public override void Write(Utf8JsonWriter writer, ITagDataDto value, JsonSerializerOptions options)
    {
        SerializeTagData(writer, value, options);
    }

    public static void SerializeTagData(Utf8JsonWriter writer, ITagDataDto tagDataDto, JsonSerializerOptions options)
    {
        if (tagDataDto is InstrumentTagDataDto instrumentTagDataDto)
        {
            JsonSerializer.Serialize(writer, instrumentTagDataDto, options);
        }
        else if (tagDataDto is ElectricalTagDataDto electricalTagDataDto)
        {
            JsonSerializer.Serialize(writer, electricalTagDataDto, options);
        }
        else if (tagDataDto is MechanicalTagDataDto mechanicalTagDataDto)
        {
            JsonSerializer.Serialize(writer, mechanicalTagDataDto, options);
        }
        else
        {
            throw new NotImplementedException();
        }
    }
}
