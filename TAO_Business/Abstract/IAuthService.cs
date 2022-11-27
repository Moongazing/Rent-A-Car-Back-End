﻿using System;
using System.Collections.Generic;
using System.Text;
using TAO_Core.Entities.Concrete;
using TAO_Core.Utilities.Results.Abstract;
using TAO_Core.Utilities.Results;
using TAO_Core.Utilities.Security.JWT;
using TAO_Entities.DTOs;

namespace TAO_Business.Abstract
{
  public interface IAuthService
  {
    IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password);
    IDataResult<User> Login(UserForLoginDto userForLoginDto);
    IResult UserExists(string email);
    IDataResult<AccessToken> CreateAccessToken(User user);
  }
}
