using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using PubTransp.Adapters;
using PubTranspShared.Services;
using PubTranspShared.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Content.PM;

namespace PubTransp.Activities
{
    [Activity(Label = "Public Transport Search", MainLauncher = true, Icon = "@drawable/icon", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Activity
    {
        RoutesHeaderAdapter headerAdapter = null;
        IList<RowRoutes> routesHeaders = null;
        EditText streetNameSearch = null;
        Button searchButton = null;
        ListView listViewRoutes = null;
        TextView txtNotFound = null;
        ProgressBar progressBarLoading = null;  

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            streetNameSearch = FindViewById<EditText>(Resource.Id.StreetNameSearch);
            searchButton = FindViewById<Button>(Resource.Id.SearchButton);
            listViewRoutes = FindViewById<ListView>(Resource.Id.ListViewRoutes);
            txtNotFound = FindViewById<TextView>(Resource.Id.txtNotFound);
            progressBarLoading = FindViewById<ProgressBar>(Resource.Id.progressBarLoading);
            progressBarLoading.Visibility = ViewStates.Gone;
            searchButton.Click += async (sender, e) => {
                await LoadHeaders();
            };
            listViewRoutes.ItemClick += (sender, e) =>
            {
                Intent intendDet = new Intent(this, typeof(DetailsActivity));
                intendDet.PutExtra("RouteId", routesHeaders[e.Position].id);
                intendDet.PutExtra("RouteLongName", routesHeaders[e.Position].longName);
                StartActivity(intendDet);
            };
        }
        protected override void OnResume()
        {
            base.OnResume();

        }
        private async Task LoadHeaders()
        {
            headerAdapter = null;
            progressBarLoading.Visibility = ViewStates.Visible;
            routesHeaders = await RoutesBL.GetInstance().GetRoutesHeader(streetNameSearch.Text);
            if (routesHeaders.Count <= 0)
            {
                listViewRoutes.Adapter = null;
                txtNotFound.Visibility = ViewStates.Visible;
            }
            else
            {
                txtNotFound.Visibility = ViewStates.Invisible;
                headerAdapter = new RoutesHeaderAdapter(this, routesHeaders);
                listViewRoutes.Adapter = headerAdapter;
            }
            progressBarLoading.Visibility = ViewStates.Gone;
        }
    }
}

