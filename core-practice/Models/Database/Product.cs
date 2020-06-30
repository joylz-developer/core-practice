using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace core_practice.Models {
  public class Product {
    public int Id { get; set; }
    public string Name { get; set; }
    public float Price { get; set; }
    public float? OldPrice { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime AddingDate { get; set; }

    public int CategoryProductId { get; set; }
    public CategoryProduct CategoryProduct { get; set; }

    public List<Purchase> Purchase { get; set; }

    public Product() {
      Purchase = new List<Purchase>();
    }
  }
}
