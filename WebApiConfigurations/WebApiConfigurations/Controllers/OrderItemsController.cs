using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebApiConfigurations.DAL.EFCore;
using WebApiConfigurations.DTOs.OrderItemDTOs;
using WebApiConfigurations.Entities;

namespace WebApiConfigurations.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly AppDbContext _context;
        IMapper _mapper;

        public OrderItemsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrderItems()
        {
            var list = await _context.OrderItems.ToListAsync();
            return StatusCode((int)HttpStatusCode.OK, list);
        }

        [HttpGet]
        public async Task<IActionResult> GetByIdOrderItem(Guid id)
        {
            var existsOrderItem = await _context.OrderItems.FirstOrDefaultAsync(x => x.Id == id);
            if (existsOrderItem == null) 
                return NotFound();

            var result = _mapper.Map<GetOrderItemDTO>(existsOrderItem);

            return StatusCode((int)HttpStatusCode.Found, result);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrderItem(CreateOrderItemDTO createOrderItemDTO)
        {
            var orderItem = _mapper.Map<OrderItem>(createOrderItemDTO);

            await _context.OrderItems.AddAsync(orderItem);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOrderItem(Guid id)
        {
            var existsOrderItem = await _context.OrderItems.FirstOrDefaultAsync(x => x.Id == id);
            if(existsOrderItem == null) 
                return NotFound();

            _context.OrderItems.Remove(existsOrderItem);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrderItem(Guid id, UpdateOrderItemDTO updateOrderItemDTO)
        {
            var existsOrderItem = await _context.OrderItems.FirstOrDefaultAsync(x =>x.Id == id);
            if(existsOrderItem == null)
                return NotFound();

            _mapper.Map(updateOrderItemDTO, existsOrderItem);

            await _context.SaveChangesAsync();
            return NoContent();
        }

        
    }
}
