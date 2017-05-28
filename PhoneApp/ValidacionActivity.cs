using Android.App;
using Android.OS;
using Android.Widget;
using SALLab06;

namespace PhoneApp
{
    [Activity(Label = "@string/TextBtnValidate", Icon = "@drawable/icon")]
    public class ValidacionActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ValidacionActivity);

            var ValidarButton = FindViewById<TextView>(Resource.Id.buttonValidar);

            ValidarButton.Click += (object sender, System.EventArgs e) =>
            {
                Validate();
            };
        }

        private async void Validate()
        {
            ServiceClient Seviceclient = new ServiceClient();

            var CorreoEditText = FindViewById<EditText>(Resource.Id.CorreoEditText);
            var ContrasenyaEditText = FindViewById<EditText>(Resource.Id.ContrasenyaEditText);

            string StudentEmail = CorreoEditText.Text;  //"@hotmail.com";
            string Password = ContrasenyaEditText.Text; //"********";
            string MyDevice = Android.Provider.Settings.Secure.GetString(ContentResolver, Android.Provider.Settings.Secure.AndroidId);

            var Result = await Seviceclient.ValidateAsync(StudentEmail, Password, MyDevice);

            var Validation = FindViewById<TextView>(Resource.Id.TextValidation);
            Validation.Text = $"{Result.Status}\n{Result.Fullname}\n{Result.Token}\n";
        }

        /// <summary>
        /// | Test Validacion XamarinDiplomado3.0 |
        /// </summary>
        private void MookValidate()
        {
            var CorreoEditText = FindViewById<EditText>(Resource.Id.CorreoEditText);
            var ContrasenyaEditText = FindViewById<EditText>(Resource.Id.ContrasenyaEditText);
            string StudentEmail = CorreoEditText.Text;  //"@hotmail.com";
            string Password = ContrasenyaEditText.Text; //"********";
            var Validation = FindViewById<TextView>(Resource.Id.TextValidation);
            Validation.Text = $"Mook-Success\nTony Simoes\n{StudentEmail}\n{Password}\nMS-9-9-2-00-000-0-1\nXamarinDiplomado3.0-Lab06A";
        }
    }
}