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
            var actual = urls.MySelect(urlItem => urlItem.Replace("http:", "https:").Replace(".com", ".com:8080"));

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
            var actual = urls.MySelect(urlItem => urlItem.Length);

            var expected = new List<int>
            {
                19,
                20,
                19,
                17
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void To91Port_IfContainstw()
        {
            var urls = RepositoryFactory.GetUrls();
            var actual = WithoutLinq.ChangeTo91Port(urls);

            var expected = new List<string>
            {
                "http://tw.yahoo.com:91",
                "https://facebook.com",
                "https://twitter.com:91",
                "http://github.com"
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void FindEngineerSalary()
        {
            var employee = RepositoryFactory.GetEmployees();
            var actual = employee.MyWhere(e => e.Role == RoleType.Engineer)
                .MySelect(s => s.MonthSalary);

            var expected = new List<int>
            {
               100,
               140 ,
               280 ,
               120 ,
               250
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void SelectTopN()
        {
            var products = RepositoryFactory.GetProducts();
            var actual = products.MyTake(3);

            var expected = new List<Product>
            {
                new Product{Id=1, Cost=11, Price=110, Supplier="Odd-e" },
                new Product{Id=2, Cost=21, Price=210, Supplier="Yahoo" },
                new Product{Id=3, Cost=31, Price=310, Supplier="Odd-e" },
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void SelectTopN_with_index()
        {
            var employees = RepositoryFactory.GetEmployees();
            var actual = employees.MyTake(3).MySelect((e, index) => (index + 1) + "-" + e.Name);

            var expected = new List<string>
            {
                "1-Joe","2-Tom","3-Kevin"
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void Skip6()
        {
            var employees = RepositoryFactory.GetEmployees();
            var actual = employees.MySkip(6);

            var expected = new List<Employee>
            {
                new Employee{Name="Frank", Role=RoleType.Engineer, MonthSalary=120, Age=16, WorkingYear=2.6} ,
                new Employee{Name="Joey", Role=RoleType.Engineer, MonthSalary=250, Age=40, WorkingYear=2.6},

            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void Skip6MoreThan300()
        {
            var products = RepositoryFactory.GetProducts();
            var actual = products.MySkipWhile(e=>e.Price>300, 4);

            var expected = new List<Product>
            {
                new Product{Id=1, Cost=11, Price=110, Supplier="Odd-e" },
                new Product{Id=2, Cost=21, Price=210, Supplier="Yahoo" },
                new Product{Id=7, Cost=71, Price=710, Supplier="Yahoo" },
                new Product{Id=8, Cost=18, Price=780, Supplier="Yahoo" }
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void Take2MoreThan300()
        {
            var products = RepositoryFactory.GetProducts();
            var actual = products.MyTakeWhile(e => e.Price > 300, 2);

            var expected = new List<Product>
            {
                new Product{Id=3, Cost=31, Price=310, Supplier="Odd-e" },
                new Product{Id=4, Cost=41, Price=410, Supplier="Odd-e" },
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

    }
}

internal static class WithoutLinq
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

        public static IEnumerable<TResult> MySelect<TSource, TResult>(this IEnumerable<TSource> urls, Func<TSource, TResult> selector)
        {
            foreach (var urlItem in urls)
            {
                yield return selector(urlItem);
            }
        }

        public static IEnumerable<TSource> MySkip<TSource>(this IEnumerable<TSource> sources, int topN)
        {
            int count = 0;
            foreach (var source in sources)
            {
                if (count >= topN)
                {
                    yield return source;
                }
                count++;
            }
        }

        public static IEnumerable<TSource> MySkipWhile<TSource>(this IEnumerable<TSource> sources, Func<TSource, bool> selector,int skipNum)
        {
            var enumerator = sources.GetEnumerator();
            var count = 0;
            while (enumerator.MoveNext())
            {
                if (count < skipNum && selector(enumerator.Current))
                {
                    count++;
                }
                else
                {
                    yield return enumerator.Current;
                }
               
            }
        }

        public static IEnumerable<TResult> MySelect<TSource, TResult>(this IEnumerable<TSource> urls, Func<TSource, int, TResult> selector)
        {
            var count = 0;
            foreach (var urlItem in urls)
            {
                yield return selector(urlItem, count);
                count++;
            }
        }

        public static IEnumerable<TSource> MyTake<TSource>(this IEnumerable<TSource> sources, int topN)
        {
            var enumerator = sources.GetEnumerator();
            var count = 0;
            while (enumerator.MoveNext())
            {
                if (count < topN)
                {
                    yield return enumerator.Current;
                }
                else
                {
                    yield break;
                }
                count++;
            }
        }

        public static IEnumerable<TSource> MyTakeWhile<TSource>(this IEnumerable<TSource> sources, Func<TSource, bool> selector, int takeNum)
        {
            var enumerator = sources.GetEnumerator();
            var count = 0;
            while (enumerator.MoveNext())
            {
                if (selector(enumerator.Current) && count < takeNum)
                {
                    yield return enumerator.Current;
                    count++;
                }
            }
        }
    }
}