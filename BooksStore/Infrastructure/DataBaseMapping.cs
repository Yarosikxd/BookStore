using AutoMapper;
using Core.Models;
using Infrastructure.Entities;

namespace Infrastructure
{
    public class DataBaseMappings : Profile
    {
        public DataBaseMappings()
        {
            CreateMap<UserEntity, User>();
            CreateMap<BookEntity, Book>();
        }
    }
}
