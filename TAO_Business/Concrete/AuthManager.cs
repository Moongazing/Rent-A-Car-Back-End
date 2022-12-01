﻿using System;
using System.Collections.Generic;
using System.Text;
using TAO_Business.Abstract;
using TAO_Business.Constants;
using TAO_Core.Entities.Concrete;
using TAO_Core.Utilities.Results.Abstract;
using TAO_Core.Utilities.Results.Concrete;
using TAO_Core.Utilities.Results;
using TAO_Core.Utilities.Security.Hashing;
using TAO_Core.Utilities.Security.JWT;
using TAO_Entities.DTOs;
using TAO_Core.Aspects.Autofac.Logging;
using TAO_Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;

namespace TAO_Business.Concrete
{
  public class AuthManager : IAuthService
  {
    private IUserService _userService;
    private ITokenHelper _tokenHelper;

    public AuthManager(IUserService userService, ITokenHelper tokenHelper)
    {
      _userService = userService;
      _tokenHelper = tokenHelper;
    }

    [LogAspect(typeof(FileLogger))]
    public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password)
    {
      byte[] passwordHash, passwordSalt;
      HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
      var user = new User
      {
        Email = userForRegisterDto.Email,
        FirstName = userForRegisterDto.FirstName,
        LastName = userForRegisterDto.LastName,
        PasswordHash = passwordHash,
        PasswordSalt = passwordSalt,
        Status = true
      };
      _userService.Add(user);
      return new SuccessDataResult<User>(user, Messages.UserRegistered);
    }
    [LogAspect(typeof(FileLogger))]
    public IDataResult<User> Login(UserForLoginDto userForLoginDto)
    {
      var userToCheck = _userService.GetByMail(userForLoginDto.Email);

      if (userToCheck == null)
      {
        return new ErrorDataResult<User>(Messages.UserNotFound);
      }

      if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
      {
        return new ErrorDataResult<User>(Messages.PasswordError);
      }

      return new SuccessDataResult<User>(userToCheck, Messages.SuccessfulLogin);
    }
    [LogAspect(typeof(FileLogger))]
    public IResult UserExists(string email)
    {
      if (_userService.GetByMail(email) != null)
      {
        return new ErrorResult(Messages.UserAlreadyExists);
      }
      return new SuccessResult();
    }
    [LogAspect(typeof(FileLogger))]
    public IDataResult<AccessToken> CreateAccessToken(User user)
    {
      var claims = _userService.GetClaims(user);
      var accessToken = _tokenHelper.CreateToken(user, claims);
      return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
    }
  }
}
