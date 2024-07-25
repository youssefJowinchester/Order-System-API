using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderSystem.Core.Models;
using OrderSystem.Core.Repositories.Interfaces;
using OrderSystem.Core.Specifications.CustomerSpecifications;
using OrderSystem.DTO;
using OrderSystem.Errors;
using OrderSystem.Helpers;

namespace OrderSystem.Controllers
{

    /// <summary>
    /// API controller for managing customers.
    /// </summary>

    public class CustomersController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #region Create Customer
        /// <summary>
        /// Creates a new customer.
        /// </summary>
        /// <param name="customerDto">Customer details</param>
        /// <returns>Status of the operation</returns>
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = _mapper.Map<Customer>(customerDto);
            await _unitOfWork.Repository<Customer>().AddAsync(customer);
            await _unitOfWork.CompleteAsync();

            var createdCustomerDto = _mapper.Map<CustomerDto>(customer);
            return CreatedAtAction(nameof(GetCustomerById), new { customerId = customer.Id }, createdCustomerDto);
        }
        #endregion


        #region Get Customer By Id
        /// <summary>
        /// Gets a customer by ID.
        /// </summary>
        /// <param name="customerId">Customer ID</param>
        /// <returns>Customer details</returns>
        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCustomerById(int customerId)
        {
            var customer = await _unitOfWork.Repository<Customer>().GetByIdAsync(customerId);
            if (customer == null)
            {
                return NotFound(new ApiResponse(404, "Customer not found."));
            }

            var customerDto = _mapper.Map<CustomerDto>(customer);
            return Ok(customerDto);
        }
        #endregion

        #region Get Customer Orders
        /// <summary>
        /// Gets orders of a specific customer.
        /// </summary>
        /// <param name="customerId">Customer ID</param>
        /// <param name="specParams">Specification parameters</param>
        /// <returns>List of orders</returns>
        [HttpGet("{customerId}/orders")]
        public async Task<ActionResult<Pagination<OrderDto>>> GetCustomerOrders(int customerId, [FromQuery] CustomerSpecParams specParams)
        {
            var spec = new CustomerWithOrdersSpecifications(customerId, specParams);
            var orders = await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(spec);

            var countSpec = new OrdersWithCountSpecifications(customerId, specParams);
            int count = await _unitOfWork.Repository<Order>().GetCountAsync(countSpec);

            var data = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderDto>>(orders);

            return Ok(new Pagination<OrderDto>(specParams.PageSize, specParams.PageIndex, count, data));
        }
        #endregion
    }


}
