/*
 *  LISTE DE NUMEROS COMPOSES
 * */
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace PhoneApp.Resources.layout
{
    [Activity(Label = "@string/CallHistory")]
    public class CallHistoryActivity : ListActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here
            var PhoneNumbers = Intent.Extras.GetStringArrayList("phone_numbers") ?? new string[0];
            this.ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, PhoneNumbers);
        }
    }
}