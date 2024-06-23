using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using FirmaKurierska.Application.Services;
using FirmaKurierska.Domain.Exceptions;
using FirmaKurierska.SharedKernel.Dto;
using FirmaKurierska.Domain.Models;

namespace FirmaKurierska.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CourierController : Controller
    {
        private readonly ICourierService _courierService;
        private readonly ILogger<CourierController> _logger;

        public CourierController(ICourierService courierService, ILogger<CourierController> logger)
        {
            this._courierService = courierService;
            this._logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CourierDto>> Get()
        {
            _logger.LogDebug("Rozpoczęto pobieranie listy wszystkich kurierów");
            var result = _courierService.GetAll();
            _logger.LogDebug("Pobrano listę wszystkich kurierów");
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Create([FromBody] CourierDto dto)
        {
            _logger.LogDebug("Rozpoczęto tworzenie nowego kuriera");
            var id = _courierService.Create(dto);

            _logger.LogDebug($"Utworzono nowego kuriera z id = {id}");
            var actionName = nameof(Get);
            var routeValues = new { id };
            return CreatedAtAction(actionName, routeValues, null);
        }

        [HttpGet("{id}", Name = "GetCourier")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CourierDto> Get(int id)
        {
            _logger.LogDebug($"Rozpoczęto pobieranie kuriera od id: ${id}");
            var result = _courierService.GetById(id);
            _logger.LogDebug($"Pobrano informacje o kurierze o id ${id}");
            return Ok(result);
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id)
        {
            _logger.LogDebug($"Rozpoczęto usuwanie kuriera o id = {id}");
            _courierService.Delete(id);
            _logger.LogDebug($"Usunieto zamówienie z id = {id}");
            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Update(int id, [FromBody] CourierDto dto)
        {
            _logger.LogDebug($"Rozpoczęto aktualizowanie informacji o kurierze z id = {id}");
            if (id != dto.Id)
            {
                throw new BadRequestException("Id param is not valid");
            }

            _courierService.Update(dto);
            _logger.LogDebug($"Zaktualizowano informacje o kurierze z id = {id}");
            return NoContent();
        }
    }
}

