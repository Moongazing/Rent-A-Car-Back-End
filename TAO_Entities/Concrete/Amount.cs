using System;
using System.Collections.Generic;
using System.Text;
using TAO_Core.Entities;

namespace TAO_Entities.Concrete
{
  public class Amount:IEntity
  {
    public int Id { get; set; }
    public int CarId { get; set; }
    public int UserId { get; set; }
    public decimal Total { get; set; }
    public DateTime PaymentDay { get; set; }

  }
}
