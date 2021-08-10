﻿
using System.Security.Claims;
using static ShoesStore.Areas.Admin.AdminConstants;

namespace ShoesStore.Data.Infrastructure
{
    public static class ClaimsPrincipalExtensions
    {
        public static string Id(this ClaimsPrincipal user) 
            => user.FindFirst(ClaimTypes.NameIdentifier).Value;

        public static bool IsAdmin(this ClaimsPrincipal user)
           => user.IsInRole(AdministratorRoleName);

    }
}
