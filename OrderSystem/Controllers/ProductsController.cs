using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderSystem.Core.Models;
using OrderSystem.Core.Repositories.Interfaces;
using OrderSystem.Core.Specifications.ProductSpecifications;
using OrderSystem.DTO;
using OrderSystem.Errors;
using OrderSystem.Helpers;

namespace OrderSystem.Controllers
{
    /// <summary>
    /// API controller for managing products.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #region Get Products
        /// <summary>
        /// Gets all products.
        /// </summary>
        /// <param name="productSpec">Specification parameters</param>
        /// <returns>List of products</returns>
        [ProducesResponseType(typeof(Pagination<ProductDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductDto>>> GetProducts([FromQuery] ProductSpecParams productSpec)
        {
            var spec = new ProductWithSpecifications(productSpec);
            var products = await _unitOfWork.Repository<Product>().GetAllWithSpecAsync(spec);

            var data = _mapper.Map<IEnumerable<Product>, IReadOnlyList<ProductDto>>(products);

            var countSpec = new ProductWithFilterionForCountSpecifications();
            int count = await _unitOfWork.Repository<Product>().GetCountAsync(countSpec);

            return Ok(new Pagination<ProductDto>(productSpec.PageSize, productSpec.PageIndex, count, data));
        }
        #endregion

        #region Add Product
        /// <summary>
        /// Adds a new product.
        /// </summary>
        /// <param name="productDto">Product details</param>
        /// <returns>Status of the operation</returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddProduct([FromBody] ProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse(400, "Invalid product data."));
            }

            var product = _mapper.Map<Product>(productDto);
            await _unitOfWork.Repository<Product>().AddAsync(product);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetProductById), new { productId = product.Id }, productDto);
        }
        #endregion

        #region Get Product By Id
        /// <summary>
        /// Gets a product by its ID.
        /// </summary>
        /// <param name="productId">Product ID</param>
        /// <returns>Product details</returns>
        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            var product = await _unitOfWork.Repository<Product>().GetByIdAsync(productId);
            if (product == null)
            {
                return NotFound(new ApiResponse(404, "Product not found."));
            }

            var productDto = _mapper.Map<ProductDto>(product);
            return Ok(productDto);
        }
        #endregion

        #region Update Product
        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="productId">Product ID</param>
        /// <param name="productDto">Updated product details</param>
        /// <returns>Status of the operation</returns>
        [HttpPut("update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct(int productId, [FromBody] ProductDto productDto)
        {
            var product = await _unitOfWork.Repository<Product>().GetByIdAsync(productId);
            if (product == null)
            {
                return NotFound(new ApiResponse(404, "Product not found."));
            }

            _mapper.Map(productDto, product);
            await _unitOfWork.Repository<Product>().Update(product);
            await _unitOfWork.CompleteAsync();

            return Ok(productDto);
        }
        #endregion
    }


}
