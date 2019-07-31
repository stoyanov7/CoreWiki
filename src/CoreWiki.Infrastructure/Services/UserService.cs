namespace CoreWiki.Infrastructure.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Domain.Repository;
    using Domain.Services;
    using Microsoft.AspNetCore.Identity;
    using Models.Identity;

    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserRepository usersRepository;
        private readonly IMapper mapper;

        public UserService(
            UserManager<ApplicationUser> userManager,
            IUserRepository usersRepository,
            IMapper mapper)
        {
            this.userManager = userManager;
            this.usersRepository = usersRepository;
            this.mapper = mapper;
        }

        public TModel FindByEmail<TModel>(string email)
        {
            var user = this.usersRepository
                .Get()
                .FirstOrDefault(x => x.Email == email);

            var model = this.mapper.Map<TModel>(user);

            return model;
        }

        public IEnumerable<TModel> GetAllUsers<TModel>()
        {
            var users = this.usersRepository.Get();
            var model = this.mapper.Map<IEnumerable<TModel>>(users);

            return model;
        }

        public async Task<bool> UpdateUserRolesAsync(string email, ICollection<string> roleNames, IEnumerable<string> updatedRoles)
        {
            var user = await this.userManager.FindByEmailAsync(email);

            if (user is null)
            {
                return false;
            }
            else
            {
                foreach (var role in roleNames)
                {
                    if (updatedRoles.Contains(role))
                    {
                        await this.userManager.AddToRoleAsync(user, role);
                    }
                    else
                    {
                        await this.userManager.RemoveFromRoleAsync(user, role);
                    }
                }

                return true;
            }
        }
    }
}