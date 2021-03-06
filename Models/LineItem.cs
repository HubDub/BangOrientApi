using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Bangazon.Models
{
  public class LineItem
  {
    [Key]  //primary key
    public int LineItemId {get;set;}
    [Required]
    public int OrderId { get; set; } //foreign key
    public Order Order { get; set; }
    [Required]
    public int ProductId { get; set; }  //foreign key
    public Product Product { get; set; }
  }
}