using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Webmvc.Middleware
{
    public class CookieMiddleware
    {
        private readonly RequestDelegate _next;

        public CookieMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, AppDbContext _db)
        {
            string requestUrl = context.Request.Path.ToString().ToLower();
            if (requestUrl == "/login" || requestUrl == "/login/index")
                goto result;

            var cookies = context.Request.Cookies;
            if (cookies["UserId"] is null || cookies["SessionId"] is null)
            {
                context.Response.Redirect("/Login");
                goto result;
            }

            string userId = cookies["UserId"]!.ToString();
            string sessionId = cookies["SessionId"]!.ToString();

            var login = await _db.Login.FirstOrDefaultAsync(x => x.SessionId == sessionId && x.UserId == userId);
            if (login is null)
            {
                context.Response.Redirect("/Login");
                goto result;
            }

            if(DateTime.Now > login.SessionExpired)
            {
                context.Response.Redirect("/Login");
                goto result;
            }
        result: await _next(context);
        }
    }

    public static class CookieMiddlewareExtentions
    {
        public static IApplicationBuilder UseCookieMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<CookieMiddleware>();
        }
    }
}
