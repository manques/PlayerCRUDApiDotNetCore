using asp.netCoreWebApi.logging;
using asp.netCoreWebApi.models;
using asp.netCoreWebApi.models.DTO;
using asp.netCoreWebApi.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace asp.netCoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerNumberController : ControllerBase
    {
        // initialization
        private readonly ILogging _logger;
        private readonly IMapper _mapper;
        private readonly IPlayerNumberRepository _playerNumberRepository;
        private readonly IPlayerRepository _playerRepository;
        protected APIResponse _response;

        // constructor
        public PlayerNumberController(
            ILogging logger,
            IMapper mapper,
            IPlayerNumberRepository playerNumberRepository,
            IPlayerRepository playerRepository
        ) {
            this._response = new();
            _logger = logger;
            _mapper = mapper;
            _playerNumberRepository = playerNumberRepository;
            _playerRepository = playerRepository;
        }

        // get all
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetPlayerNumbers()
        {
            try
            {
                IEnumerable<PlayerNumber> playerList = await _playerNumberRepository.GetAllAsync();
                _response.Result = playerList;
                _response.StatusCode = HttpStatusCode.OK;
                _logger.LogInformation("Get Player List", "success");
                return Ok(_response);
            }
            catch (Exception ex)
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

        // get by id
        [HttpGet("{playerNo:int}", Name = "GetPlayerNumberById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetPlayerNumber(int playerNo)
        {
            try
            {
                // check player no
                if(playerNo <= 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string>() 
                    { 
                        "Invalid Player No. value"
                    };
                    return BadRequest(_response);
                }

                PlayerNumber playerNumber = 
                    await _playerNumberRepository.GetAsync(u => u.PlayerNo == playerNo);

                // player exist in db
                if(playerNumber == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string>()
                    { 
                        "Not found this Player No."
                    };
                    return NotFound(_response);
                }

                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = _mapper.Map<PlayerNumberDTO>(playerNumber);
                return Ok(_response);
            }
            catch(Exception ex)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>()
                { 
                    ex.ToString()
                };
                return _response; 
            }
        }

        // create player number
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> CreatePlayerNumber
        (
            PlayerNumberCreateDTO playerNumberCreateDTO
        )
        {
            try
            {
                // check field(s) value
                if(
                    playerNumberCreateDTO == null || 
                    !(playerNumberCreateDTO.PlayerNo > 0)
                )
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string>()
                    {
                        "Invalid Field(s) Value."
                    };
                    return BadRequest(_response);
                }

                PlayerNumber playerNumber = 
                    await _playerNumberRepository.GetAsync(u => u.PlayerNo == playerNumberCreateDTO.PlayerNo);

                // check player no.
                if(playerNumber != null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string>()
                    {
                        "Player No already exist."
                    };
                    return BadRequest(_response);
                }

                // check foreign key - playerID
                if(await _playerRepository.GetAsync(u => u.Id == playerNumberCreateDTO.PlayerID) == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string>()
                    {
                        "PlayerID not exist."
                    };
                    return BadRequest(_response);
                }

                PlayerNumber model = _mapper.Map<PlayerNumber>(playerNumberCreateDTO);
                await _playerNumberRepository.CreateAsync(model);
                _response.StatusCode = HttpStatusCode.Created;
                _response.Result = model;
                // return CreatedAtRoute("GetPlayerNumberById", new { PlayerNo: model.PlayerNo}, _response);
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
                return _response;
            }
        }

        // update 
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> updatePlayer(
            PlayerNumberUpdateDTO playerNumberUpdateDTO
        )
        {
            try
            {

                // check field(s) value
                if (
                    playerNumberUpdateDTO == null ||
                    !(playerNumberUpdateDTO.PlayerNo > 0)
                )
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string>()
                    {
                        "Invalid Field(s) Value."
                    };
                    return BadRequest(_response);
                }

                PlayerNumber playerNumber =
                    await _playerNumberRepository.GetAsync(
                        u => u.PlayerNo == playerNumberUpdateDTO.PlayerNo, 
                        tracked: false
                    );

                // check player no.
                if (playerNumber == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string>()
                    {
                        "Player No not exist."
                    };
                    return BadRequest(_response);
                }

                // check foreign key - playerID
                if (await _playerRepository.GetAsync(u => u.Id == playerNumberUpdateDTO.PlayerID) == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string>()
                    {
                        "PlayerID not exist."
                    };
                    return BadRequest(_response);
                }

                PlayerNumber model = _mapper.Map<PlayerNumber>(playerNumberUpdateDTO);
                await _playerNumberRepository.UpdateAsync(model);
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = model;
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
                return _response;
            }
        }

        // partial update
        [HttpPatch("{PlayerNo:int}", Name = "PartialUpdatePlayerNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> PartialUpdatePlayerNumber
        (
            int PlayerNo,
            JsonPatchDocument<PlayerNumberUpdateDTO> playerNumberPartialUpdateDTO
        )
        {
            try
            {
                // check field(s) value
                if (
                    playerNumberPartialUpdateDTO == null ||
                    PlayerNo == 0
                )
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string>()
                    {
                        "Invalid Field(s) Value."
                    };
                    return BadRequest(_response);
                }

                PlayerNumber playerNumber =
                    await _playerNumberRepository.GetAsync(u => u.PlayerNo == PlayerNo, tracked: false);
                PlayerNumberUpdateDTO playerNumberUpdateDTO = _mapper.Map<PlayerNumberUpdateDTO>(playerNumber);

                // check player no.
                if (playerNumber != null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string>()
                    {
                        "Player No already exist."
                    };

                }

                playerNumberPartialUpdateDTO.ApplyTo(playerNumberUpdateDTO, ModelState);
                if (!ModelState.IsValid)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                PlayerNumber model = _mapper.Map<PlayerNumber>(playerNumberUpdateDTO);
                await _playerNumberRepository.UpdateAsync(model);
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = model;
                return Ok( _response );
            }
            catch ( Exception ex )
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
                return _response;
            }

        }

        // delete player number
        [HttpDelete("{PlayerNo:int}", Name = "DeletePlayerNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> DeletePlayerNumber(int playerNo)
        {
            try{
                if (playerNo == 0)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                PlayerNumber playerNumber = await _playerNumberRepository.GetAsync(u => u.PlayerNo == playerNo);

                if (playerNumber == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                await _playerNumberRepository.RemoveAsync(playerNumber);
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
