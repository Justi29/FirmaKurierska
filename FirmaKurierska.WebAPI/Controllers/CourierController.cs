using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using FirmaKurierska.Application.Services;
using FirmaKurierska.Domain.Exceptions;
using FirmaKurierska.Application.Dto;

namespace FirmaKurierska.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CourierController : Controller
    {
        private readonly ICourierService _courierService;


        public CourierController(ICourierService courierService)
        {
            this._courierService = courierService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CourierDto>> Get()
        {
            var result = _courierService.GetAll();
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Create([FromBody] CourierDto dto)
        {
            var id = _courierService.Create(dto);

            //_logger.LogDebug($"Utworzono nowe zamówienie z id = {id}");
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
            var result = _courierService.GetById(id);
            return Ok(result);
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id)
        {
            _courierService.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Update(int id, [FromBody] CourierDto dto)
        {
            if (id != dto.Id)
            {
                throw new BadRequestException("Id param is not valid");
            }

            _courierService.Update(dto);
            //_logger.LogDebug($"Zaktualizowano zamówienie z id = {id}");
            return NoContent();
        }
    }
}

