using ExpectedObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        public void SkipMoreThan300()
        {
            var products = RepositoryFactory.GetProducts();
            var actual = products.MyRealSkipWhile(e => e.Price < 300);

            var expected = new List<Product>
            {
                new Product{Id=3, Cost=31, Price=310, Supplier="Odd-e" },
                new Product{Id=4, Cost=41, Price=410, Supplier="Odd-e" },
                new Product{Id=5, Cost=51, Price=510, Supplier="Momo" },
                new Product{Id=6, Cost=61, Price=610, Supplier="Momo" },
                new Product{Id=7, Cost=71, Price=710, Supplier="Yahoo" },
                new Product{Id=8, Cost=18, Price=780, Supplier="Yahoo" },
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

        [TestMethod]
        public void TakeWhileMoreThan300()
        {
            var employees = RepositoryFactory.GetEmployees();
            var actual = employees.MyRealTakeWhile(e=>e.Age>30);

            var expected = new List<Employee>
            {
                new Employee{Name="Joe", Role=RoleType.Engineer, MonthSalary=100, Age=44, WorkingYear=2.6 } ,
                new Employee{Name="Tom", Role=RoleType.Engineer, MonthSalary=140, Age=33, WorkingYear=2.6} ,
                new Employee{Name="Kevin", Role=RoleType.Manager, MonthSalary=380, Age=55, WorkingYear=2.6} 
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void Sum()
        {
            var products = RepositoryFactory.GetProducts();
            var actual = products.MySum(p => p.Price);

            var expected = 3650;

            expected.ToExpectedObject().ShouldEqual(actual);
        }
        [Ignore]
        [TestMethod]
        public void Group_3_AndSum()
        {
            var products = RepositoryFactory.GetProducts();
            var actual = products.GroupSum(3,p => p.Price);

            var expected = new List<int>
            {
                630,1530,1490
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }
        [Ignore]
        [TestMethod]
        public void Group_5_AndSum()
        {
            var products = RepositoryFactory.GetProducts();
            var actual = products.GroupSum(5, p => p.Price);

            var expected = new List<int>
            {
                1550,2100
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }
        
        [TestMethod]
        public void AnyHasRecord()
        {
            var products = RepositoryFactory.GetProducts();
            var actual = products.MyAny();

            Assert.IsTrue(actual);

        }

        [TestMethod]
        public void All()
        {
            var products = RepositoryFactory.GetProducts();
            var actual = products.MyAll(p=>p.Price>200);

            Assert.IsFalse(actual);

        }

        [TestMethod]
        public void Distinct()
        {
            var employees = RepositoryFactory.GetEmployees();
            var actual = employees.MySelect(p=>p.Role).MyDistinct();
            var expected = new List<RoleType>
            {
                RoleType.Engineer,
                RoleType.OP,
                RoleType.Manager
            };
            expected.ToExpectedObject().ShouldEqual(actual.ToList());

        }
    }
}