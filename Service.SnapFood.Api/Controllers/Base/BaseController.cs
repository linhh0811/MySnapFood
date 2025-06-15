using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.SnapFood.Share.Interface.Extentions;
using Service.SnapFood.Share.Model.Commons;

namespace Service.SnapFood.Api.Controllers.Base
{
    public class BaseController : ControllerBase
    {

        /// <summary>hghghg
        /// 
        /// </summary>
        protected CurrentUser CurrentUser { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestContext"></param>
        public BaseController(IRequestContext requestContext)
        {
            CurrentUser = requestContext.CurrentUser;
        }

    }
}
