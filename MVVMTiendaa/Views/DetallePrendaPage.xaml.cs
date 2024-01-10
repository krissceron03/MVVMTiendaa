using MVVMTiendaa.Models;
using MVVMTiendaa.Services;
using MVVMTiendaa.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MVVMTiendaa;

public partial class DetallePrendaPage : ContentPage
{
 
    private readonly APIService _ApiService;
    private readonly int _idPrenda;
    private readonly DetallePrendaPageViewModel _viewModel;


    public DetallePrendaPage(APIService apiservice, int idPrenda)
    {
        InitializeComponent();
        _ApiService = apiservice;
        _viewModel = new DetallePrendaPageViewModel();
        _viewModel.SetAPIService(apiservice);
        _idPrenda = idPrenda;
        BindingContext = _viewModel;

        int usuarioid = Preferences.Get("idUsuario", 0);
        if (usuarioid == 0)
        {
            Preferences.Set("idUsuario", 0);
            Preferences.Set("idCarrito", 0);

        }

    }



    

    protected async override void OnAppearing()
    {
      base.OnAppearing();
        _viewModel.CargarPrendasAsync(_idPrenda);
        List<string> num = await _viewModel.CargarStock(_idPrenda);
        cantidad.ItemsSource= num;
        
    }

    private async void OnClickSalir(object sender, EventArgs e)
    {

        await Navigation.PopAsync();

    }




    private async void OnClickAnadir(object sender, EventArgs e)
    {
        int resultado = await _viewModel.OnClickAnadir();
        if(resultado == -1)
        {
            await Navigation.PushAsync(new LoginPage(_ApiService));
        }
        else
        {
            if(resultado == 0)
            {
                await DisplayAlert("NO!", "AGREGA ALGO AL CARRITO", "OK");
            }
            else
            {
                await DisplayAlert("Exito!", "Agregaste el producto a tu carrito con éxito", "OK");
                await Navigation.PopAsync();
            }
        }
       
            

            //var toast = CommunityToolkit.Maui.Alerts.Toast.Make("Usuario o contraseña incorrecta", ToastDuration.Short, 14);

            //await toast.Show();
        }
    }
