using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VgFc.Test.Services
{
    /// <summary>
    /// Class to manage the authentication process
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        public bool Authenticate(string user, string password)
        {
            return true;
        }
    }
}