using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using core_practice.Code;

namespace core_practice.Models {
  public class Purchase {
    public int Id { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime Date { get; set; }
    public EStatusPurchase Status { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }

    public Purchase() {
      Status = EStatusPurchase.Expected;
    }
  }
}
