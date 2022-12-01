using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TAO_Business.Abstract;
using TAO_Business.BussinesAspects.Autofac;
using TAO_Business.Constants;
using TAO_Business.ValidationRules.FluentValidation;
using TAO_Core.Aspects.Autofac.Caching;
using TAO_Core.Aspects.Autofac.Logging;
using TAO_Core.Aspects.Autofac.Performance;
using TAO_Core.Aspects.Autofac.Validation;
using TAO_Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using TAO_Core.Utilities.Business;
using TAO_Core.Utilities.Results;
using TAO_Core.Utilities.Results.Abstract;
using TAO_Core.Utilities.Results.Concrete;
using TAO_DataAccess.Abstract;
using TAO_DataAccess.Concrete.EntityFramework;
using TAO_Entities.Concrete;
using TAO_Entities.DTOs;

namespace TAO_Business.Concrete
{
  public class RentalManager : IRentalService
  {
    IRentalDal _rentalDal;
    public RentalManager(IRentalDal rentalDal)
    {
      _rentalDal = rentalDal;
    }
    [LogAspect(typeof(FileLogger))]
    [PerformanceAspect(5)]
    [SecuredOperation("rental.add,admin")]
    [ValidationAspect(typeof(RentalValidator))]
    public IResult Add(Rental rental)
    {
      var result = BusinessRules.Run(CheckReturnDateForAvailableCar(rental.CarId));
      if (result != null)
      {
        return result;
      }
      _rentalDal.Add(rental);
      return new SuccessResult(Messages.RentalAdded);
    }
    [LogAspect(typeof(FileLogger))]
    [PerformanceAspect(5)]
    [CacheAspect]
    [SecuredOperation("admin,rental.availablelist")]
    public IDataResult<List<Rental>> AvailableDate(DateTime min, DateTime max)
    {
      return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(r => r.RentDate >= min && r.RentDate <= max));
    }
    [LogAspect(typeof(FileLogger))]
    [PerformanceAspect(5)]
    [SecuredOperation("admin,rental.delete")]
    public IResult Delete(Rental rental)
    {
      _rentalDal.Delete(rental);
      return new SuccessResult(Messages.RentalDeleted);
    }
    [LogAspect(typeof(FileLogger))]
    [PerformanceAspect(5)]
    [CacheAspect]
    [SecuredOperation("admin,rental.list")]
    public IDataResult<List<Rental>> GetAll()
    {
      return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());
    }
    [LogAspect(typeof(FileLogger))]
    [PerformanceAspect(5)]
    [CacheAspect]
    [SecuredOperation("admin,rental.getbyid")]
    public IDataResult<Rental> GetById(int rentalId)
    {
      return new SuccessDataResult<Rental>(_rentalDal.Get(r => r.Id == rentalId));
    }
    [LogAspect(typeof(FileLogger))]
    [PerformanceAspect(7)]
    [CacheAspect]
    [SecuredOperation("admin,rental.detail")]
    public IDataResult<List<RentalDetailDto>> GetRentalDetails()
    {
      return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetails());

    }
    [PerformanceAspect(5)]
    [SecuredOperation("admin,rental.update")]
    [ValidationAspect(typeof(RentalValidator))]
    public IResult Update(Rental rental)
    {
      _rentalDal.Update(rental);
      return new SuccessResult();
    }

    #region Rules
    private IResult CheckReturnDateForAvailableCar(int carId)
    {
      var result = _rentalDal.GetAll(r => r.ReturnDate == null).Where(r => r.CarId == carId);
      return new ErrorResult(Messages.NotAvailableCarForRental);
    }

   


    #endregion
  }
}
