namespace Shopping.Api.Extensions;

public static class UriHelpers
{
    /// <summary>
    /// Returns a resource uri for a cart
    /// </summary>
    /// <param name="source">Request context</param>
    /// <param name="cartId">Resource Identifier</param>
    /// <returns>
    /// {host}/api/carts/{cartId}
    /// </returns>
    public static Uri AsCartResourceUri(this HttpRequest source, string cartId)
    {
        return new Uri($"{source.Scheme}://{source.Host}/api/carts/{cartId}", UriKind.Absolute);
    }

    /// <summary>
    /// Returns a resource uri for a carts item
    /// </summary>
    /// <param name="source">Request context</param>
    /// <param name="cartId">Resource Identifier</param>
    /// <param name="productId">Product Identifier</param>
    /// <returns>
    /// {host}/api/carts/{cartId}/items/{productId}
    /// </returns>
    public static Uri AsCartItemResourceUri(this HttpRequest source, string cartId, string productId)
    {
        return new Uri($"{source.Scheme}://{source.Host}/api/carts/{cartId}/items/{productId}", UriKind.Absolute);
    }
}