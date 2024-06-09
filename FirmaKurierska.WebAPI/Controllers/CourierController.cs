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
    }
}

