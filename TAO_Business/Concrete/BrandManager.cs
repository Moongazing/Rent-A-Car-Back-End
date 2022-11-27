using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using TAO_Business.Abstract;
using TAO_Business.BussinesAspects.Autofac;
using TAO_Business.Constants;
using TAO_Business.ValidationRules.FluentValidation;
using TAO_Core.Aspects.Autofac.Caching;
using TAO_Core.Aspects.Autofac.Validation;
using TAO_Core.Utilities.Business;
using TAO_Core.Utilities.Results;
using TAO_Core.Utilities.Results.Abstract;
using TAO_Core.Utilities.Results.Concrete;
using TAO_DataAccess.Abstract;
using TAO_Entities.Concrete;

namespace TAO_Business.Concrete
{
  public class BrandManager : IBrandService
  {
    IBrandDal _brandDal;
    public BrandManager(IBrandDal brandDal)
    {
      _brandDal = brandDal;
    }

    [SecuredOperation("admin,brand.add")]
    [ValidationAspect(typeof(BrandValidator))]
    [CacheRemoveAspect("IBrandService.Get")]

    public IResult Add(Brand brand)
    {
      var result = BusinessRules.Run(CheckIfBrandNameExists(brand.Name));
      if(result != null)
      {
        return result;
      }
      _brandDal.Add(brand);
      return new SuccessResult(Messages.BrandAdded);
    }

    [SecuredOperation("admin,brand.delete")]
    public IResult Delete(Brand brand)
    {
      _brandDal.Delete(brand);
      return new SuccessResult(Messages.BrandDeleted);
    }

    [SecuredOperation("admin,brand.list")]
    public IDataResult<List<Brand>> GetAll()
    {
      return new SuccessDataResult<List<Brand>>(_brandDal.GetAll());
    }
    [SecuredOperation("admin,brand.getbyid")]
    public IDataResult<Brand> GetById(int brandId)
    {
      return new SuccessDataResult<Brand>( _brandDal.Get(b => b.Id == brandId));
    }

    [SecuredOperation("admin,brand.update")]
    [ValidationAspect(typeof(BrandValidator))]
    public IResult Update(Brand brand)
    {
      _brandDal.Update(brand);
      return new SuccessResult(Messages.BrandUpdated);
    }


    #region Rules
    private IResult CheckIfBrandNameExists(string brandName)
    {
      var result = _brandDal.GetAll(b => b.Name == brandName).Count;
      if(result > 0 )
      {
        return new ErrorResult(Messages.BrandNameAlreadyExists);
      }
      return new SuccessResult();
    }

    #endregion
  }
}
