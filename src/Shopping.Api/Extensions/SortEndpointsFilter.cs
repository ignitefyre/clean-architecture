using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Shopping.Api.Extensions;

public class SortEndpointsFilter : IDocumentFilter
{
    private static readonly Dictionary<OperationType, int> MethodOrder = new()
    {
        { OperationType.Post, 0 },
        { OperationType.Get, 1 },
        { OperationType.Patch, 2 },
        { OperationType.Put, 3 },
        { OperationType.Delete, 4 }
    };

    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var sortedPaths = new OpenApiPaths();

        foreach (var path in swaggerDoc.Paths.OrderBy(p => p.Key))
        {
            var sortedOperations = path.Value.Operations
                .OrderBy(o => MethodOrder.TryGetValue(o.Key, out var value) ? value : 999) // Default for methods not listed
                .ToDictionary(o => o.Key, o => o.Value);

            var newPathItem = new OpenApiPathItem();
            foreach (var operation in sortedOperations)
            {
                newPathItem.Operations.Add(operation.Key, operation.Value);
            }
            sortedPaths.Add(path.Key, newPathItem);
        }

        swaggerDoc.Paths = sortedPaths;
    }
}