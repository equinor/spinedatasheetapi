using System.Text.Json;
using System.Text.Json.Serialization;

namespace datasheetapi;
public class ListITagDataDtoConverter : JsonConverter<List<ITagDataDto>>
{
    public override List<ITagDataDto>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return new List<ITagDataDto>();
    }

    public override void Write(Utf8JsonWriter writer, List<ITagDataDto> value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        foreach (var tagDataDto in value)
        {
            writer.WriteStartObject();
            writer.WriteString("TagDataType", tagDataDto.GetType().Name);
            ITagDataDtoConverter.SerializeTagData(writer, tagDataDto, options);
            writer.WriteEndObject();
        }
        writer.WriteEndArray();
    }
}
