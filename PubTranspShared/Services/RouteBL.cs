using System;
using System.Collections.Generic;
using System.Text;
using PubTranspShared.Model;
using PubTranspShared.Data;
using System.Threading.Tasks;
using System.Linq;

namespace PubTranspShared.Services
{
    class RoutesBL
    {
        public enum TimeTableDay
        {
            All,
            WeekDay,
            Saturday,
            Sunday
        }
        private RoutesDAL dal = null;
        private static RoutesBL instance;//Singleton pattern in order to reuse the object along the app
        public static RoutesBL GetInstance()
        {
            if (instance == null)
            {
                instance = new RoutesBL();
            }
            return instance;
        }
        private RoutesBL()
        {
            dal = new RoutesDAL();
        }
        public async Task<IList<RowRoutes>> GetRoutesHeader(string StreetStrSearch)
        {
            return await dal.GetRoutesHeaders(StreetStrSearch); ;
        }

        public async Task<IList<RowStreets>> GetRouteStreets(int routeId)
        {
            return await dal.GetRouteStreets(routeId); 
        }
        public async Task<IList<RowTimeTable>> GetRouteTimeTable(int routeId)
        {
            IList<RowTimeTable> timetable = await dal.GetRouteTimeTable(routeId);
            return timetable;
        }
    }
}
