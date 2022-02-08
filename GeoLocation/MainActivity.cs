using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Linq;
using Xamarin.Essentials;

namespace GeoLocation
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]

    public class MainActivity : AppCompatActivity
    {
        private TextView textView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            GetLatLong();
            GetAddressFromLatLong();
            textView = FindViewById<TextView>(Resource.Id.textView);

        }

        private async void GetAddressFromLatLong()
        {
            try
            {
                var lat = 47.673988;
                var lon = -122.121513;

                var placemarks = await Geocoding.GetPlacemarksAsync(lat, lon);

                var placemark = placemarks?.FirstOrDefault();
                if (placemark != null)
                {
                    var geocodeAddress =
                        $"AdminArea:       {placemark.AdminArea}\n" +
                        $"CountryCode:     {placemark.CountryCode}\n" +
                        $"CountryName:     {placemark.CountryName}\n" +
                        $"FeatureName:     {placemark.FeatureName}\n" +
                        $"Locality:        {placemark.Locality}\n" +
                        $"PostalCode:      {placemark.PostalCode}\n" +
                        $"SubAdminArea:    {placemark.SubAdminArea}\n" +
                        $"SubLocality:     {placemark.SubLocality}\n" +
                        $"SubThoroughfare: {placemark.SubThoroughfare}\n" +
                        $"Thoroughfare:    {placemark.Thoroughfare}\n";

                    Log.Debug("jfa;lkssadfassahjhas;fash", geocodeAddress);
                    string address = geocodeAddress.ToString();
                    textView.Text = address;
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature not supported on device
                Log.Debug("jfa;lkssadfassahjhas;fashkjasdk;asjosa", fnsEx.Message);
            }
            catch (Exception ex)
            {
                Log.Debug("jfa;lkssadfassahjhas;fashkjasdk;asjosa", ex.Message);
            }
        }

        private async void GetLatLong()
        {
            try
            {
                var address = "Microsoft Building 25 Redmond WA USA";
                var locations = await Geocoding.GetLocationsAsync(address);

                var location = locations?.FirstOrDefault();
                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                Log.Debug("jfa;lkssadfassahjhas;fashkjasdk;asjosa", fnsEx.Message);
            }
            catch (Exception ex)
            {
                Log.Debug("jfa;lkssadfassahjhas;fashkjasdk;asjosa", ex.Message);
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}