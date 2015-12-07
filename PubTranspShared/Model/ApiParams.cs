using System;
using System.Collections.Generic;
using System.Text;

namespace PubTranspShared.Model
{
    public class ApiParams
    {
        public Params @params { get; set; }
    }

    public class Params
    {
        public string stopName { get; set; }
        public int routeId { get; set; }
    }

}
