using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminAPIImplementation.Services
{
    public interface IFortellisConnectionCallbackService
    {
        public string getFortellisToken();
        public void approveFortellisActivationRequest(string accessToken, string connectionId, string status);
    }
}
