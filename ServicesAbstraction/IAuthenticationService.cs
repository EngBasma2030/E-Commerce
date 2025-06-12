using Shared.DataTransferObject.IdentityDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface IAuthenticationService
    {
        // Login
        // Take Email and Password Then Return Token ,  Email and DisplayName To Client  
        Task<UserDto> LoginAsync(LoginDto loginDto);

        // Register
        // Take Email and Password and UserName , DisplayName And PhoneNumber
        // Then Return Token ,  Email and DisplayName To Client  
        Task<UserDto> RegisterAsync(RegisterDto registerDto);


    }
}
