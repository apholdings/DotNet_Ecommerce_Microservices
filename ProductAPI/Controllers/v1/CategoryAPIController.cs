using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Models;
using ProductAPI.Repository.IRepository;
using System.Net;
using System.Text.Json;

namespace ProductAPI.Controllers.v1
{
    [Route("api/v{version:apiVersion}/CategoryAPI")]
	[ApiController]
	[ApiVersion("1.0", Deprecated = false)]
	public class CategoryAPIController : ControllerBase
	{
		protected APIResponse _response;
		private readonly ILogger<CategoryAPIController> _logger;
		private readonly ICategoryRepository _dbCategories;
		private readonly IMapper _mapper;

		public CategoryAPIController(ICategoryRepository dbCategories, ILogger<CategoryAPIController> logger, IMapper mapper)
		{
			_dbCategories = dbCategories;
			_logger = logger;
			_mapper = mapper;
			_response = new();
		}


	}
}
