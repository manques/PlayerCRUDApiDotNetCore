using System.ComponentModel.DataAnnotations;

namespace asp.netCoreWebApi.models.Dto
{
    public class PlayerUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}
