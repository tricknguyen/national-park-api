using Park.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Park.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string name);
        User Authenticate(string username, string password);
        User Register(string username, string password);
    }
}
