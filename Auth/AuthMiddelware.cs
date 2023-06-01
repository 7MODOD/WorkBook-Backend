using System.Text.RegularExpressions;
using WorkBook.WorkBookDbModel;

namespace WorkBook.Auth
{
    public class AuthMiddelware
    {
        
        private readonly RequestDelegate _next;

        public AuthMiddelware( RequestDelegate next)
        {
            
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {

            if (!context.Request.Headers.ContainsKey("Authorization"))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized");
                return;
            }

            var authorizationHeader = context.Request.Headers["Authorization"].FirstOrDefault();
            if (authorizationHeader == null || authorizationHeader == string.Empty)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized");
                return;
            }
            DateTime time;
            int result = 0;
            if (context.Request.Path.StartsWithSegments("/Customer"))
            {
                
                using (var db = new DbModel())
                {
                    time = db.CustomerAuth.FirstOrDefault(x => x.Token == authorizationHeader).UpdatedAt;
                    var currentTime = DateTime.Now.AddMinutes(-15);
                    result = DateTime.Compare(time, currentTime);
                }

            }
            else if (context.Request.Path.StartsWithSegments("/Worker"))
            {
                using (var auth = new DbModel())
                {
                    time = auth.WorkerAuth.FirstOrDefault(x => x.Token == authorizationHeader).UpdatedAt;
                    var currentTime = DateTime.Now.AddMinutes(-15);
                    result = DateTime.Compare(time, currentTime);
                }
            }
            
            

            if (result < 0) {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized");
                return;
            }
            await _next(context);
            


        }
    }
}
