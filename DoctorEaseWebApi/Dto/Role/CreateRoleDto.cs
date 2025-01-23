using System.ComponentModel.DataAnnotations;

namespace DEWebApi.Dto.Role
{
    public class CreateRoleDto
    {
        [Required]
        public string Description { get; set; } = string.Empty;
    }
}
