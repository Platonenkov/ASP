﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using WebStore.Domain.Entities;

namespace WebStore.Interfaces.Servcies
{
    interface IUserClient:IUserRoleStore<User>,
                            IUserClaimStore<User>,
                            IUserPasswordStore<User>,
                            IUserTwoFactorStore<User>,
                            IUserEmailStore<User>,
                            IUserPhoneNumberStore<User>,
                            IUserLoginStore<User>,
                            IUserLockoutStore<User>
    {
    }
}
