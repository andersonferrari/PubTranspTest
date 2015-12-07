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
using PubTranspShared.Model;

namespace PubTransp.Adapters
{
    class RouteTimeTableAdapter : BaseAdapter<RowTimeTable>
    {
        Activity context = null;
        IList<RowTimeTable> routes = null;

        public RouteTimeTableAdapter(Activity context, IList<RowTimeTable> routes) : base()
        {
            this.context = context;
            this.routes = routes;
        }

        public override RowTimeTable this[int position]
        {
            get
            {
                return routes[position];
            }
        }

        public override int Count
        {
            get
            {
                return routes.Count();
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = routes[position];
            var view = convertView as TextView ?? new TextView(context);
            view.Gravity = GravityFlags.CenterHorizontal | GravityFlags.CenterVertical;
            view.SetBackgroundResource(Resource.Drawable.border);
            view.SetText((string)item.time, TextView.BufferType.Normal);
            return view;
        }
    }
}