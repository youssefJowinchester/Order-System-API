using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderSystem.Core.Models;
using OrderSystem.Core.Repositories.Interfaces;
using OrderSystem.DTO;
using OrderSystem.Errors;

namespace OrderSystem.Controllers
{

    /// <summary>
    /// API controller for managing invoices.
    /// </summary>
    public class InvoicesController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InvoicesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #region Get Invoice
        /// <summary>
        /// Gets an invoice by its ID.
        /// </summary>
        /// <param name="invoiceId">Invoice ID</param>
        /// <returns>Invoice details</returns>
        [HttpGet("{invoiceId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetInvoice(int invoiceId)
        {
            var invoice = await _unitOfWork.Repository<Invoice>().GetByIdAsync(invoiceId);
            if (invoice == null) return NotFound(new ApiResponse(404,"NO Invoices Here "));

            var invoiceDto = _mapper.Map<InvoiceDto>(invoice);
            return Ok(invoiceDto);
        }
        #endregion

        #region Get AllInvoices

        /// <summary>
        /// Gets all invoices.
        /// </summary>
        /// <returns>List of invoices</returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllInvoices()
        {
            var invoices = await _unitOfWork.Repository<Invoice>().GetAllAsync();
            var invoicesDto = _mapper.Map<IEnumerable<InvoiceDto>>(invoices);
            return Ok(invoicesDto);
        }

        #endregion
    }

}
