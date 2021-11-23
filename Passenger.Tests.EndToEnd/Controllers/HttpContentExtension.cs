using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Passenger.Tests.EndToEnd.Controllers;

public static class HttpContentExtension
{
    public static async Task<T> FromJson<T>(this HttpContent content, JsonSerializerOptions options)
        => JsonSerializer.Deserialize<T>(await content.ReadAsStringAsync(), options);
}