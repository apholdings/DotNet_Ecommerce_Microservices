using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Models;
using ProductAPI.Models.DTO.ProductDtos;
using ProductAPI.Repository;
using ProductAPI.Repository.IRepository;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
		private readonly ICategoryRepository _dbCategories;
		private readonly IMapper _mapper;

		public ProductAPIController(
			IProductRepository dbProducts, 
			ICategoryRepository dbCategories, 
			ILogger<ProductAPIController> logger, 
			IMapper mapper)
		{
			_dbProducts = dbProducts;
			_dbCategories = dbCategories;
			_logger = logger;
			_mapper = mapper;
			_response = new();
		}

		private readonly IList<string> _validSortColumns = new List<string> { "owner", "name", "priceRange", "category", "onSale", "averageRating" };


		[HttpGet(Name = "ListProducts")]
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

				// Calculate the total number of pages and total number of items
				int totalItems = products.Count();
				int totalPages = (int)Math.Ceiling((double)totalItems / pagination.PageSize);

				// Add the pagination information to the response headers
				Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(new { PageNumber = pagination.PageNumber, PageSize = pagination.PageSize, TotalItems = totalItems, TotalPages = totalPages }));

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



		[HttpGet("search")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<APIResponse>> Search([FromQuery] string searchQuery, [FromQuery] Pagination pagination)
		{
			// Validate the searchQuery parameter
			if (string.IsNullOrEmpty(searchQuery))
			{
				_response.ErrorMessages.Add("The searchQuery parameter is required.");
				_response.IsSuccess = false;
				_response.StatusCode = HttpStatusCode.BadRequest;
				return BadRequest(_response);
			}

			// Validate the pagination object
			if (!ModelState.IsValid)
			{
				_response.ErrorMessages.AddRange(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
				_response.IsSuccess = false;
				_response.StatusCode = HttpStatusCode.BadRequest;
				return BadRequest(_response);
			}

			try
			{

				// Query the database for products matching the search query
				var products = await _dbProducts.SearchProductsAsync(searchQuery, pagination);
				var productDTOs = _mapper.Map<IEnumerable<ProductDTO>>(products);

				// Add the pagination information to the response headers
				Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));

				// Return the result in the response
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


		[HttpGet("{productId}", Name = "GetProduct")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<APIResponse>> GetProduct(int id)
		{
			// Get the product from the repository
			var product = await _dbProducts.GetProductByIdAsync(id, false);

			// Check if the product was found
			if (product == null)
			{
				_response.ErrorMessages.Add($"Product with id '{id}' was not found.");
				_response.IsSuccess = false;
				_response.StatusCode = HttpStatusCode.NotFound;
				return NotFound(_response);
			}

			try
			{
				// Map the product to a product DTO
				var productDto = _mapper.Map<ProductDTO>(product);

				// Set the response data and return it
				_response.Result = productDto;
				_response.IsSuccess = true;
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

		// CREATE
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<APIResponse>> CreateProduct([FromBody] ProductCreateDTO createDTO)
		{
			try
			{
				if (createDTO == null)
				{
					return BadRequest(createDTO);
				}

				// Check if the category exists
				//var category = await _dbCategories.GetByIdAsync(createDTO.CategoryId);
				//if (category == null)
				//{
				//	return BadRequest("Invalid category");
				//}

				//	// Check if the user is authorized to create a product
				//	// You'll need to implement this check yourself
				//	if (!await IsAuthorizedToCreateProduct())
				//	{
				//		return Unauthorized();
				//	}

				//	// Set the owner id of the product to the current user's id
				//	// You'll need to implement this yourself
				//	model.OwnerId = GetCurrentUserId();

				Product product = _mapper.Map<Product>(createDTO);

				await _dbProducts.CreateProductAsync(product);

				_response.Result = _mapper.Map<ProductDTO>(product);
				_response.StatusCode = HttpStatusCode.Created;
				_response.IsSuccess = true;
				return CreatedAtRoute("GetProduct", new { id = product.ProductId }, _response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.StatusCode = HttpStatusCode.BadRequest;
				_response.ErrorMessages = new List<string>() { ex.ToString() };
			}
			return _response;
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteProduct(int id)
		{
			// Get the product from the database
			var product = await _dbProducts.GetProductByIdAsync(id);

			// Check if the product exists
			if (product == null)
			{
				return NotFound();
			}

			// Remove the product and its associated images and videos
			bool success = await _dbProducts.RemoveProductAsync(product);

			if (!success)
			{
				return StatusCode(500, "An error occurred while deleting the product.");
			}

			return NoContent();
		}


		[HttpPut("{id:int}", Name = "UpdateProduct")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<APIResponse>> UpdateProduct(int id, string userId, [FromBody] ProductUpdateDTO updateDTO)
		{
			var response = new APIResponse();
			try
			{
				// Validate the request data
				if (updateDTO == null || id != updateDTO.ProductId)
				{
					response.StatusCode = HttpStatusCode.BadRequest;
					response.IsSuccess = false;
					return response;
				}

				// Check if the product exists
				var existingProduct = await _dbProducts.GetByIdAsync(id);
				if (existingProduct == null)
				{
					response.StatusCode = HttpStatusCode.NotFound;
					response.IsSuccess = false;
					return response;
				}

				// Map the updateDTO to the existing product using AutoMapper
				_mapper.Map(updateDTO, existingProduct);

				// Update the product
				await _dbProducts.UpdateAsync(existingProduct, userId);

				response.StatusCode = HttpStatusCode.NoContent;
				response.IsSuccess = true;
				return CreatedAtRoute("UpdateProduct", null, _response);
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.StatusCode = HttpStatusCode.BadRequest;
				response.ErrorMessages = new List<string>() { ex.ToString() };
			}
			return response;
		}


		[HttpGet("category/{categoryId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<APIResponse>> GetProductsForCategoryAsync(int categoryId, [FromQuery] Pagination pagination, [Required] string sortColumn, bool sortOrder = false)
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
				var products = await _dbProducts.GetProductsForCategoryAsync(categoryId, pageSize: pagination.PageSize, pageNumber: pagination.PageNumber, sortColumn: sortColumn, sortOrder: sortOrder);
				var productDTOs = _mapper.Map<IEnumerable<ProductDTO>>(products);


				// Calculate the total number of pages and total number of items
				int totalItems = products.Count();
				int totalPages = (int)Math.Ceiling((double)totalItems / pagination.PageSize);

				// Add the pagination information to the response headers
				Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(new { PageNumber = pagination.PageNumber, PageSize = pagination.PageSize, TotalItems = totalItems, TotalPages = totalPages }));

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

		[HttpGet("listByAverageRating")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<IEnumerable<Product>>> ListByAverageRating([Required] double averageRating, [FromQuery] Pagination pagination, [Required] string sortColumn, bool sortOrder = false, bool tracked = true)
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
				var products = await _dbProducts.GetProductsByAverageRatingAsync(averageRating, pageSize: pagination.PageSize, pageNumber: pagination.PageNumber, sortColumn: sortColumn, sortOrder: sortOrder,tracked);
				var productDTOs = _mapper.Map<IEnumerable<ProductDTO>>(products);

				// Calculate the total number of pages and total number of items
				int totalItems = products.Count();
				int totalPages = (int)Math.Ceiling((double)totalItems / pagination.PageSize);

				// Add the pagination information to the response headers
				Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(new { PageNumber = pagination.PageNumber, PageSize = pagination.PageSize, TotalItems = totalItems, TotalPages = totalPages }));

				_response.Result = productDTOs;
				_response.StatusCode = HttpStatusCode.OK;

				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.ErrorMessages.Add(ex.Message);
				_response.IsSuccess = false;
				_response.StatusCode = HttpStatusCode.InternalServerError;
				return StatusCode(500, _response);
			}
		}

		[HttpGet("listByAverageTimeSpent")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]

		public async Task<ActionResult<IEnumerable<Product>>> ListByAverageTimeSpent([Required] int averageTimeSpent, [FromQuery] Pagination pagination, [Required] string sortColumn = "", bool sortOrder = false, bool tracked = true)
		{
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
				var products = await _dbProducts.GetProductsByAvgTimeSpentAsync(averageTimeSpent, pageSize: pagination.PageSize, pageNumber: pagination.PageNumber, sortColumn, sortOrder, tracked);
				var productDTOs = _mapper.Map<IEnumerable<ProductDTO>>(products);

				// Calculate the total number of pages and total number of items
				int totalItems = products.Count();
				int totalPages = (int)Math.Ceiling((double)totalItems / pagination.PageSize);

				// Add the pagination information to the response headers
				Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(new { PageNumber = pagination.PageNumber, PageSize = pagination.PageSize, TotalItems = totalItems, TotalPages = totalPages }));

				_response.Result = productDTOs;
				_response.StatusCode = HttpStatusCode.OK;

				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.ErrorMessages.Add(ex.Message);
				_response.IsSuccess = false;
				_response.StatusCode = HttpStatusCode.InternalServerError;
				return StatusCode(500, _response);
			}
		}

		[HttpGet("listByCTR")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]

		public async Task<ActionResult<IEnumerable<Product>>> ListByCTR([Required] double clickThroughRate,  [FromQuery] Pagination pagination, [Required] string sortColumn = "", bool sortOrder = false, bool tracked = true)
		{
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
				var products = await _dbProducts.GetProductsByClickThroughRateAsync(clickThroughRate, pageSize: pagination.PageSize, pageNumber: pagination.PageNumber, sortColumn: sortColumn, sortOrder: sortOrder, tracked);
				var productDTOs = _mapper.Map<IEnumerable<ProductDTO>>(products);

				// Calculate the total number of pages and total number of items
				int totalItems = products.Count();
				int totalPages = (int)Math.Ceiling((double)totalItems / pagination.PageSize);

				// Add the pagination information to the response headers
				Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(new { PageNumber = pagination.PageNumber, PageSize = pagination.PageSize, TotalItems = totalItems, TotalPages = totalPages }));

				_response.Result = productDTOs;
				_response.StatusCode = HttpStatusCode.OK;

				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.ErrorMessages.Add(ex.Message);
				_response.IsSuccess = false;
				_response.StatusCode = HttpStatusCode.InternalServerError;
				return StatusCode(500, _response);
			}
		}

		[HttpGet("listByCR")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]

		public async Task<ActionResult<IEnumerable<Product>>> ListByCR([Required] double conversionRate, [FromQuery] Pagination pagination, [Required] string sortColumn = "", bool sortOrder = false, bool tracked = true)
		{
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
				var products = await _dbProducts.GetProductsByConversionRateAsync(conversionRate, pageSize: pagination.PageSize, pageNumber: pagination.PageNumber, sortColumn: sortColumn, sortOrder: sortOrder, tracked);
				var productDTOs = _mapper.Map<IEnumerable<ProductDTO>>(products);

				// Calculate the total number of pages and total number of items
				int totalItems = products.Count();
				int totalPages = (int)Math.Ceiling((double)totalItems / pagination.PageSize);

				// Add the pagination information to the response headers
				Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(new { PageNumber = pagination.PageNumber, PageSize = pagination.PageSize, TotalItems = totalItems, TotalPages = totalPages }));

				_response.Result = productDTOs;
				_response.StatusCode = HttpStatusCode.OK;

				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.ErrorMessages.Add(ex.Message);
				_response.IsSuccess = false;
				_response.StatusCode = HttpStatusCode.InternalServerError;
				return StatusCode(500, _response);
			}
		}

		[HttpGet("listByManufacturer")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]

		public async Task<ActionResult<IEnumerable<Product>>> ListByManufacturer([Required] string manufacturer, [FromQuery] Pagination pagination, [Required] string sortColumn = "", bool sortOrder = false, bool tracked = true)
		{
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
				var products = await _dbProducts.GetProductsByManufacturerAsync(manufacturer, pageSize: pagination.PageSize, pageNumber: pagination.PageNumber, sortColumn: sortColumn, sortOrder: sortOrder, tracked);
				var productDTOs = _mapper.Map<IEnumerable<ProductDTO>>(products);

				// Calculate the total number of pages and total number of items
				int totalItems = products.Count();
				int totalPages = (int)Math.Ceiling((double)totalItems / pagination.PageSize);

				// Add the pagination information to the response headers
				Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(new { PageNumber = pagination.PageNumber, PageSize = pagination.PageSize, TotalItems = totalItems, TotalPages = totalPages }));

				_response.Result = productDTOs;
				_response.StatusCode = HttpStatusCode.OK;

				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.ErrorMessages.Add(ex.Message);
				_response.IsSuccess = false;
				_response.StatusCode = HttpStatusCode.InternalServerError;
				return StatusCode(500, _response);
			}
		}


		[HttpGet("listByNumLikes")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]

		public async Task<ActionResult<IEnumerable<Product>>> ListByNumLikes([Required] int numLikes, [FromQuery] Pagination pagination, [Required] string sortColumn = "", bool sortOrder = false, bool tracked = true)
		{
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
				var products = await _dbProducts.GetProductsByNumLikesAsync(numLikes, pageSize: pagination.PageSize, pageNumber: pagination.PageNumber, sortColumn: sortColumn, sortOrder: sortOrder, tracked);
				var productDTOs = _mapper.Map<IEnumerable<ProductDTO>>(products);

				// Calculate the total number of pages and total number of items
				int totalItems = products.Count();
				int totalPages = (int)Math.Ceiling((double)totalItems / pagination.PageSize);

				// Add the pagination information to the response headers
				Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(new { PageNumber = pagination.PageNumber, PageSize = pagination.PageSize, TotalItems = totalItems, TotalPages = totalPages }));

				_response.Result = productDTOs;
				_response.StatusCode = HttpStatusCode.OK;

				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.ErrorMessages.Add(ex.Message);
				_response.IsSuccess = false;
				_response.StatusCode = HttpStatusCode.InternalServerError;
				return StatusCode(500, _response);
			}
		}

		[HttpGet("listByNumPurchases")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]

		public async Task<ActionResult<IEnumerable<Product>>> ListByNumPurchases([Required] int numPurchases, [FromQuery] Pagination pagination, [Required] string sortColumn = "", bool sortOrder = false, bool tracked = true)
		{
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
				var products = await _dbProducts.GetProductsByNumPurchasesAsync(numPurchases, pageSize: pagination.PageSize, pageNumber: pagination.PageNumber, sortColumn: sortColumn, sortOrder: sortOrder, tracked);
				var productDTOs = _mapper.Map<IEnumerable<ProductDTO>>(products);

				// Calculate the total number of pages and total number of items
				int totalItems = products.Count();
				int totalPages = (int)Math.Ceiling((double)totalItems / pagination.PageSize);

				// Add the pagination information to the response headers
				Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(new { PageNumber = pagination.PageNumber, PageSize = pagination.PageSize, TotalItems = totalItems, TotalPages = totalPages }));

				_response.Result = productDTOs;
				_response.StatusCode = HttpStatusCode.OK;

				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.ErrorMessages.Add(ex.Message);
				_response.IsSuccess = false;
				_response.StatusCode = HttpStatusCode.InternalServerError;
				return StatusCode(500, _response);
			}
		}


		[HttpGet("listByNumViews")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]

		public async Task<ActionResult<IEnumerable<Product>>> ListByNumViews([Required] int numViews, [FromQuery] Pagination pagination, [Required] string sortColumn = "", bool sortOrder = false, bool tracked = true)
		{
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
				var products = await _dbProducts.GetProductsByNumViewsAsync(numViews, pageSize: pagination.PageSize, pageNumber: pagination.PageNumber, sortColumn: sortColumn, sortOrder: sortOrder, tracked);
				var productDTOs = _mapper.Map<IEnumerable<ProductDTO>>(products);

				// Calculate the total number of pages and total number of items
				int totalItems = products.Count();
				int totalPages = (int)Math.Ceiling((double)totalItems / pagination.PageSize);

				// Add the pagination information to the response headers
				Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(new { PageNumber = pagination.PageNumber, PageSize = pagination.PageSize, TotalItems = totalItems, TotalPages = totalPages }));

				_response.Result = productDTOs;
				_response.StatusCode = HttpStatusCode.OK;

				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.ErrorMessages.Add(ex.Message);
				_response.IsSuccess = false;
				_response.StatusCode = HttpStatusCode.InternalServerError;
				return StatusCode(500, _response);
			}
		}


		[HttpGet("listByOnSale")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]

		public async Task<ActionResult<IEnumerable<Product>>> ListByOnSale([Required] bool onSale, [FromQuery] Pagination pagination, [Required] string sortColumn = "", bool sortOrder = false, bool tracked = true)
		{
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
				var products = await _dbProducts.GetProductsByOnSaleAsync(onSale, pageSize: pagination.PageSize, pageNumber: pagination.PageNumber, sortColumn: sortColumn, sortOrder: sortOrder, tracked);
				var productDTOs = _mapper.Map<IEnumerable<ProductDTO>>(products);

				// Calculate the total number of pages and total number of items
				int totalItems = products.Count();
				int totalPages = (int)Math.Ceiling((double)totalItems / pagination.PageSize);

				// Add the pagination information to the response headers
				Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(new { PageNumber = pagination.PageNumber, PageSize = pagination.PageSize, TotalItems = totalItems, TotalPages = totalPages }));

				_response.Result = productDTOs;
				_response.StatusCode = HttpStatusCode.OK;

				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.ErrorMessages.Add(ex.Message);
				_response.IsSuccess = false;
				_response.StatusCode = HttpStatusCode.InternalServerError;
				return StatusCode(500, _response);
			}
		}


		[HttpGet("listByTotalRevenue")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]

		public async Task<ActionResult<IEnumerable<Product>>> ListByTotalRevenue([Required] double totalRevenue, [FromQuery] Pagination pagination, [Required] string sortColumn = "", bool sortOrder = false, bool tracked = true)
		{
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
				var products = await _dbProducts.GetProductsByTotalRevenueAsync(totalRevenue, pageSize: pagination.PageSize, pageNumber: pagination.PageNumber, sortColumn: sortColumn, sortOrder: sortOrder, tracked);
				var productDTOs = _mapper.Map<IEnumerable<ProductDTO>>(products);

				// Calculate the total number of pages and total number of items
				int totalItems = products.Count();
				int totalPages = (int)Math.Ceiling((double)totalItems / pagination.PageSize);

				// Add the pagination information to the response headers
				Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(new { PageNumber = pagination.PageNumber, PageSize = pagination.PageSize, TotalItems = totalItems, TotalPages = totalPages }));

				_response.Result = productDTOs;
				_response.StatusCode = HttpStatusCode.OK;

				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.ErrorMessages.Add(ex.Message);
				_response.IsSuccess = false;
				_response.StatusCode = HttpStatusCode.InternalServerError;
				return StatusCode(500, _response);
			}
		}


		[HttpGet("listByOwner/{ownerId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetProductsByOwner([Required] string ownerId, [FromQuery] Pagination pagination, [Required] string sortColumn = "", bool sortOrder = false, bool tracked = true)
		{

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
				var products = await _dbProducts.GetProductsByOwnerAsync(ownerId, pageSize: pagination.PageSize, pageNumber: pagination.PageNumber, sortColumn: sortColumn, sortOrder: sortOrder);
				var productDTOs = _mapper.Map<IEnumerable<ProductDTO>>(products);

				// Calculate the total number of pages and total number of items
				int totalItems = products.Count();
				int totalPages = (int)Math.Ceiling((double)totalItems / pagination.PageSize);

				// Add the pagination information to the response headers
				Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(new { PageNumber = pagination.PageNumber, PageSize = pagination.PageSize, TotalItems = totalItems, TotalPages = totalPages }));

				_response.Result = productDTOs;
				_response.StatusCode = HttpStatusCode.OK;

				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.ErrorMessages.Add(ex.Message);
				_response.IsSuccess = false;
				_response.StatusCode = HttpStatusCode.InternalServerError;
				return StatusCode(500, _response);
			}
		}


	}
}