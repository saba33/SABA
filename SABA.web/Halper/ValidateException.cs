using Microsoft.AspNetCore.Http.Features;
using System.Diagnostics;

namespace SABA.web.Halper
{
    public class ValidateException
    {
        public static class ExceptionValidator
        {
            public static void ValidateException(HttpContext context)
            {
                var statusCode = context.Response.StatusCode;
                var reasonPhrase = context.Features.Get<IHttpResponseFeature>().ReasonPhrase;
                var method = context.Request.Method;
                var path = context.Request.Path;
                var sw = Stopwatch.StartNew();
                var elapsedMilliseconds = sw.ElapsedMilliseconds;
                if (statusCode >= 500)
                {
                    Serilog.Log.Error($"{statusCode} {reasonPhrase} for request {method} {path} ({elapsedMilliseconds}ms)");
                }
                else if (statusCode >= 400)
                {
                    Serilog.Log.Warning($"{statusCode} {reasonPhrase} for request {method} {path} ({elapsedMilliseconds}ms)");
                }
                else
                {
                    Serilog.Log.Information($"{method} {path} responded {statusCode} ({elapsedMilliseconds}ms)");
                }
            }
        }
    }
}
