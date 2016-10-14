using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Bangazon.Models
{
  public class Product
  {
    [Key]
    public int ProductId {get;set;}

    [Required]
    [DataType(DataType.Date)]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]  
    public DateTime DateCreated {get;set;}

    [Required]
    [StringLength(255)] //this limits how many characters they can use
    public string Description { get; set; }

    [Required]
    public double Price { get; set; }
    public ICollection<LineItem> LineItems; //he has not designated this as a certain type, it can be anything. the compiler is deciding what type it is. by setting it as an ICollection he's giving it more options, more flexibility.
    // public ICollection<CustomerFave> CustomerFavorites; we won't use this
  }
}