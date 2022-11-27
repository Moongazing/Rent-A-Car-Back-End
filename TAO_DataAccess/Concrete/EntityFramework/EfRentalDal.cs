using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TAO_Core.DataAccess.EntityFramework;
using TAO_DataAccess.Abstract;
using TAO_Entities.Concrete;
using TAO_Entities.DTOs;
using System.Linq;
using TAO_DataAccess.Concrete.EntityFramework;

namespace TAO_DataAccess.Concrete.EntityFramework
{
  public class EfRentalDal : EfEntityRepositoryBase<Rental, TAO_RentACarContext>, IRentalDal
  {
    public List<RentalDetailDto> GetRentalDetails()
    {
      using (TAO_RentACarContext context = new TAO_RentACarContext())
      {
        var result = from r in context.Rentals
                     join c in context.Cars on r.CarId equals c.Id
                     join b in context.Brands on c.BrandId equals b.Id
                     join co in context.Colors on c.ColorId equals co.Id
                     join u in context.Users on r.UserId equals u.Id
                     select new RentalDetailDto
                     {
                       RentalId = r.Id,
                       CarId = c.Id,
                       CarName = c.Name,
                       ColourName = co.Name,
                       BrandName = b.Name,
                       UserFirstName = u.FirstName,
                       UserLastName = u.LastName,
                       RentDate = r.RentDate,
                       ReturnDate = r.ReturnDate
                     };

        return result.ToList();

      }
    }
  }
}

