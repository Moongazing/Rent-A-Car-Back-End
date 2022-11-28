using System;
using System.Collections.Generic;
using System.Text;
using TAO_Core.Entities;

namespace TAO_Entities.DTOs
{
  public class AmountDetailDto : IEntity
  {
    public int RentalId { get; set; }
    public int CarId { get; set; }
    public string CarName { get; set; }
    public decimal DailyPrice { get; set; }
    public int UserId { get; set; }
    public string UserFirstName { get; set; }
    public string UserLastName { get; set; }
    public DateTime RentDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public decimal Total { get; set; }



  }
}
