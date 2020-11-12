using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoDisplayApp.UILibrary.API
{
    public interface IAPIHelper
    {
        Task<string> GetAsync(string requestUri, Dictionary<string, string> additionalHeaders = null);
    }
}
