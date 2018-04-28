using Amanda;
using ExpectedObjects;
using LinqTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void find_products_that_price_between_200_and_500()
        {
            var products = RepositoryFactory.GetProducts();
            var actual = WithoutLinq.FindProductByPrice(products, 200, 500, "Odd-e");

            var expected = new List<Product>()
            {
                new Product{Id=3, Cost=31, Price=310, Supplier="Odd-e" },
                new Product{Id=4, Cost=41, Price=410, Supplier="Odd-e" }
            };

            expected.ToExpectedObject().ShouldEqual(actual);
        }

        [TestMethod]
        public void find_products_that_price_between_200_and_500_UseMyWhere()
        {
            var products = RepositoryFactory.GetProducts();
            var actual = products.MyWhere(p => p.Price >= 200 && p.Price <= 500 && p.Supplier == "Odd-e").ToList();

            var expected = new List<Product>()
            {
                new Product{Id=3, Cost=31, Price=310, Supplier="Odd-e" },
                new Product{Id=4, Cost=41, Price=410, Supplier="Odd-e" }
            };

            expected.ToExpectedObject().ShouldEqual(actual);
        }

        [TestMethod]
        public void find_products_that_price_between_200_and_500_UseLinqWhere()
        {
            var products = RepositoryFactory.GetProducts();
            var actual = products.Where(p => p.Price >= 200 && p.Price <= 500 && p.Supplier == "Odd-e").ToList();

            var expected = new List<Product>()
            {
                new Product{Id=3, Cost=31, Price=310, Supplier="Odd-e" },
                new Product{Id=4, Cost=41, Price=410, Supplier="Odd-e" }
            };

            expected.ToExpectedObject().ShouldEqual(actual);
        }

        [TestMethod]
        public void find_Employee_that_age_between_25_and_45_ForEmployee()
        {
            var employees = RepositoryFactory.GetEmployees();
            var actual = employees.MyWhere(p => p.Age >= 25 && p.Age <= 40).ToList();

            var expected = new List<Employee>()
            {
                new Employee{Name="Tom", Role=RoleType.Engineer, MonthSalary=140, Age=33, WorkingYear=2.6} ,
                new Employee{Name="Bas", Role=RoleType.Engineer, MonthSalary=280, Age=36, WorkingYear=2.6} ,
                new Employee{Name="Mary", Role=RoleType.OP, MonthSalary=180, Age=26, WorkingYear=2.6} ,
                new Employee{Name="Joey", Role=RoleType.Engineer, MonthSalary=250, Age=40, WorkingYear=2.6}
            };

            expected.ToExpectedObject().ShouldEqual(actual);
        }

        [TestMethod]
        public void ToHttps()
        {
            var urls = RepositoryFactory.GetUrls();
            var actual = WithoutLinq.GetHttps(urls);

            var expected = new List<string>
            {
                "https://tw.yahoo.com:8080",
                "https://facebook.com:8080",
                "https://twitter.com:8080",
                "https://github.com:8080"
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void UrlLength()
        {
            var urls = RepositoryFactory.GetUrls();
            var actual = WithoutLinq.GetUrlLength(urls);

            var expected = new List<int>
            {
                19,
                20,
                19,
                17 
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }
    }
}

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

    public static IEnumerable<string> GetHttps(IEnumerable<string> urls)
    {
        foreach (var urlItem in urls)
        {
            yield return urlItem.Replace("http:", "https:").Replace(".com", ".com:8080");
        }
    }

    public static IEnumerable<int> GetUrlLength(IEnumerable<string> urls)
    {
        foreach (var urlItem in urls)
        {
            yield return urlItem.Length;
        }
    }
}

namespace Amanda
{
    internal static class YourOwnLinq
    {
        public static IEnumerable<TSource> MyWhere<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> func)
        {
            var result = source.GetEnumerator();
            while (result.MoveNext())
            {
                var itme = result.Current;
                if (func(itme))
                {
                    yield return itme;
                }
            }
        }
    }
}