
using AutoMapper;

namespace datasheetapi.Helpers;
//TODO: Refactor this class
public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateCommentDto, Conversation>();

        CreateMap<MessageDto, Message>()
         .ForMember(des => des.CreatedDate, opt => opt.MapFrom(_ => DateTime.UtcNow))
         .ForMember(des => des.ModifiedDate, opt => opt.MapFrom(_ => DateTime.UtcNow));
        CreateMap<Message, GetMessageDto>()
         .ForMember(des => des.Text, opt => opt.MapFrom(src => src.SoftDeleted ? "" : src.Text));

        CreateMap<CreateCommentDto, Message>()
         .ForMember(des => des.CreatedDate, opt => opt.MapFrom(_ => DateTime.UtcNow))
         .ForMember(des => des.ModifiedDate, opt => opt.MapFrom(_ => DateTime.UtcNow));

        CreateMap<Conversation, Participant>()
         .ForMember(des => des.CreatedDate, opt => opt.MapFrom(_ => DateTime.UtcNow))
         .ForMember(des => des.ModifiedDate, opt => opt.MapFrom(_ => DateTime.UtcNow));

        CreateMap<Participant, UserDto>()
         .ForMember(des => des.UserId, opt => opt.MapFrom(src => src.UserId));

        CreateMap<Conversation, GetConversationDto>()
         .ForMember(dest => dest.UserDtos, opt => opt.MapFrom(src => src.Participants))
         .ForMember(dest => dest.MessageDtos, opt => opt.MapFrom(src => src.Messages));
    }
}