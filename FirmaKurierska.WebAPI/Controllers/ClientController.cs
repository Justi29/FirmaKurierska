using System;
using Microsoft.AspNetCore.Mvc;
using FirmaKurierska.Application.Services;
using FirmaKurierska.Domain.Exceptions;
using FirmaKurierska.Application.Dto;
using System.Collections.Generic;

namespace FirmaKurierska.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : Controller
    {
        private readonly IClientService _clientService;


        public ClientController(IClientService clientService)
        {
            this._clientService = clientService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ClientDto>> Get()
        {
            var result = _clientService.GetAll();
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Create([FromBody] ClientDto dto)
        {
            var id = _clientService.Create(dto);

            //_logger.LogDebug($"Utworzono nowe zamówienie z id = {id}");
            var actionName = nameof(Get);
            var routeValues = new { id };
            return CreatedAtAction(actionName, routeValues, null);
        }

        [HttpGet("{id}", Name = "GetClient")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ClientDto> Get(int id)
        {
            var result = _clientService.GetById(id);
            return Ok(result);
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id)
        {
            _clientService.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Update(int id, [FromBody] ClientDto dto)
        {
            if (id != dto.Id)
            {
                throw new BadRequestException("Id param is not valid");
            }

            _clientService.Update(dto);
            //_logger.LogDebug($"Zaktualizowano zamówienie z id = {id}");
            return NoContent();
        }
    }
}

