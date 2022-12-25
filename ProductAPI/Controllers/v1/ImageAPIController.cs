using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Models;
using ProductAPI.Repository.IRepository;
using System.Net;
using System.Text.Json;

namespace ProductAPI.Controllers.v1
{
    [Route("api/v{version:apiVersion}/ImageAPI")]
	[ApiController]
	[ApiVersion("1.0", Deprecated = false)]
	public class ImageAPIController : ControllerBase
	{
		protected APIResponse _response;
		private readonly ILogger<ProductAPIController> _logger;
		private readonly IProductRepository _dbProducts;
		private readonly IMapper _mapper;

		public ImageAPIController(IProductRepository dbProducts, ILogger<ProductAPIController> logger, IMapper mapper)
		{
			_dbProducts = dbProducts;
			_logger = logger;
			_mapper = mapper;
			_response = new();
		}


	}
}
