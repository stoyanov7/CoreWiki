namespace CoreWiki.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Contracts;
    using Microsoft.AspNetCore.Identity;
    using Repository.Contracts;

    public class UserService : IUserService
    {
        private readonly IUserRepository usersRepository;
        private readonly IMapper mapper;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserService(
            IUserRepository usersRepository,
            IMapper mapper,
            RoleManager<IdentityRole> roleManager)
        {
            this.usersRepository = usersRepository;
            this.mapper = mapper;
            this.roleManager = roleManager;
        }

        public IEnumerable<TModel> GetAllUsers<TModel>()
        {
            var users = this.usersRepository.Get();
            var model = this.mapper.Map<IEnumerable<TModel>>(users);

            return model;
        }

        public ICollection<string> GetAllRoleNames() =>
            this.roleManager
                .Roles
                .Select(x => x.Name)
                .ToList();
    }
}