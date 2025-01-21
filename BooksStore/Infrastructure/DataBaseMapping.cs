using AutoMapper;
using Core.Models;
using Infrastructure.Entities;

namespace Infrastructure
{
    public class DataBaseMapping : Profile
    {
        public DataBaseMapping()
        {
            CreateMap<UserEntity, User>();
            CreateMap<BookEntity, Book>();
        }

    }
}
