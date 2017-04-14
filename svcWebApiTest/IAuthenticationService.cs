using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VgFc.Test.Services
{
    /// <summary>
    /// Interface to manage the authentication process
    /// </summary>
    public interface IAuthenticationService
    {
        bool Authenticate(string user, string password);
    }
}