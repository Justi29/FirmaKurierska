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
    public class AddressController : Controller
    {
        private readonly IAddressService _addressService;


        public AddressController(IAddressService addressService)
        {
            this._addressService = addressService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AddressDto>> Get()
        {
            var result = _addressService.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetAddress")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<AddressDto> Get(int id)
        {
            var result = _addressService.GetById(id);
            return Ok(result);
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id)
        {
            _addressService.Delete(id);
            return NoContent();
        }
    }
}

