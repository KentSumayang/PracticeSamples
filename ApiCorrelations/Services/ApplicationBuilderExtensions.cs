using ApiCorrelations.Helpers;

namespace ApiCorrelations.Services
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddCorrelationIdMiddleware(this IApplicationBuilder appBuilder)
            => appBuilder.UseMiddleware<CorrelationIdMiddleware>();
    }
}
