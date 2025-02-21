﻿using System.ComponentModel.DataAnnotations;

namespace Api.Contracts.Users
{
    public record RegisterUserRequest(
    [Required] string UserName,
    [Required] string Email,
    [Required] string Password);
}
