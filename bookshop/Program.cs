using bookshop.DataAccessLayer;
using bookshop.DataAccessLayer.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using static System.Reflection.Metadata.BlobBuilder;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using bookshop.DataAccessLayer.Models.DAO;


namespace bookshop
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            var MyAllowSpecificOrigins = "MyAllowSpecificOrigins";
            // Add services to the container.

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                // PascalCase thay vì camelCase
                options.JsonSerializerOptions.PropertyNamingPolicy = null;

                // Ignore null
                options.JsonSerializerOptions.DefaultIgnoreCondition =
                    System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;

                // Case-insensitive khi parse JSON (m?c ??nh true)
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
                    options =>
                    {
                        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = builder.Configuration["JWT:Issuer"],
                            ValidAudience = builder.Configuration["JWT:Audience"],
                            IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]))
                        };

                        options.Events = new JwtBearerEvents
                        {
                            OnAuthenticationFailed = context =>
                            {
                                Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                                return System.Threading.Tasks.Task.CompletedTask;
                            }
                        };
                    });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.WithOrigins("*").AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader() ;
                                  });
            });

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            //Add ODP Service
            builder.Services.AddScoped<DBConnection>();
            builder.Services.AddScoped<BookDAO>();
            builder.Services.AddScoped<CategoryDAO>();


            //Use DB Context
            builder.Services.AddDbContext<BookShopContext>(opt =>opt.UseOracle(builder.Configuration.GetConnectionString("bookshopContext")));

            ////Use In Memory Database
            //builder.Services.AddDbContext<BookShopContext>(opt =>
            //{
            //    opt.UseInMemoryDatabase("BookShopTempDB");
            //    opt.EnableSensitiveDataLogging(); // Add this line
            //});


            var app = builder.Build();
            //using (var scope = app.Services.CreateScope())
            //{
            //    var db = scope.ServiceProvider.GetRequiredService<BookShopContext>();

            //    if (!db.Category.Any())
            //    {
            //        db.Category.AddRangeAsync(
            //            GenerateInMemoryDB.Books
            //            );

            //        db.SaveChanges();
            //    }

            //    if (!db.Book.Any())
            //    {
            //        db.Book.AddRangeAsync(
                       
            //            );

            //        db.SaveChanges();
            //    }
            //}



            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();


            app.UseCors(MyAllowSpecificOrigins);


            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
