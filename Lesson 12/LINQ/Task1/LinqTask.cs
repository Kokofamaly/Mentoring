using System;
using System.Collections.Generic;
using System.Linq;
using Task1.DoNotChange;

namespace Task1
{
    public static class LinqTask
    {
        public static IEnumerable<Customer> Linq1(IEnumerable<Customer> customers, decimal limit)
        {
            var result = customers.Where(c => c.Orders.Sum(o => o.Total) > limit);
            return result;
        }

        public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers
        )
        {
            var result = customers.GroupJoin(
                suppliers, 
                c => new {c.Country, c.City}, 
                s => new {s.Country, s.City}, 
                (c, s) => (customer: c, suppliers: s));

            return result;
        }

        public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2UsingGroup(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers
        )
        {
            var groupedSuppliers = suppliers.GroupBy(s => new {s.Country, s.City});

            var result = customers.Select(c => (customer: c, suppliers: groupedSuppliers.FirstOrDefault(g => g.Key.Country == c.Country && g.Key.City == c.City) ?? Enumerable.Empty<Supplier>()));

            return result;
        }

        public static IEnumerable<Customer> Linq3(IEnumerable<Customer> customers, decimal limit)
        {
            var result = customers.Where(c => c.Orders.Length > 0 && c.Orders.Sum(o => o.Total) > limit);
            return result;        
        }

        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq4(
            IEnumerable<Customer> customers
        )
        {
            var result = customers.Where(c => c.Orders.Length > 0).Select(
                c => (
                    customer: c, 
                    dateOfEntry: c.Orders.Min(o => o.OrderDate)
                    )
                );
            
            return result;
        }

        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq5(
            IEnumerable<Customer> customers
        )
        {
            var result = customers.Where(c => c.Orders.Length > 0)
                .Select(c => (
                customer: c, 
                dateOfEntry: c.Orders.Min(o => o.OrderDate)
                )
            )
            .OrderBy(obj => obj.dateOfEntry.Year)
            .ThenBy(obj => obj.dateOfEntry.Month)
            .ThenByDescending(obj => obj.customer.Orders.Sum(o => o.Total))
            .ThenBy(obj => obj.customer.CustomerID);

            return result;
        }

        public static IEnumerable<Customer> Linq6(IEnumerable<Customer> customers)
        {
            var result = customers.Where(
                c => c.Region == null 
                || !c.Phone.Contains('(') 
                || !c.PostalCode.All(pc => char.IsDigit(pc)));

            return result;
            
        }

        public static IEnumerable<Linq7CategoryGroup> Linq7(IEnumerable<Product> products)
        {

            var result = products.GroupBy(p => p.Category).Select(categoryGroup => new Linq7CategoryGroup {
                Category = categoryGroup.Key,
                UnitsInStockGroup = categoryGroup.GroupBy(p => p.UnitsInStock)
                    .Select(stockGroup => new Linq7UnitsInStockGroup
                    {
                        UnitsInStock = stockGroup.Key,
                        Prices = stockGroup.OrderBy(p => p.UnitPrice).Select(p => p.UnitPrice)
                    }) 
                });
            return result;
            
            /* example of Linq7result

             category - Beverages
	            UnitsInStock - 39
		            price - 18.0000
		            price - 19.0000
	            UnitsInStock - 17
		            price - 18.0000
		            price - 19.0000
             */

        }

        public static IEnumerable<(decimal category, IEnumerable<Product> products)> Linq8(
            IEnumerable<Product> products,
            decimal cheap,
            decimal middle,
            decimal expensive
        )
        {
            var result = products
                .GroupBy(p =>
                    p.UnitPrice <= cheap ? cheap :
                    p.UnitPrice <= middle ? middle :
                    expensive
                )
                .Select(g => (category: g.Key, products: g.AsEnumerable()));

            return result;
        }

        public static IEnumerable<(string city, int averageIncome, int averageIntensity)> Linq9(
            IEnumerable<Customer> customers
        )
        {
            var result = customers.GroupBy(c => c.City).Select(g => (city: g.Key, averageIncome: (int)Math.Round(g.Average(c => c.Orders.Sum(o => o.Total))), averageIntensity: (int)Math.Round(g.Average(c => c.Orders.Count()))));
            return result;
        }

        public static string Linq10(IEnumerable<Supplier> suppliers)
        {
            var countries = suppliers.Select(s => s.Country).Distinct().OrderBy(c => c.Length).ThenBy(c => c);
            var result = string.Join("",countries);
            return result;
        }
    }
}