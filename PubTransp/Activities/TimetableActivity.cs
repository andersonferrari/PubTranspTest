using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PubTransp.Adapters;
using PubTranspShared.Services;
using PubTranspShared.Model;
using System.Threading.Tasks;

namespace PubTransp.Activities
{
    [Activity(Label = "TimeTable")]
    public class TimetableActivity : Activity
    {
        RouteTimeTableAdapter timeTableWeekdayAdapter = null;
        RouteTimeTableAdapter timeTableSaturdayAdapter = null;
        RouteTimeTableAdapter timeTableSundayAdapter = null;
        IList<RowTimeTable> routeTimeTable;
        GridView gridWeekdays = null;
        GridView gridSaturdays = null;
        GridView gridSundays = null;
        Button btnBack = null;
        int routeId = 0;
        string routeLongName = "";
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Timetable);

            routeId = Intent.GetIntExtra("RouteId", 0);
            routeLongName = Intent.GetStringExtra("RouteLongName");
            gridWeekdays = FindViewById<GridView>(Resource.Id.gridWeekdays);
            gridSaturdays = FindViewById<GridView>(Resource.Id.gridSaturdays);
            gridSundays = FindViewById<GridView>(Resource.Id.gridSundays);
            btnBack = FindViewById<Button>(Resource.Id.btnBack);
            btnBack.Click += (sender, e) => {
                Finish();
            };
        }
        protected override void OnResume()
        {
            base.OnResume();
            LoadTimeTable();
        }
        private async Task LoadTimeTable()
        {
            routeTimeTable = await RoutesBL.GetInstance().GetRouteTimeTable(routeId);
            IList<RowTimeTable> timetableWeekday = (IList<RowTimeTable>)routeTimeTable.Where(p => p.calendar.ToUpper() == "WEEKDAY").ToList(); ;
            IList<RowTimeTable> timetableSaturday = (IList<RowTimeTable>)routeTimeTable.Where(p => p.calendar.ToUpper() == "SATURDAY").ToList(); ;
            IList<RowTimeTable> timetableSunday = (IList<RowTimeTable>)routeTimeTable.Where(p => p.calendar.ToUpper() == "SUNDAY").ToList(); ;
            timeTableWeekdayAdapter = new RouteTimeTableAdapter(this, timetableWeekday);
            timeTableSaturdayAdapter = new RouteTimeTableAdapter(this, timetableSaturday);
            timeTableSundayAdapter = new RouteTimeTableAdapter(this, timetableSunday);
            gridWeekdays.Adapter = timeTableWeekdayAdapter;
            gridSaturdays.Adapter = timeTableSaturdayAdapter;
            gridSundays.Adapter = timeTableSundayAdapter;
        }
    }
}