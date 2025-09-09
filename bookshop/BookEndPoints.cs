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

            return group;
        }
    }
}
