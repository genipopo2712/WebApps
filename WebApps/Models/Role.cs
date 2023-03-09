using System.ComponentModel.DataAnnotations;

namespace WebApps.Models
{
    public class Role
    {
        public Guid RoleId { get; set; }
        [Required(ErrorMessage =" Please enter your {0}")]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }

    }
}
