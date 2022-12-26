using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ProductAPI;
using ProductAPI.Data;
using ProductAPI.Repository;
using ProductAPI.Repository.IRepository;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// LOGGING
var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// AUTOMAPPER
builder.Services.AddAutoMapper(typeof(MappingConfig));

// DATABASE
builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
	option.UseNpgsql(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
	//option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
});

// VERSIONING
builder.Services.AddApiVersioning(options =>
{
	options.AssumeDefaultVersionWhenUnspecified = true;
	options.DefaultApiVersion = new ApiVersion(1, 0);
	options.ReportApiVersions = true;
});
builder.Services.AddVersionedApiExplorer(options =>
{
	options.GroupNameFormat = "'v'VVV";
	options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddMemoryCache();

// REPOSITORIES
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IVideoRepository, VideoRepository>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();


builder.Services.AddControllers(option =>
{
	option.CacheProfiles.Add("Default60",
		new CacheProfile()
		{
			Duration = 60
		});
	//option.ReturnHttpNotAcceptable = true; // This makes it so only application/json can receive data, also xml
}).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	options.SwaggerDoc("v1", new OpenApiInfo
	{
		Version = "v1.0",
		Title = "ProductAPI",
		Description = "DotNet Microservices Ecommerce Example ProductAPI",
		TermsOfService = new Uri("https://example.com/terms"),
		Contact = new OpenApiContact
		{
			Name = "SoloPython",
			Url = new Uri("https://solopython.com")
		},
		License = new OpenApiLicense
		{
			Name = "Example License",
			Url = new Uri("https://example.com/license")
		}
	});
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(options =>
	{
		options.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductAPIV1");
	});
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
