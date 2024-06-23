using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using FirmaKurierska.Application.Services;
using FirmaKurierska.Domain.Exceptions;
using FirmaKurierska.SharedKernel.Dto;
using Microsoft.Extensions.Logging;

namespace FirmaKurierska.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : Controller
    {
        private readonly IClientService _clientService;
        private readonly ILogger<ClientController> _logger;

        public ClientController(IClientService clientService, ILogger<ClientController> logger)
        {
            this._clientService = clientService;
            this._logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ClientDto>> Get()
        {
            _logger.LogDebug("Rozpoczęto pobieranie listy wszystkich klientów");
            var result = _clientService.GetAll();
            _logger.LogDebug("Pobrano listę wszystkich klientów");
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Create([FromBody] ClientDto dto)
        {
            _logger.LogDebug("Rozpoczęto tworzenie nowego klienta");
            var id = _clientService.Create(dto);

            _logger.LogDebug($"Utworzono nowego klienta z id = {id}");
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
            _logger.LogDebug($"Rozpoczęto pobieranie klienta o id: {id}");
            var result = _clientService.GetById(id);
            if (result == null)
            {
                _logger.LogWarning($"Klient o id {id} nie został znaleziony");
                return NotFound();
            }
            _logger.LogDebug($"Pobrano informacje o kliencie o id {id}");
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id)
        {
            _logger.LogDebug($"Rozpoczęto usuwanie klienta o id = {id}");
            var client = _clientService.GetById(id);
            if (client == null)
            {
                _logger.LogWarning($"Klient o id {id} nie został znaleziony");
                return NotFound();
            }
            _clientService.Delete(id);
            _logger.LogDebug($"Usunięto klienta o id = {id}");
            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Update(int id, [FromBody] ClientDto dto)
        {
            _logger.LogDebug($"Rozpoczęto aktualizowanie informacji o kliencie z id = {id}");
            if (id != dto.Id)
            {
                _logger.LogWarning($"Nieprawidłowy parametr id: {id} w porównaniu do id DTO: {dto.Id}");
                throw new BadRequestException("Id param is not valid");
            }

            _clientService.Update(dto);
            _logger.LogDebug($"Zaktualizowano informacje o kliencie z id = {id}");
            return NoContent();
        }
    }
}
