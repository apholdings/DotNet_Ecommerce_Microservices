using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Models;
using ProductAPI.Models.DTO.ProductDtos;
using ProductAPI.Repository;
using ProductAPI.Repository.IRepository;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace ProductAPI.Controllers.v1
{
	[Route("api/v{version:apiVersion}/ProductAPI")]
	[ApiController]
	[ApiVersion("1.0", Deprecated = false)]
	public class ProductAPIController : ControllerBase
	{
		protected APIResponse _response;
		private readonly ILogger<ProductAPIController> _logger;
		private readonly IProductRepository _dbProducts;
		private readonly IMapper _mapper;

		public ProductAPIController(IProductRepository dbProducts, ILogger<ProductAPIController> logger, IMapper mapper)
		{
			_dbProducts = dbProducts;
			_logger = logger;
			_mapper = mapper;
			_response = new();
		}

		private readonly IList<string> _validSortColumns = new List<string> { "owner", "name", "priceRange", "category", "onSale", "averageRating" };


		[HttpGet]
		[ResponseCache(CacheProfileName = "Default60")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<APIResponse>> GetAll([FromQuery] Pagination pagination, [Required] string sortColumn, bool sortOrder = false)
		{
			// Validate the pagination object
			if (!ModelState.IsValid)
			{
				_response.ErrorMessages.AddRange(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
				_response.IsSuccess = false;
				_response.StatusCode = HttpStatusCode.BadRequest;
				return BadRequest(_response);
			}

			// Validate the sortColumn parameter
			if (string.IsNullOrEmpty(sortColumn))
			{
				_response.ErrorMessages.Add("The sortColumn parameter is required.");
				_response.IsSuccess = false;
				_response.StatusCode = HttpStatusCode.BadRequest;
				return BadRequest(_response);
			}


			// Validate that the sortColumn value is valid
			if (!_validSortColumns.Contains(sortColumn))
			{
				_response.ErrorMessages.Add($"The sortColumn value '{sortColumn}' is not valid. Valid values are: {string.Join(", ", _validSortColumns)}.");
				_response.IsSuccess = false;
				_response.StatusCode = HttpStatusCode.BadRequest;
				return BadRequest(_response);
			}


			// Query the database and map the results to ProductDTO objects
			try
			{
				var products = await _dbProducts.GetAllAsync(pageSize: pagination.PageSize, pageNumber: pagination.PageNumber, sortColumn: sortColumn, sortOrder: sortOrder);
				var productDTOs = _mapper.Map<IEnumerable<ProductDTO>>(products);

				// Add the pagination information to the response headers
				Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));

				_response.Result = productDTOs;
				_response.StatusCode = HttpStatusCode.OK;

				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.ErrorMessages.Add($"An error occurred while querying the database: {ex.Message}");
				_response.IsSuccess = false;
				_response.StatusCode = HttpStatusCode.InternalServerError;
				return StatusCode((int)HttpStatusCode.InternalServerError, _response);
			}
		}


	}
}