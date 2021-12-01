using Microsoft.AspNetCore.Mvc;

namespace Passenger.Api.Extensions;

public static class TaskExtensions
{
    // Just the testing the idea
    public static async Task<IActionResult> Return(this Task task, StatusCodeResult result)
    {
        await task;

        return result;
    } 
}