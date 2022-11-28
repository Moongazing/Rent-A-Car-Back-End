using System;
using System.Collections.Generic;
using System.Text;
using TAO_Core.Utilities.Results;
using TAO_Core.Utilities.Results.Abstract;
using TAO_Entities.Concrete;
using TAO_Entities.DTOs;

namespace TAO_Business.Abstract
{
  public interface IAmountService
  {
    IResult Add(Amount amount);
    IResult Update(Amount amount);
    IResult Delete(Amount amount);
    IDataResult<List<AmountDetailDto>> GetAmountDetail();
  }
}
