using System.ComponentModel.DataAnnotations;

namespace asp.netCoreWebApi.models.Dto
{
    public class PlayerCreateDTO
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}
