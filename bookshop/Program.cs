using bookshop.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using static System.Reflection.Metadata.BlobBuilder;


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

            //builder.Services.AddDbContext<BookShopContext>(opt =>opt.UseOracle(builder.Configuration.GetConnectionString("bookshopContext")));
            builder.Services.AddDbContext<BookShopContext>(opt =>
            {
                opt.UseInMemoryDatabase("BookShopTempDB");
                opt.EnableSensitiveDataLogging(); // Add this line
            });

            //Dont use DAO --> Use Entity Framework
            //builder.Services.AddScoped<DBConnection>();
            //builder.Services.AddScoped<Models.DAO.IDAO<Book>, Models.DAO.BookDAO>();

            //builder.Services.AddSingleton<TempData>();


            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<BookShopContext>();

                if (!db.Book.Any())
                {
                    //Book[] books = new Book[10];
                    //for (int i = 0; i < 10; i++)
                    //{
                    //    books[i] = new Book(
                    //        i,
                    //        "Book " + i.ToString(),
                    //        i * 50,
                    //        i * 7 % 2
                    //        );
                    //}
                    db.Book.AddRange(
                        new Book(
                                1,
                                "Book " + 1.ToString(),
                                1 * 50,
                                1 * 7 % 2
                                ),
                        new Book(
                                2,
                                "Book " + 2.ToString(),
                                2 * 50,
                                2 * 7 % 2
                                )

                        );

                    db.SaveChanges();
                }
            }


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
