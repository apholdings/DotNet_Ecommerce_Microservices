using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Models;
using ProductAPI.Models.DTO.ProductDtos;
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
		public async Task<IActionResult> GetAll([FromQuery] Pagination pagination, [Required] string sortColumn, bool sortOrder = false)
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


		[HttpGet("{id}", Name = "GetProduct")]
		public async Task<IActionResult> Get(int id)
		{
			var product = await _dbProducts.GetAsync(p => p.ProductId == id);
			if (product == null)
			{
				_response.ErrorMessages.Add("Product not found");
				_response.IsSuccess = false;
				_response.StatusCode = HttpStatusCode.NotFound;
				return NotFound(_response);
			}

			var productDTO = _mapper.Map<ProductDTO>(product);
			_response.Result = productDTO;
			_response.StatusCode = HttpStatusCode.OK;

			return Ok(_response);
		}


		[HttpGet("search")]
		public async Task<IActionResult> Search(string searchQuery)
		{
			var products = await _dbProducts.SearchProductsAsync(searchQuery);
			var productDTOs = _mapper.Map<IEnumerable<ProductDTO>>(products);

			_response.Result = productDTOs;
			_response.StatusCode = HttpStatusCode.OK;

			return Ok(_response);
		}


		[HttpGet("category/{id}")]
		public async Task<IActionResult> GetByCategory(int id)
		{
			var products = await _dbProducts.GetProductsForCategoryAsync(id);
			var productDTOs = _mapper.Map<IEnumerable<ProductDTO>>(products);

			_response.Result = productDTOs;
			_response.StatusCode = HttpStatusCode.OK;

			return Ok(_response);
		}


		[HttpGet("averageRating/{averageRating}")]
		public async Task<IActionResult> GetByAverageRating(double averageRating)
		{
			var products = await _dbProducts.GetAsync(p => p.AverageRating == averageRating);
			var productDTOs = _mapper.Map<IEnumerable<ProductDTO>>(products);
			_response.Result = productDTOs;
			_response.StatusCode = HttpStatusCode.OK;
			return Ok(_response);
		}


		[HttpGet("numPurchases/{numPurchases}")]
		public async Task<IActionResult> GetByNumPurchases(int numPurchases)
		{
			var products = await _dbProducts.GetAsync(p => p.NumPurchases == numPurchases);
			var productDTOs = _mapper.Map<IEnumerable<ProductDTO>>(products);
			_response.Result = productDTOs;
			_response.StatusCode = HttpStatusCode.OK;

			return Ok(_response);

		}


		[HttpGet("numViews/{numViews}")]
		public async Task<IActionResult> GetByNumViews(int numViews)
		{
			var products = await _dbProducts.GetAsync(p => p.NumViews == numViews);
			var productDTOs = _mapper.Map<IEnumerable<ProductDTO>>(products);
			_response.Result = productDTOs;
			_response.StatusCode = HttpStatusCode.OK;

			return Ok(_response);

		}


		[HttpGet("numLikes/{numLikes}")]
		public async Task<IActionResult> GetByNumLikes(int numLikes)
		{
			var products = await _dbProducts.GetAsync(p => p.NumLikes == numLikes);
			var productDTOs = _mapper.Map<IEnumerable<ProductDTO>>(products);
			_response.Result = productDTOs;
			_response.StatusCode = HttpStatusCode.OK;

			return Ok(_response);
		}


		[HttpGet("avgTimeSpent/{avgTimeSpent}")]
		public async Task<IActionResult> GetByAvgTimeSpent(int avgTimeSpent)
		{
			var products = await _dbProducts.GetAsync(p => p.AvgTimeSpent == avgTimeSpent);
			var productDTOs = _mapper.Map<IEnumerable<ProductDTO>>(products);
			_response.Result = productDTOs;
			_response.StatusCode = HttpStatusCode.OK;

			return Ok(_response);
		}


		[HttpGet("clickThroughRate/{clickThroughRate}")]
		public async Task<IActionResult> GetByClickThroughRate(double clickThroughRate)
		{
			var products = await _dbProducts.GetAsync(p => p.ClickThroughRate == clickThroughRate);
			var productDTOs = _mapper.Map<IEnumerable<ProductDTO>>(products);
			_response.Result = productDTOs;
			_response.StatusCode = HttpStatusCode.OK;
			return Ok(_response);
		}


		[HttpGet("name/{productName}")]
		public async Task<IActionResult> GetByName(string productName)
		{
			// Use the GetProductByNameAsync method from the IProductRepository to retrieve the products matching the given name
			var products = await _dbProducts.GetProductByNameAsync(productName);
			// Map the retrieved products to ProductDTO objects
			var productDTOs = _mapper.Map<IEnumerable<ProductDTO>>(products);

			_response.Result = productDTOs;
			_response.StatusCode = HttpStatusCode.OK;

			return Ok(_response);
		}


		[HttpGet("manufacturer/{manufacturer}")]
		public async Task<IActionResult> GetByManufacturer(string manufacturer)
		{
			var products = await _dbProducts.GetAsync(p => p.Manufacturer == manufacturer);
			var productDTOs = _mapper.Map<IEnumerable<ProductDTO>>(products);

			_response.Result = productDTOs;
			_response.StatusCode = HttpStatusCode.OK;

			return Ok(_response);
		}


		// CREATE
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] ProductCreateDTO productDTO)
		{
			if (!ModelState.IsValid)
			{
				_response.ErrorMessages.Add("Invalid model state");
				_response.IsSuccess = false;
				_response.StatusCode = HttpStatusCode.BadRequest;
				return BadRequest(_response);
			}

			// Create a new product entity from the product DTO
			var product = _mapper.Map<Product>(productDTO);

			// Set the created at and updated at timestamps
			product.CreatedAt = DateTime.Now;
			product.UpdatedAt = DateTime.Now;

			// Add the product to the database
			await _dbProducts.CreateAsync(product);

			// Return the created product DTO in the response
			var createdProductDTO = _mapper.Map<ProductDTO>(product);
			_response.Result = createdProductDTO;
			_response.StatusCode = HttpStatusCode.Created;

			return CreatedAtRoute("GetProduct", new { version = "1.0", id = product.ProductId }, _response);
		}


		// UPDATE
		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, [FromBody] ProductUpdateDTO productUpdateDTO)
		{
			var productFromDb = await _dbProducts.GetAsync(p => p.ProductId == id);
			if (productFromDb == null)
			{
				_response.ErrorMessages.Add("Product not found");
				_response.IsSuccess = false;
				_response.StatusCode = HttpStatusCode.NotFound;
				return NotFound(_response);
			}

			_mapper.Map(productUpdateDTO, productFromDb);

			await _dbProducts.UpdateAsync(productFromDb);

			_response.Result = _mapper.Map<ProductDTO>(productFromDb);
			_response.StatusCode = HttpStatusCode.OK;

			return Ok(_response);
		}

		// DELETE
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			// First, retrieve the product to be deleted from the database
			var product = await _dbProducts.GetAsync(p => p.ProductId == id);
			if (product == null)
			{
				// If the product does not exist, return a 404 Not Found response
				_response.ErrorMessages.Add("Product not found");
				_response.IsSuccess = false;
				_response.StatusCode = HttpStatusCode.NotFound;
				return NotFound(_response);
			}
			// Otherwise, remove the product from the database and save the changes
			await _dbProducts.RemoveAsync(product);
			await _dbProducts.SaveAsync();

			// Return a 200 OK response to confirm that the product has been successfully deleted
			_response.StatusCode = HttpStatusCode.OK;
			return Ok(_response);
		}
	}
}