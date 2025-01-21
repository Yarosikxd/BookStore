using AutoMapper;
using Core.Abstraction.Repository;
using Core.Models;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositoryes
{
    public class UserRepository : IUserRepository
    {
        private readonly DataBaseDbContext _context;
        private readonly IMapper _mapper;

        public UserRepository(DataBaseDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task CreateUserAsync(User user)
        {
            try
            {
                var userEnriry = new UserEntity()
                {
                    Id = user.Id,
                    Name = user.Name,
                    PasswordHash = user.PasswordHash,
                    Email = user.Email,

                };

                await _context.Users.AddAsync(userEnriry);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) 
            {
                throw new Exception("Failed to create user", ex);
            }
        }

        public async Task<User> GetByEmailUserAsync(string email)
        {
            try
            {
                var userEntity = await _context.Users
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.Email == email);
                if(userEntity == null)
                {
                    throw new Exception ($"User with email '{email}' not found");
                }

                return _mapper.Map<User>(userEntity);
            }
            catch(Exception ex) 
            {
                throw new Exception($"Failed to get user by email '{email}'", ex);
            }
        }
    }
}
