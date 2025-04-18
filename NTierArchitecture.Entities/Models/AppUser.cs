﻿using Microsoft.AspNetCore.Identity;

namespace NTierArchitecture.Entities.Models;
public sealed class AppUser : IdentityUser<Guid>
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
}
