using AutoMapper;
using Chat.Domain.Dtos;
using Chat.Domain.Models;


namespace Chat.Api.MappingProfile;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<Message, MessageDto>().ReverseMap();

    }
}