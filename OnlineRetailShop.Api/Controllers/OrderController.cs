using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineRetailShop.Application.Interfaces;
using OnlineRetailShop.Utilities.CustomValidation;
using OnlineRetailShop.Domain.DTO;

namespace OnlineRetailShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        //GET : Get Product by Id
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetOrderById([FromRoute] Guid id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);

            if (order == null)
            {
                return NotFound();
            }
            
            return Ok(order);
        }

        //POST: Add order to the database
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> PlaceOrder(AddOrderRequestDto addOrderRequestDto)
        {
            var orderCreated = await _orderService.PlaceOrderAsync(addOrderRequestDto);
           
            return CreatedAtAction(nameof(GetOrderById), new { id = orderCreated.Id }, orderCreated);
        }


        //DELETE: Cancel Order by Id
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> CancelOrder([FromRoute] Guid id)
        {
            await _orderService.CancelOrderAsync(id);    
            
            return NoContent();
        }
    }
}
