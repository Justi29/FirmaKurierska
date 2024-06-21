using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using FirmaKurierska.Application.Services;
using FirmaKurierska.Domain.Exceptions;
using FirmaKurierska.Application.Dto;
using FirmaKurierska.Domain.Models;

namespace FirmaKurierska.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrderController> _logger;


        public OrderController(IOrderService orderService, ILogger<OrderController> logger)
        {
            this._orderService = orderService;
            this._logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<OrderDto>> Get()
        {
            _logger.LogDebug("Rozpoczęto pobieranie listy wszystkich zamówień");
            var result = _orderService.GetAll();
            _logger.LogDebug("Pobrano listę wszystkich zamówień");
            return Ok(result);
        }

        [HttpGet("client/{clientId}", Name = "GetOrdersForClient")]
        public ActionResult<IEnumerable<OrderDto>> GetForClient(int clientId)
        {
            _logger.LogDebug($"Rozpoczęto pobieranie listy wszystkich zamówień dla klienta ${clientId}");
            var result = _orderService.GetAllForClient(clientId);
            _logger.LogDebug($"Pobrano liste wszystkich zamówień dla klienta ${clientId}");
            return Ok(result);
        }

        [HttpGet("courier/{courierId}", Name = "GetOrdersForCourier")]
        public ActionResult<IEnumerable<OrderDto>> GetForCourier(int courierId)
        {
            _logger.LogDebug($"Rozpoczęto pobieranie listy wszystkich zamówień dla kuriera ${courierId}");
            var result = _orderService.GetAllForCourier(courierId);
            _logger.LogDebug($"Pobrano liste wszystkich zamówień dla kuriera ${courierId}");
            return Ok(result);
        }


        [HttpGet("{id}", Name = "GetOrder")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<OrderDto> Get(int id)
        {
            _logger.LogDebug($"Rozpoczęto pobieranie zamówienia o Id: ${id}");
            var result = _orderService.GetById(id);
            _logger.LogDebug($"Pobrano zamówienie o Id: {id}");
            return Ok(result);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Create([FromBody] CreateOrderDto dto)
        {
            _logger.LogDebug("Rozpoczęto tworzenie nowego zamówienia");
            var id = _orderService.Create(dto);

            var actionName = nameof(Get);
            var routeValues = new { id };
            _logger.LogDebug($"Utworzono nowe zamówienie z id = {id}");
            return CreatedAtAction(actionName, routeValues, null);
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id)
        {
            _logger.LogDebug($"Rozpoczęto usuwanie zamówienie o id = {id}");
            _orderService.Delete(id);
            _logger.LogDebug($"Usunieto zamówienie z id = {id}");
            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Update(int id, [FromBody] UpdateOrderDto dto)
        {
            _logger.LogDebug($"Rozpoczęto akutalizaowanie zamówienia z id = {id}");
            if (id != dto.Id)
            {
                throw new BadRequestException("Id param is not valid");
            }

            _orderService.Update(dto);
            _logger.LogDebug($"Zaktualizowano zamówienie z id = {id}");
            return NoContent();
        }
    }
}

