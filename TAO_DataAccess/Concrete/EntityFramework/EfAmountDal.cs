using System;
using System.Collections.Generic;
using System.Text;
using TAO_Core.DataAccess.EntityFramework;
using TAO_DataAccess.Abstract;
using TAO_Entities.Concrete;
using TAO_Entities.DTOs;
using System.Linq;

namespace TAO_DataAccess.Concrete.EntityFramework
{
  public class EfAmountDal : EfEntityRepositoryBase<Amount, TAO_RentACarContext>, IAmountDal
  {
    public List<AmountDetailDto> GetAmountDetails()
    {
      using (TAO_RentACarContext context = new TAO_RentACarContext())
      {
        var result = from a in context.Amounts
                     join r in context.Rentals on a.CarId equals r.CarId
                     join c in context.Cars on a.CarId equals c.Id
                     join u in context.Users on a.UserId equals u.Id
                     select new AmountDetailDto
                     {
                       RentalId = r.Id,
                       CarId = c.Id,
                       CarName = c.Name,
                       DailyPrice = c.DailyPrice,
                       UserId = u.Id,
                       UserFirstName = u.FirstName,
                       UserLastName = u.LastName,
                       RentDate = r.RentDate,
                       ReturnDate = r.ReturnDate,
                       Total = a.Total
                     };




        return result.ToList();
      }
    }


  }
}
