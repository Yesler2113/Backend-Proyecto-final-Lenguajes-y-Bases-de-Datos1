using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Red_Social_Proyecto.Database;
using Red_Social_Proyecto.Dtos;
using Red_Social_Proyecto.Dtos.Security;
using Red_Social_Proyecto.Dtos.Task;
using Red_Social_Proyecto.Dtos.ValidationsDto;
using Red_Social_Proyecto.Entities;
using Red_Social_Proyecto.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Red_Social_Proyecto.Services
{
    

    public class UsersService : IUsersService
    {
        private readonly TodoListDBContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<UsersEntity> _userManager;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<UsersEntity> _signInManager;

        public UsersService(TodoListDBContext context,
            IMapper mapper,
            UserManager<UsersEntity> userManager,
            IConfiguration configuration,
            SignInManager<UsersEntity> signInManager
            )
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            this._configuration = configuration;
            this._signInManager = signInManager;
        }


        public async Task<ResponseDto<UsersDto>> CreateUserAsync(UsersCreateDto model)
        {
            Console.WriteLine($"Creating user with UserName: '{model.UserName}' and Email: '{model.Email}'");

            var user = new UsersEntity
            {
                UserName = model.UserName,
                Email = model.Email,
                RegistrationDate = DateTime.UtcNow,
                PasswordHash = model.Password,
                PhotoUrl = model.PhotoUrl,
                Biography = model.Biography,
                SocialMediaLinks = model.SocialMediaLinks
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var usersDto = _mapper.Map<UsersDto>(user);

                return new ResponseDto<UsersDto>
                {
                    Status = true,
                    StatusCode = 201,
                    Message = "Usuario Creado Correctamente",
                    Data = usersDto
                };
            }
            else
            {
                return new ResponseDto<UsersDto>
                {
                    Status = false,
                    StatusCode = 400,
                    Message = String.Join("; ", result.Errors.Select(e => e.Description)),
                    Data = null
                };
            }
        }



    }
}




