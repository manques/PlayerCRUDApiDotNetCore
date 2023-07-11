using asp.netCoreWebApi.logging;
using asp.netCoreWebApi.models;
using asp.netCoreWebApi.models.Dto;
using asp.netCoreWebApi.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
        private readonly IPlayerRepository _playerRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public PlayerController(
            ILogging logger, 
            IPlayerRepository playerRepo,
            IMapper mapper
        )
        {
            _logger = logger;
            _playerRepo = playerRepo;
            _mapper = mapper;
            this._response = new();
        }

        // get player list
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetPlayers()
        {
            try
            {
                _logger.LogInformation("get player list", "success");
                IEnumerable<Player> playerList = await _playerRepo.GetAllAsync();
                _response.Result = _mapper.Map<List<PlayerDTO>>(playerList);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages = new List<String>() 
                { 
                    ex.ToString()
                };
                return _response;
            }
        }

        // get player by id
        [HttpGet("{id:int}", Name = "GetPlayer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(200, Type=typeof(PlayerDTO))]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(404)]
        public async Task<ActionResult<APIResponse>> getPlayer(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogInformation("Bad request!", "error");
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var player = await _playerRepo.GetAsync(u => u.Id == id);
                if (player == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<PlayerDTO>(player);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages = new List<String>()
                {
                    ex.ToString()
                };
                return _response;
            }
        }

        // add player in store
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> AddPlayer([FromBody] PlayerCreateDTO playerCreateDTO)
        {
            try
            {
                //if (!ModelState.IsValid)
                //{
                //    return BadRequest(ModelState);
                //}
                if (
                    await _playerRepo.GetAsync(
                        u => u.Name.ToLower() == playerCreateDTO.Name.ToLower()
                    )
                    != null
                )
                {
                    //ModelState.AddModelError("CustomError", "Player Already Exists!");
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages = new List<string>()
                    {
                        "Player Already Exists!"
                    };
                    return BadRequest(_response);
                }

                if (playerCreateDTO == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                Player player = _mapper.Map<Player>(playerCreateDTO);
                //Player model = new()
                //{
                //   Name = playerCreateDTO.Name,
                //   CreatedAt = DateTime.Now
                //};

                await _playerRepo.CreateAsync(player);
                _response.Result = player;
                _response.StatusCode = HttpStatusCode.Created;
                _response.IsSuccess = true;
                return CreatedAtRoute("GetPlayer", new { Id = player.Id }, _response);
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages = new List<string>() 
                { 
                    ex.ToString()
                };

                return _response;
            }
        }

        // update player in store
        [HttpPut("{id:int}", Name = "UpdatePlayer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> UpdatePlayer([FromBody] PlayerUpdateDTO playerUpdateDTO)
        {
            try
            {
                if (playerUpdateDTO == null || playerUpdateDTO.Id == 0)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var player = await _playerRepo.GetAsync(x => x.Id == playerUpdateDTO.Id, tracked: false);
                if (player == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                Player model = _mapper.Map<Player>(playerUpdateDTO);

                //Player model = new () { 
                //    Id = playerUpdateDTO.Id,
                //    Name = playerUpdateDTO.Name,
                //};

                await _playerRepo.UpdateAsync(model);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
                return _response;
            }
        }

        // update specific field(s) in player
        [HttpPatch("{id:int}", Name = "PartialUpdatePlayer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> PartialUpdatePlayer(
            int id, 
            JsonPatchDocument<PlayerUpdateDTO> playerPartialUpdateDTO
        )
        {
            try
            {
                if (playerPartialUpdateDTO == null || id == 0)
                {
                    return BadRequest();
                }

                var player = await _playerRepo.GetAsync(x => x.Id == id, tracked: false);
                PlayerUpdateDTO playerDTO = _mapper.Map<PlayerUpdateDTO>(player);
                //PlayerUpdateDTO playerDTOModel = new() { 
                //    Id = player.Id,
                //    Name = player.Name,
                //};
                if (player == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                playerPartialUpdateDTO.ApplyTo(playerDTO, ModelState);
                if (!ModelState.IsValid)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                Player model = _mapper.Map<Player>(playerDTO);
                //Player model = new Player() {
                //    Id = playerDTOModel.Id,
                //    Name = playerDTOModel.Name,
                //    CreatedAt = DateTime.Now,
                //};

                await _playerRepo.UpdateAsync(model);
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = model;
                // return CreatedAtRoute("Get Player", new { id }, player);
                return Ok(_response);

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
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages = new List<String>()
                {
                    ex.ToString()
                };
                return _response;
            }
        }

        [HttpDelete("{id:int}", Name = "DeletePlayer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<APIResponse>> DeletePlayer(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var player = await _playerRepo.GetAsync(u => u.Id == id);
                if (player == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _playerRepo.RemoveAsync(player);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages = new List<String>() 
                {
                    ex.ToString()
                };
                return _response;
            }
        }

    }
}
