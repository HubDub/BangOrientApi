using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Bangazon.Models
{
  public class PaymentType
  {
    [Key]
    public int PaymentTypeId {get;set;}  //here you see the primary key is PaymentTypeId which is created in Order.cs. 

    [Required]
    [DataType(DataType.Date)]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime DateCreated {get;set;}

    [Required]
    [StringLength(12)]
    public string Description { get; set; }

    [Required]
    [StringLength(20)]
    public string AccountNumber { get; set; }
    public int CustomerId {get;set;} //relates paymenttype to customer. every payment type has one customemr so one customerID. this is the one side of a one to many 
    public Customer Customer {get;set;}
  }
}