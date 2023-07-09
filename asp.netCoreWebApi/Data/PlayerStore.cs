using asp.netCoreWebApi.models.Dto;

namespace asp.netCoreWebApi.Data
{
    public static class PlayerStore
    {
        public static List<PlayerDTO> playerList = new List<PlayerDTO> {
            new PlayerDTO{ Id = 1, Name = "anup" },
            new PlayerDTO{ Id= 2 , Name = "vivek" },
            new PlayerDTO{ Id = 3, Name = "abhishek" },
            new PlayerDTO{ Id= 4 , Name = "sushil" }
        };
    }
}
