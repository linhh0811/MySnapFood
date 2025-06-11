using Service.SnapFood.Share.Model.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Share.Interface.Extentions
{
    public interface IRequestContext
    {
        CurrentUser CurrentUser { get; set; }
    }
}
