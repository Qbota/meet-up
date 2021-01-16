using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Application.AIs
{
    public interface IRPCClient
    {
       Task<string> SendAsync(string message);
    }
}
