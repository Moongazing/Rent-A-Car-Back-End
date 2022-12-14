using System;
using System.Collections.Generic;
using System.Text;
using TAO_Core.DataAccess;
using TAO_Entities.Concrete;
using TAO_Entities.DTOs;

namespace TAO_DataAccess.Abstract
{
  public interface ICarDal : IEntityRepository<Car>
  {
    List<CarDetailDto> GetCarDetails();
  }
}
