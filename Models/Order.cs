using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Bangazon.Models
{
  public class Order
  {
    [Key]
    public int OrderId {get;set;}

    [Required]
    [DataType(DataType.Date)]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime DateCreated {get;set;}

    
    [DataType(DataType.Date)]
    public DateTime? DateCompleted {get;set;}  //this will be null until it is actually complete

    public int CustomerId {get;set;}  //this is another foreign key relationship to the customer table.
    public Customer Customer {get;set;}  

    public int? PaymentTypeId {get;set;}  //this can be null until customer is ready to pay for it. the PaymentTypeId is the foreign key for the payment type table... this is clarified in the next line. table is like spreadsheet
    public PaymentType PaymentType {get;set;} //this creates the physical connection to the paymenttype.cs

    public ICollection<LineItem> LineItems;
  }
}