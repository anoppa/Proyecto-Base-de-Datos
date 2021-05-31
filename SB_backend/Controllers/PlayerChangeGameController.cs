﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SB_backend.Interfaces;
using SB_backend.Models;

namespace SB_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerChangeGameController : ControllerBase
    {
        private IPlayerChangeGameRepository _pchRep;
        public PlayerChangeGameController(IPlayerChangeGameRepository repo)
        {
            _pchRep = repo;
        }
        [HttpGet("{GameID}/WinerTeam")]    
        public IActionResult GetGameChangesWinerTeam(Guid GameId)
        {
            var changes = _pchRep.GetPlayersChangesInGameWinerTeam(GameId);
            if (changes == null)
                return NotFound($"Not Changes Found in Game {GameId}");
            return Ok(changes);
        }
        [HttpGet("{GameID}/LoserTeam")]
        public IActionResult GetGameChangesLoserTeam(Guid GameId)
        {
            var changes = _pchRep.GetPlayersChangesInGameLoserTeam(GameId);
            if (changes == null)
                return NotFound($"Not Changes Found in Game {GameId}");
            return Ok(changes);
        }
        [HttpPost]
        public IActionResult AddChange(PlayerChangeGame change)
        {
            var changeA = _pchRep.AddChangeInGame(change);
            if (changeA == null)
                return BadRequest("Not created Object");
            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + change.GameGameId + "/" + change.PlayerInId + "/" + change.PlayerInPositionId, change);
        }
        [HttpDelete("{GameId}/{PlayerInId}/{PositionId}")]
        public IActionResult RemoveChange(Guid GameId, Guid PlayerInId, Guid PositionId,PlayerChangeGame change)
        {
            var rem = _pchRep.RemoveChangeInGame(change);
            if (rem)
                return Ok();
            return NotFound("Not change Found");
        }
    }
}