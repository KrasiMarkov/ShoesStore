using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static ShoesStore.Data.DataConstants.User;

namespace ShoesStore.Data.Models
{
    public class User : IdentityUser
    {
        [MaxLength(FullNameMaxLength)]
        public string FullName { get; set; }



    }
}
