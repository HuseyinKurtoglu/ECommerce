using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ECommerce.DataAcces.Entity;
using Microsoft.Data.SqlClient;
using System.Data;
using ECommerce.DataAcces.Absract;
using ECommerce.DataAcces.Concrete;
using ECommerce.Business.Absract;
using ECommerce.Business.Concrete;
using ECommerce.DataAcces.Models;
using FluentValidation;
using ECommerce.Entities;
using ECommerce.DataAcces.Validator;
using ECommerce.DataAcces.Validator.ECommerce.DataAcces.Validator;
using ECommerce.Business.ValidationRules;
using ECommerce.DataAcces.Concrete.Dapper;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ECommerce.Business.ValidationRules.FluentValidation;


public class Startup
{
    public IConfiguration Configuration { get; }

    // Constructor, IConfiguration bağımlılığını dependency injection yoluyla alır.
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        // API kontrolcülerini ekler.
        services.AddControllers();

        // Swagger'i yapılandırır ve API belgelerini oluşturur.
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "ECommerce API", Version = "v1" });
        });

        // Entity Framework Core ile SQL Server'a bağlanır.
        services.AddDbContext<ECommerce.DataAcces.Entity.ECommerceDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        // FluentValidation için validator'ları ekler.
        services.AddScoped<IValidator<User>, UserValidator>();
        services.AddScoped<IValidator<Order>, OrderValidator>();
        services.AddScoped<IValidator<OrderDetail>, OrderDetailValidator>();
        services.AddScoped<IValidator<Product>, ProductValidator>();
        services.AddScoped<IValidator<Customer>, CustomerValidator>();
        services.AddScoped<IValidator<Shipper>, ShipperValidator>();
        services.AddScoped<IValidator<OrderStatus>, OrderStatusValidator>();
        services.AddScoped<IValidator<ProductReview>, ProductReviewValidator>();
        services.AddScoped<IValidator<Category>, CategoryValidator>();
        services.AddScoped<IValidator<Image>, ImageValidator>();
        services.AddScoped<IValidator<Payment>, PaymentValidator>();
        services.AddScoped<IValidator<Employee>, EmployeeValidator>();
        // İş servislerini ve repository'leri ekler.
        services.AddTransient<IOrderService, OrderService>();
        services.AddTransient<IOrderRepository, OrderRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IShipperRepository, ShipperRepository>();
        services.AddScoped<IShipperService, ShipperService>();
        services.AddScoped<IOrderStatusRepository, OrderStatusRepository>();
        services.AddScoped<IOrderStatusService, OrderStatusService>();
        services.AddScoped<IProductReviewRepository, ProductReviewRepository>();
        services.AddScoped<IProductReviewService, ProductReviewService>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IImageRepository, ImageRepository>();
        services.AddScoped<IImageService, ImageService>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        // Veritabanı bağlantısını Scoped olarak ekler.
        services.AddScoped<IDbConnection>(sp => new SqlConnection(Configuration.GetConnectionString("DefaultConnection")));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            // Geliştirme ortamında hata ayıklama sayfasını kullanır.
            app.UseDeveloperExceptionPage();
        }


        // HTTPS yönlendirmeyi etkinleştirir.
        app.UseHttpsRedirection();

        // Routing middleware'ini ekler.
        app.UseRouting();

        // Yetkilendirme middleware'ini ekler.
        app.UseAuthorization();

        // Swagger'ı etkinleştirir.
        app.UseSwagger();

        // Swagger UI'yi yapılandırır.
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "ECommerce API v1");
        });

        // Endpoint'leri yapılandırır.
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
