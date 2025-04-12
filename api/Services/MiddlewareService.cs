namespace galaxy_match_make.Services;

public class MiddlewareService
{
    private readonly RequestDelegate _next;

    public MiddlewareService(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, GoogleAuthService googleAuthService)
    {
        var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
        
        if (authHeader?.StartsWith("Bearer ") == true)
        {
            var token = authHeader["Bearer ".Length..].Trim();
            
            try
            {
                var principal = await googleAuthService.ValidateGoogleJwtAndCreatePrincipal(token);
                context.User = principal;
            }
            catch
            {
                // Don't set context.User if validation fails
            }
        }

        await _next(context);
    }
}