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
using Android.Util;

namespace PubTransp.Activities
{
    [Activity(Label = "Route Details")]
    public class DetailsActivity : Activity
    {
        RouteStreetsAdapter streetAdapter=null;
        IList<RowStreets> routeStreets;
        TextView routeHeader = null;
        ListView listViewStreets = null;
        Button btnTimetable = null;
        Button btnBack=null;
        View viewBack = null;
        int routeId = 0;
        string routeLongName = "";
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Details);
            routeHeader = FindViewById<TextView>(Resource.Id.RouteHeader);
            listViewStreets = FindViewById<ListView>(Resource.Id.listViewStreets);
            LayoutInflater inflater = (LayoutInflater)this.GetSystemService(Context.LayoutInflaterService);
            viewBack = inflater.Inflate(Resource.Layout.back, null) ;
            btnBack = viewBack.FindViewById<Button>(Resource.Id.btnBack);
            btnBack.Click += (sender, e) => {
                Finish();
            };
            listViewStreets.AddFooterView(viewBack);
            btnTimetable = FindViewById<Button>(Resource.Id.btnTimetable);
            btnTimetable.Click += (sender, e) =>
            {
                Intent intendTT = new Intent(this, typeof(TimetableActivity));
                intendTT.PutExtra("RouteId", routeId);
                intendTT.PutExtra("RouteLongName", routeLongName);
                StartActivity(intendTT);
            };
        }

        protected override void OnResume()
        {
            base.OnResume();
            LoadDetails();
        }
        private async Task LoadDetails()
        {
            routeId = Intent.GetIntExtra("RouteId", 0);
            routeLongName = Intent.GetStringExtra("RouteLongName");
            routeHeader.Text = $"Route ({routeId}) - {routeLongName}";
            routeStreets = await RoutesBL.GetInstance().GetRouteStreets( routeId );
            streetAdapter = new RouteStreetsAdapter(this, routeStreets);
            listViewStreets.Adapter = streetAdapter;
        }
    }
}