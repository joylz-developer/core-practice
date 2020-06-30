using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace core_practice.Models {
  public class User {
    public int Id { get; set; }
    public string Name { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime CreateDate { get; set; }

    public List<Purchase> Purchases { get; set; }
    public User() {
      Purchases = new List<Purchase>();
    }
  }
}
