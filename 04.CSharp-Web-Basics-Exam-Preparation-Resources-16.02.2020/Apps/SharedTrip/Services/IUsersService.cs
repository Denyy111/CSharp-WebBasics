using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.Services
{
    public interface IUsersService
    {
        string GetUserId(string username, string password);

        void Create(string username, string email, string passord);

        bool IsUsernameAvailable(string username);

        bool IsEmailAvailable(string email);
    }
}
