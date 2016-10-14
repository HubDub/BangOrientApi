using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;  //this is for the key as well as the required words
using System.ComponentModel.DataAnnotations.Schema;  //this is for the databse generated options


namespace Bangazon.Models
{
  public class Customer
  {
    [Key]  //the primary key is assumed to be required. 
    public int CustomerId {get;set;}

    [Required]  //an empty value can never be put in if it's required. it'll reject. 
    [DataType(DataType.Date)]  //this is the format. he just wants the date not the time.
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]  //he wants database to generate value.
    public DateTime DateCreated {get;set;}  

    [Required]
    public string FirstName { get; set; }  //so the database will reject their submission if they don't give their first and last name. just using [required] sets that up

    [Required]
    public string LastName { get; set; }

    public ICollection<PaymentType> PaymentTypes; //this is the other side of the relationship. every customer can have many payment types. this establishes the relationship . this is the many side of the one to many with the payment types. 
  }
}