using Cocona.Filters;

namespace Ghost.Application.Filters;

public class ExceptionHandlingFilterAttribute : CommandFilterAttribute
{
    public override async ValueTask<int> OnCommandExecutionAsync(CoconaCommandExecutingContext ctx, CommandExecutionDelegate next)
    {
        try
        {
            return await next(ctx);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"{ex.Message}");
            return 1;
        }
        catch (HttpRequestException)
        {
            Console.WriteLine("API request failed. Please check your network connection and try again.");
            return 1;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error ocurred, please try again");
            return 1;
        }
    }
}
