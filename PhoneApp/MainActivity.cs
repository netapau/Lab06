using Android.App;
using Android.Widget;
using Android.OS;
using PhoneApp.Resources.layout;

namespace PhoneApp
{
    [Activity(Label = "@string/ApplicationName", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        static readonly System.Collections.Generic.List<string> PhoneNumbers = new System.Collections.Generic.List<string>();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView (Resource.Layout.Main);

            var PhoneNumberText = FindViewById<EditText>(Resource.Id.PhoneNumberText);
            var TranslateButton = FindViewById<Button>(Resource.Id.TranslateButton);
            var CallButton = FindViewById<Button>(Resource.Id.CallButton);
            var CallHistoryButton = FindViewById<Button>(Resource.Id.CallHistoryButton);
            var ValidacionButton = FindViewById<Button>(Resource.Id.ValidacionButton);
            var TranslatedNumber = string.Empty;

            CallButton.Enabled = false;

            TranslateButton.Click += (object sender, System.EventArgs e) => 
            {
                var Translator = new PhoneTranslator();
                TranslatedNumber = Translator.ToNumber(PhoneNumberText.Text);
                if (string.IsNullOrWhiteSpace(TranslatedNumber))
                {
                    // No hay numéro a llamar
                    CallButton.Text = "Llamar";
                    CallButton.Enabled = false;
                }
                else
                {
                    // Hay un posible numero telefonico a llamar
                    CallButton.Text = $"Llamar al {TranslatedNumber}";
                    CallButton.Enabled = true;
                }
            };

            CallButton.Click += (object sender, System.EventArgs e) =>
            {
                // Intentar marcar el numero telefonico
                var CallDialog = new AlertDialog.Builder(this);
                CallDialog.SetMessage($"Llamar al numero {TranslatedNumber} ?");
                CallDialog.SetNeutralButton("Llamar", delegate 
                {
                    // Agregar el numero marcado a la lista de numeros marcados.
                    PhoneNumbers.Add(TranslatedNumber);
                    // Habilitar el boton CallHistoryButton
                    CallHistoryButton.Enabled = true;                       
                    //Crear un intento para marcar el numero telefonico
                    var CallIntent = new Android.Content.Intent(Android.Content.Intent.ActionCall);
                    CallIntent.SetData(Android.Net.Uri.Parse($"tel:{TranslatedNumber}"));
                    StartActivity(CallIntent);
                });
                CallDialog.SetNegativeButton("Cancelar", delegate { });
                CallDialog.Show(); //Mostrar el cuadro de dialogo al utilizador y esperar una respuesta
            };

            CallHistoryButton.Click += (object sender, System.EventArgs e) =>
            {
                var Intent = new Android.Content.Intent(this, typeof(CallHistoryActivity));
                Intent.PutStringArrayListExtra("phone_numbers", PhoneNumbers);
                StartActivity(Intent);
            };

            ValidacionButton.Click += (object sender, System.EventArgs e) =>
            {
                var Intent = new Android.Content.Intent(this, typeof(ValidacionActivity));
                StartActivity(Intent);
            };
        }
    }
}