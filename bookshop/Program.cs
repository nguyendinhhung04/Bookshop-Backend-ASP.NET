using Microsoft.AspNetCore.Builder;
using Oracle.ManagedDataAccess.Client;
using bookshop.Models;
using Microsoft.EntityFrameworkCore;


namespace bookshop
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            var MyAllowSpecificOrigins = "MyAllowSpecificOrigins";
            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.WithOrigins("*").AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                                  });
            });

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddDbContext<BookShopContext>(opt =>opt.UseOracle(builder.Configuration.GetConnectionString("bookshopContext")));


            builder.Services.AddScoped<DBConnection>();

            builder.Services.AddScoped<Models.DAO.IDAO<Book>, Models.DAO.BookDAO>();

            builder.Services.AddSingleton<TempData>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();


            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();

            app.MapBookEndpoints();
            app.MapControllers();

            app.Run();
        }
    }
}
