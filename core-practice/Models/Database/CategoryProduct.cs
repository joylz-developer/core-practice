using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace core_practice.Models {
  public class CategoryProduct {
    public int Id { get; set; }
    public string Name { get; set; }

    public List<Product> Products { get; set; }
    public CategoryProduct() {
      Products = new List<Product>();
    }
  }
}
