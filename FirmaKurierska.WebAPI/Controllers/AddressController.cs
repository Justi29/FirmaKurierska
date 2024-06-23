using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using FirmaKurierska.Application.Services;
using FirmaKurierska.Domain.Exceptions;
using FirmaKurierska.Application.Dto;
using Microsoft.Extensions.Logging;

namespace FirmaKurierska.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressController : Controller
    {
        private readonly IAddressService _addressService;
        private readonly ILogger<AddressController> _logger;

        public AddressController(IAddressService addressService, ILogger<AddressController> logger)
        {
            this._addressService = addressService;
            this._logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AddressDto>> Get()
        {
            _logger.LogDebug("Rozpoczęto pobieranie listy wszystkich adresów");
            var result = _addressService.GetAll();
            _logger.LogDebug("Pobrano listę wszystkich adresów");
            return Ok(result);
        }

        [HttpGet("client/{clientId}", Name = "GetAddressesForClient")]
        public ActionResult<IEnumerable<OrderDto>> GetForClient(int clientId)
        {
            _logger.LogDebug($"Rozpoczęto pobieranie listy wszystkich adresów dla klienta ${clientId}");
            var result = _addressService.GetAllForClient(clientId);
            _logger.LogDebug($"Pobrano liste wszystkich zamówień dla klienta ${clientId}");
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Create([FromBody] AddressDto dto)
        {
            _logger.LogDebug("Rozpoczęto tworzenie nowego adresu");
            var id = _addressService.Create(dto);

            _logger.LogDebug($"Utworzono nowegy adres z id = {id}");
            var actionName = nameof(Get);
            var routeValues = new { id };
            return CreatedAtAction(actionName, routeValues, null);
        }

        [HttpGet("{id}", Name = "GetAddress")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<AddressDto> Get(int id)
        {
            _logger.LogDebug($"Rozpoczęto pobieranie adresu o id: {id}");
            var result = _addressService.GetById(id);
            if (result == null)
            {
                _logger.LogWarning($"Adres o id {id} nie został znaleziony");
                return NotFound();
            }
            _logger.LogDebug($"Pobrano informacje o adresie o id {id}");
            return Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Update(int id, [FromBody] UpdateAddressDto dto)
        {
            _logger.LogDebug($"Rozpoczęto aktualizowanie informacji o kliencie z id = {id}");
            if (id != dto.Id)
            {
                _logger.LogWarning($"Nieprawidłowy parametr id: {id} w porównaniu do id DTO: {dto.Id}");
                throw new BadRequestException("Id param is not valid");
            }

            _addressService.Update(dto);
            _logger.LogDebug($"Zaktualizowano informacje o kliencie z id = {id}");
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id)
        {
            _logger.LogDebug($"Rozpoczęto usuwanie adresu o id = {id}");
            var address = _addressService.GetById(id);
            if (address == null)
            {
                _logger.LogWarning($"Adres o id {id} nie został znaleziony");
                return NotFound();
            }
            _addressService.Delete(id);
            _logger.LogDebug($"Usunięto adres o id = {id}");
            return NoContent();
        }
    }
}
