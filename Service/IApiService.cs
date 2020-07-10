using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VkProxy.Models;

namespace VkProxy.Service
{
    public interface IApiService
    {
        Task<bool> CheckId(ParamModel model);
        Task<string> GetName(string tag);
        Task<string> CheckClub(ClubModel model);
    }
}
