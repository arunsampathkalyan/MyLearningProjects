using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeneralConcepts.Lazy
{
    public class LazyIntialisation
    {
    }

    public class ProductItem
    {
        public ProductItem()
        {
            Thread.Sleep(2000);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public static IList<ProductItem> GetSampleProductList()
        {
            var prd1 = new ProductItem() { Id = 101, Title = "Product101" };
            var prd2 = new ProductItem() { Id = 102, Title = "Product102" };
            var prd3 = new ProductItem() { Id = 103, Title = "Product103" };
            var prd4 = new ProductItem() { Id = 104, Title = "Product104" };

            return new List<ProductItem>(new[] { prd1, prd2, prd3, prd4 });
        }

        public static IList<Lazy<ProductItem>> GetSampleProductListLazy()
        {
            var prd1 = new Lazy<ProductItem>(() => new ProductItem() { Id = 101, Title = "Product101" });
            var prd2 = new Lazy<ProductItem>(() => new ProductItem() { Id = 102, Title = "Product102" });
            var prd3 = new Lazy<ProductItem>(() => new ProductItem() { Id = 103, Title = "Product103" });
            var prd4 = new Lazy<ProductItem>(() => new ProductItem() { Id = 104, Title = "Product104" });

            return new List<Lazy<ProductItem>>(new[] { prd1, prd2, prd3, prd4 });
        }
    }
}
