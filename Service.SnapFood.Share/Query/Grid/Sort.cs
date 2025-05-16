using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Share.Query.Grid
{
    [Serializable]
    public struct Sort
    {
#pragma warning disable IDE1006 // Naming Styles
        public string field { get; set; }

        public string dir { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
