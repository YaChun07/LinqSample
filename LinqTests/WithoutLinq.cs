using System.Collections.Generic;
using LinqTests;

internal class WithoutLinq
{
    public static List<Product> FindProductByPrice(IEnumerable<Product> products, int lowBoundary, int highBoundary, string supplier)
    {
        var productList = new List<Product>();
        foreach (var product in products)
        {
            if (product.Price >= lowBoundary && product.Price <= highBoundary && product.Supplier == supplier)
            {
                productList.Add(product);
            }
        }

        return productList;
    }

    public static IEnumerable<string> ChangeTo91Port(IEnumerable<string> urls)
    {
        foreach (var urlItem in urls)
        {
            if (urlItem.Contains("tw"))
            {
                yield return urlItem + ":91";
            }
            else
            {
                yield return urlItem;
            }
        }
    }
}