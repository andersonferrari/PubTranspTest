using System;
using System.Collections.Generic;
using System.Text;

namespace PubTranspShared.Model
{
    public class RowStreets
    {
        public int id { get; set; }
        public string name { get; set; }
        public int sequence { get; set; }
        public int route_id { get; set; }
    }

    public class RouteDetailModel
    {
        public List<RowStreets> rows { get; set; }
        public int rowsAffected { get; set; }
    }
}
