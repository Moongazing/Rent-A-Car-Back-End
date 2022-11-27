﻿using System;
using System.Collections.Generic;
using System.Text;
using TAO_Core;

namespace TAO_Entities.DTOs
{
  public class UserForLoginDto : IDto
  {
    public string Email { get; set; }
    public string Password { get; set; }
  }
}
