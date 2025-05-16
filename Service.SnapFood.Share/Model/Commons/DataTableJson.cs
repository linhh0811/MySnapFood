using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Share.Model.Commons
{
    public class DataTableJson
    {
        public DataTableJson() { }
        public DataTableJson Message(string exMessage)
        {
            this.exMessage = exMessage;

            return this;
        }
        public DataTableJson(string exMessage)
        {
            this.exMessage = exMessage;
        }
        public DataTableJson(string exMessage, object data)
        {
            recordsTotal = recordsTotal;
            recordsFiltered = recordsFiltered;
        }

        public DataTableJson(object data, int draw, int recordsTotal, int recordsFiltered)
        {
            this.data = data;
            this.draw = draw;
            this.recordsTotal = recordsTotal;
            this.recordsFiltered = recordsFiltered;
        }
        public DataTableJson(object data, int draw, int recordsTotal)
        {
            this.data = data;
            this.draw = draw;
            this.recordsTotal = recordsTotal;
            recordsFiltered = recordsTotal;
        }

#pragma warning disable IDE1006 // Bỏ qua thông báo cái này
        public int? draw { get; set; }

        public int? recordsTotal { get; set; }
        public int? recordsFiltered { get; set; }
        public object? data { get; set; }
        public string? exMessage { get; set; }
        public string? querytext { get; set; }
#pragma warning restore IDE1006 
    }
}
