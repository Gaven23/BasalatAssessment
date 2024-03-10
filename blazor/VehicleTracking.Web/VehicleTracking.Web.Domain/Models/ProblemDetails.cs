namespace VehicleTracking.Web.Domain.Models
{
    // Used for 400 Bad Request problem details structure
    // https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.problemdetails?view=aspnetcore-7.0
    public record ProblemDetails(string Title, string ErrorCode, string TraceId);
}
