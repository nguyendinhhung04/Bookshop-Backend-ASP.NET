using bookshop.Models;
using bookshop.Models.DAO;
using Microsoft.AspNetCore.Mvc;

namespace bookshop
{
    public static class BookEndPoints
    {
        public static RouteGroupBuilder MapBookEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/books");

            group.MapGet("/getbooks", ([FromServices] IDAO<Book> bookDAO) =>
            {
                return Results.Ok(bookDAO.GetAll().ToArray());
            });

            group.MapGet("/gettemp", ([FromServices] IDAO<Book> bookDAO) =>
            {
                return Results.Ok(bookDAO.GetTempData());
            });

            return group;
        }
    }
}
