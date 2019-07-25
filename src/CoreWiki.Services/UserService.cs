namespace CoreWiki.Services
{
    using System.Collections.Generic;
    using AutoMapper;
    using Contracts;
    using Repository.Contracts;

    public class UserService : IUserService
    {
        private readonly IUserRepository usersRepository;
        private readonly IMapper mapper;

        public UserService(IUserRepository usersRepository, IMapper mapper)
        {
            this.usersRepository = usersRepository;
            this.mapper = mapper;
        }

        public IEnumerable<TModel> GetAllUsers<TModel>()
        {
            var users = this.usersRepository.Get();
            var model = this.mapper.Map<IEnumerable<TModel>>(users);

            return model;
        }
    }
}