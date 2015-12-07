using System;
using System.Collections.Generic;
using System.Text;

namespace PubTranspShared.Model
{
    public class RowTimeTable
    {
        public int id { get; set; }
        public string calendar { get; set; }
        public string time { get; set; }
    }

    public class RouteTimeTableModel
    {
        public List<RowTimeTable> rows { get; set; }
        public int rowsAffected { get; set; }
    }
}
