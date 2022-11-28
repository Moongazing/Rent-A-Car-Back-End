using System;
using System.Collections.Generic;
using System.Text;
using TAO_Business.Abstract;
using TAO_Business.BussinesAspects.Autofac;
using TAO_Business.Constants;
using TAO_Core.Utilities.Results;
using TAO_Core.Utilities.Results.Abstract;
using TAO_Core.Utilities.Results.Concrete;
using TAO_DataAccess.Abstract;
using TAO_DataAccess.Concrete.EntityFramework;
using TAO_Entities.Concrete;
using TAO_Entities.DTOs;

namespace TAO_Business.Concrete
{
  public class AmountManager : IAmountService
  {
    IAmountDal _amountDal;
    public AmountManager(IAmountDal amountDal)
    {
      _amountDal = amountDal;
    }
    [SecuredOperation("admin,amount.add")]
    public IResult Add(Amount amount)
    {
      _amountDal.Delete(amount);
      return new SuccessResult(Messages.AmountDeleted);
    }
    [SecuredOperation("admin,amount.delete")]
    public IResult Delete(Amount amount)
    {
      _amountDal.Add(amount);
      return new SuccessResult(Messages.AmountAdded);
    }

    [SecuredOperation("admin,amount.detail")]
    public IDataResult<List<AmountDetailDto>> GetAmountDetail()
    {
      return new SuccessDataResult<List<AmountDetailDto>>(_amountDal.GetAmountDetails());
    }
    [SecuredOperation("admin,amount.add")]
    public IResult Update(Amount amount)
    {
      _amountDal.Update(amount);
      return new SuccessResult(Messages.AmountUpdated);
    }
  }
}
