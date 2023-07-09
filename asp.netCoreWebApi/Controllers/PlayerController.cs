using asp.netCoreWebApi.Data;
using asp.netCoreWebApi.logging;
using asp.netCoreWebApi.models;
using asp.netCoreWebApi.models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection;

namespace asp.netCoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        // logger
        //private readonly ILogger<PlayerController> _logger;
        //public PlayerController(ILogger<PlayerController> logger)
        //{
        //    _logger = logger;
        //}

        // custom logger
        private readonly ILogging _logger;
        private readonly ApplicationDbContext _db;
        public PlayerController(ILogging logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        // get player list
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<PlayerDTO>> GetPlayers()
        {
            _logger.LogInformation("get player list", "success");
            return Ok(_db.Players.ToList());
        }

        // get player by id
        [HttpGet("{id:int}", Name = "GetPlayer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(200, Type=typeof(PlayerDTO))]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(404)]
        public ActionResult<PlayerDTO> getPlayer(int id)
        {
            if (id == 0)
            {
                _logger.LogInformation("Bad request!", "error");
                return BadRequest();
            }
            var player = _db.Players.FirstOrDefault(u => u.Id == id);
            if (player == null)
            {
                return NotFound();
            }
            return Ok(player);
        }

        // add player in store
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PlayerDTO> AddPlayer([FromBody] PlayerDTO playerDTO)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            if (_db.Players.FirstOrDefault(u => u.Name.ToLower() == playerDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Player Already Exists!");
                return BadRequest(ModelState);
            }

            if (playerDTO == null)
            {
                return BadRequest(playerDTO);
            }

            if (playerDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            Player model = new()
            {
               Name = playerDTO.Name,
               CreatedAt = DateTime.Now
            };
            _db.Players.Add(model);
            _db.SaveChanges();
            return CreatedAtRoute("GetPlayer", new { id = playerDTO.Id }, playerDTO);
        }

        // update player in store
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PlayerDTO> UpdatePlayer([FromBody] PlayerDTO playerDTO)
        {

            if (playerDTO.Id > 0)
            {
                var player = _db.Players.FirstOrDefault(x => x.Id == playerDTO.Id);
                if (player == null)
                {
                    return NotFound();
                }
                Player model = new Player() { 
                    Name = player.Name,
                    CreatedAt = DateTime.Now
                };
                _db.Players.Update(model);
                _db.SaveChanges();
                return CreatedAtRoute("GetPlayer", new { id = playerDTO.Id }, playerDTO);
            }
            return BadRequest();
        }

        // update specific field(s) in player
        [HttpPatch("{id:int}", Name = "PartialUpdatePlayer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult PartialUpdatePlayer(int id, JsonPatchDocument<PlayerDTO> playerDTO)
        {
            if(playerDTO == null && id <= 0)
            {
                return BadRequest();
            }
            var player = _db.Players.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if(player == null)
            {
                return NotFound();
            }

            PlayerDTO playerDTOModel = new() { 
                Id = player.Id,
                Name = player.Name,
            };
            playerDTO.ApplyTo(playerDTOModel, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Player model = new Player() {
                Id = playerDTOModel.Id,
                Name = playerDTOModel.Name,
                CreatedAt = DateTime.Now,
            };

            _db.Players.Update(model);
            _db.SaveChanges();
            // return CreatedAtRoute("Get Player", new { id }, player);
            return Ok(player);

            //foreach (PropertyInfo prop in playerDTO.GetType().GetProperties())
            //{
            //    if (prop != null && prop.Name != null)
            //    {
            //        // player[prop.Name.ToString()] = prop.GetValue(playerDTO, null);
            //        // PlayerStore.playerList.ForEach(x => x[prop] = prop.GetValue(playerDTO, null));
            //        return CreatedAtRoute("GetPlayer", new { id = playerDTO.Id }, playerDTO);
            //    }
            //}
            
            
        }

        [HttpDelete("{id:int}", Name = "DeletePlayer")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeletePlayer(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var player = _db.Players.FirstOrDefault(u => u.Id == id);
            if(player == null)
            {
                return NotFound();
            }
            _db.Players.Remove(player);
            _db.SaveChanges();
            return NoContent();
        }

    }
}
