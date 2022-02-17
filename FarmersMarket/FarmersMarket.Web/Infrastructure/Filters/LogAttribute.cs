namespace FarmersMarket.Web.Infrastructure.Filters
{
    using Microsoft.AspNetCore.Mvc.Filters;
    using System.IO;

    public class LogAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var logfile = "Infrastructure/Logs/log_" + DateTime.UtcNow.ToString("d_M_yyyy") + ".txt";
            using (var writer = new StreamWriter(logfile, true)) {
                try
                {
                    var dateTime = DateTime.UtcNow;
                    var ipAddress = context.HttpContext.Connection.RemoteIpAddress;
                    var userName = context.HttpContext.User?.Identity?.Name ?? "Anonymous";
                    var controller = context.Controller.GetType().Name;
                    var action = context.RouteData.Values["action"];

                    var logMessage = $"{dateTime} - {ipAddress} - {userName} - {controller}.{action}";

                    if (context.Exception != null)
                    {
                        var exceptionType = context.Exception.GetType().Name;
                        var exceptionMessage = context.Exception.Message;

                        logMessage = $"[!] {logMessage} - {exceptionType} - {exceptionMessage}";
                    }

                    writer.WriteLine(logMessage);
                }
                catch (Exception)
                {
                    throw;
                }
            };

            base.OnActionExecuted(context);
        }
    }
}
