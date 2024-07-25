using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderSystem.Core.Enums;
using OrderSystem.Core.Models;
using OrderSystem.Core.Models.Emailllll;
using OrderSystem.Core.Repositories.Interfaces;
using OrderSystem.Core.Services.Interfaces;
using OrderSystem.Core.Specifications.OrderSpecifications;
using OrderSystem.Core.Specifications.ProductSpecifications;
using OrderSystem.DTO;
using OrderSystem.Errors;
using OrderSystem.Helpers;

namespace OrderSystem.Controllers
{
    /// <summary>
    /// API controller for managing orders.
    /// </summary>
    public class OrdersController : BaseApiController
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;

        public OrdersController( IMapper mapper, IUnitOfWork unitOfWork,IOrderService orderService)
        {
           
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _orderService = orderService;

        }

        #region Get Order
        /// <summary>
        /// Gets all orders.
        /// </summary>
        /// <param name="orderSpec">Specification parameters</param>
        /// <returns>List of orders</returns>
        [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrder([FromQuery] OrderSpecParams orderSpec)
        {
            var spec = new OrderWithSpecifications(orderSpec);
            var orders = await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(spec);

            var data = _mapper.Map<IEnumerable<Order>, IReadOnlyList<OrderDto>>(orders);

            var countSpec = new OrderWithFilteringForCountSpecifications();
            int count = await _unitOfWork.Repository<Order>().GetCountAsync(countSpec);

            return Ok(new Pagination<OrderDto>(orderSpec.PageSize, orderSpec.PageIndex, count, data));
        }
        #endregion



        #region Get Order ById
        /// <summary>
        /// Gets an order by its ID.
        /// </summary>
        /// <param name="Id">Order ID</param>
        /// <returns>Order details</returns>
        [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("order")]
        public async Task<ActionResult<OrderDto>> GetOrderById(int Id, [FromQuery] OrderSpecParams orderSpec)
        {
            var spec = new OrderWithSpecifications(Id,orderSpec);
            var order = await _unitOfWork.Repository<Order>().GetWithSpecAsync(spec);
            var data = _mapper.Map<Order, OrderDto>(order);

            if (data == null) return NotFound(new ApiResponse(404, "No Order Here"));
            return Ok(data);
        }
        #endregion

        #region Create Order
        /// <summary>
        /// Creates a new order.
        /// </summary>
        /// <param name="orderDto">Order details</param>
        /// <returns>Status of the operation</returns>
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDTO orderDto)
        {
            if (orderDto == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = _mapper.Map<Order>(orderDto);

            if (order == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error mapping order DTO to order.");
            }

            await _orderService.CreateOrderAsync(order);

            return Ok(new { message = "Order created successfully" });
        }
        #endregion


        #region Update Order Status
        /// <summary>
        /// Updates the status of an existing order.
        /// </summary>
        /// <param name="orderId">Order ID</param>
        /// <param name="status">New order status</param>
        /// <returns>Status of the operation</returns>
        [HttpPut("{orderId}/status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, [FromBody] OrderStatus status)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse(400, "Don't Successfully Update"));
            }

            await _orderService.UpdateOrderStatusAsync(orderId, status);
            return Ok(new { message = "Order Status Updated Successfully" });
        }

        #endregion
    }



}
