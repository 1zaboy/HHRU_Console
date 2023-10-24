using AutoMapper;
using HHRU_Console.Core.Models;
using HHRU_Console.Data.Models;

namespace HHRU_Console.Core.Configuration;

internal class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserAccessData, UserEntity>();
    }
}
