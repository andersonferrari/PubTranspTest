using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using PubTranspShared.Model;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PubTranspShared.Data
{
    class RoutesDAL
    {
        private HttpClient http;
        public RoutesDAL()
        {
            http = new HttpClient();
            //TODO: put end point on config file
            http.BaseAddress = new Uri("https://api.appglu.com/v1/queries/");
            http.DefaultRequestHeaders.Accept.Clear();
            http.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("Basic",Convert.ToBase64String(
                    System.Text.ASCIIEncoding.ASCII.GetBytes(
                        string.Format("{0}:{1}", "WKD4N7YMA1uiM8V", "DtdTtzMLQlA0hk2C1Yi5pLyVIlAQ68")))); //TODO: put user and pdw on safe place 
            http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            http.DefaultRequestHeaders.Add("X-AppGlu-Environment", "staging");
            http.MaxResponseContentBufferSize = 256000;
        }

        public async Task<IList<RowRoutes>> GetRoutesHeaders(string StreetStrSearch)
        {
            RouteHeaderModel routes = new RouteHeaderModel();
            ApiParams apiP = new ApiParams(  );
            var p = new Params();
            p.stopName = $"%{StreetStrSearch}%";
            apiP.@params = p;

            var json = JsonConvert.SerializeObject(apiP);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");  
            HttpResponseMessage response = await http.PostAsync("findRoutesByStopName/run", content );
            if (response.IsSuccessStatusCode)
            {
                string strResponse = response.Content.ReadAsStringAsync().Result;
                routes = JsonConvert.DeserializeObject<RouteHeaderModel>(strResponse);
            }
            return routes.rows;
        }
        public async Task<IList<RowStreets>> GetRouteStreets(int routeId)
        {
            RouteDetailModel routes = new RouteDetailModel();
            ApiParams apiP = new ApiParams();
            var p = new Params();
            p.routeId = routeId;
            apiP.@params = p;

            var json = JsonConvert.SerializeObject(apiP);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await http.PostAsync("findStopsByRouteId/run", content);
            if (response.IsSuccessStatusCode)
            {
                string strResponse = response.Content.ReadAsStringAsync().Result;
                routes = JsonConvert.DeserializeObject<RouteDetailModel>(strResponse);
            }
            return routes.rows;
        }

        public async Task<IList<RowTimeTable>> GetRouteTimeTable(int routeId)
        {
            RouteTimeTableModel routes = new RouteTimeTableModel();
            ApiParams apiP = new ApiParams();
            var p = new Params();
            p.routeId = routeId;
            apiP.@params = p;

            var json = JsonConvert.SerializeObject(apiP);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            try {
                response = await http.PostAsync("findDeparturesByRouteId/run", content);
            }
            catch (Exception ex){
                Console.WriteLine(ex.Message);
            }
            if (response.IsSuccessStatusCode)
            {
                string strResponse = response.Content.ReadAsStringAsync().Result;
                routes = JsonConvert.DeserializeObject<RouteTimeTableModel>(strResponse);
            }
            return routes.rows;
        }
    }
}
