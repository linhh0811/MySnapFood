using Service.SnapFood.Share.Interface.Extentions;
using Service.SnapFood.Share.Model.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Application.Service
{
    public class RequestContext : IRequestContext
    {
        public CurrentUser CurrentUser { get; set; } = new CurrentUser();
    }
}
