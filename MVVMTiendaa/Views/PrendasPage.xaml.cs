using MVVMTiendaa.Models;
using MVVMTiendaa.Services;
using MVVMTiendaa.ViewModels;
using System.Collections.ObjectModel;

namespace MVVMTiendaa;

public partial class PrendasPage : ContentPage
{
    private readonly APIService _ApiService;
    private readonly PrendasPageViewModel _viewModel;

    public PrendasPage(APIService apiservice)
    {

        InitializeComponent();
        _ApiService = apiservice;
        _viewModel = new PrendasPageViewModel();
        _viewModel.SetAPIService(apiservice);
        BindingContext = _viewModel;

        int usuarioid = Preferences.Get("idUsuario", 0);
        if (usuarioid == 0)
        {
            Preferences.Set("idUsuario", 0);
            Preferences.Set("idCarrito", 0);

        }
    }
    private void CargarPrendas()
    {
        _viewModel.CargarPrendasAsync();
    }


    private async void OnClickBuscar(object sender, EventArgs e)
    {
        Prenda resultado = await _viewModel.OnClickBuscar();
    
        if (resultado != null)
        {
            await Navigation.PushAsync(new DetallePrendaPage(_ApiService, resultado.idPrenda));
        }
        else
        {
            await DisplayAlert("UPS!", "Ingresa un código válido.", "OK");
        }
       
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        CargarPrendas();
    }

    private async void OnClickMostrarDetalles(object sender, SelectedItemChangedEventArgs e)
    {

        Prenda prenda = e.SelectedItem as Prenda;
        await Navigation.PushAsync(new DetallePrendaPage(_ApiService, prenda.idPrenda));
        
    }


    
}