using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace asp.netCoreWebApi.models
{
    public class PlayerNumber
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PlayerNo { get; set; }

        [ForeignKey("Player")]
        public int PlayerID { get; set; }
        public Player Player { get; set; }

        public string SpecialDetails { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
