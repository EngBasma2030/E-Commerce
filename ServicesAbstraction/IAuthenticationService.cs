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

        // Check Email
        // Take Email Then Return boolean To Client
        Task<bool> CheckEmailAsync(string email);

        // Get Current User Address
        // Email Then Return Address of Current Logged in User To Client
        Task<AddressDto> GetCurrentUserAddress(string email);

        // Update Current User Address Endpoint
        // Take Updated Address and Email Then Return Address after Update To Client  
        Task<AddressDto> UpdateCurrentUserAddress(string email, AddressDto addressDto);

        // Get Current User Endpoint
        // Take Email Then Return Token , Email and Display Name To Client  
        Task<UserDto> GetCurrentUserAsync(string email);

    }
}
