using Service.SnapFood.Share.Model.ServiceCustomHttpClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Share.Interface.Extentions
{
    public interface ICallServiceRegistry
    {
        Task<ResultAPI> Delete(ApiRequestModel apiRequestModel);
        Task<ResultAPI> Put(ApiRequestModel apiRequestModel, object data);
        Task<ResultAPI> Get<T>(ApiRequestModel apiRequestModel);
        Task<ResultAPI> Post<T>(ApiRequestModel apiRequestModel, object data);
    }
}
