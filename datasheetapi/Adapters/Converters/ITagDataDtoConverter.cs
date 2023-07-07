using System.Text.Json;
using System.Text.Json.Serialization;

namespace datasheetapi;
public class ITagDataDtoConverter : JsonConverter<List<ITagDataDto>>
{
    public override List<ITagDataDto>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, List<ITagDataDto> value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        foreach (var tagDataDto in value)
        {
            writer.WriteStartObject();
            writer.WriteString("TagDataType", tagDataDto.GetType().Name);
            SerializeTagData(writer, tagDataDto, options);
            writer.WriteEndObject();
        }
        writer.WriteEndArray();
    }

    private void SerializeTagData(Utf8JsonWriter writer, ITagDataDto tagDataDto, JsonSerializerOptions options)
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
