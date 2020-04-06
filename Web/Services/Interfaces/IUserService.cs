﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.Users;

namespace Web.Services.Interfaces
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
        User GetById(long id);
        User Create(User user, string password);
        void Update(User user, string password = null);
        void Delete(long id);
    }

}
