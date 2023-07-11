using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace asp.netCoreWebApi.models.DTO
{
    public class PlayerNumberCreateDTO
    {
        [Required]
        public int PlayerNo { get; set; }

        [Required]
        public int PlayerID { get; set; }

        public string SpecialDetails { get; set; }
    }
}
