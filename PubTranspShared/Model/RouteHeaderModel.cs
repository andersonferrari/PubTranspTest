using System;
using System.Collections.Generic;
using System.Text;

namespace PubTranspShared.Model
{

    public class RowRoutes
    {
        public int id { get; set; }
        public string shortName { get; set; }
        public string longName { get; set; }
        public string lastModifiedDate { get; set; }
        public int agencyId { get; set; }
    }

    public class RouteHeaderModel
    {
        public List<RowRoutes> rows { get; set; }
        public int rowsAffected { get; set; }
    }
}
