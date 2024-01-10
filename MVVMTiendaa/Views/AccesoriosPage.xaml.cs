using MVVMTiendaa.Models;
using MVVMTiendaa.Services;
using System.Collections.ObjectModel;

namespace MVVMTiendaa;

public partial class AccesoriosPage : ContentPage
{
    private readonly APIService _ApiService;

    public AccesoriosPage(APIService apiservice)
    {

        InitializeComponent();
        _ApiService = apiservice;
    }
    private async void OnClickBuscar(object sender, EventArgs e)
    {

        if (string.IsNullOrWhiteSpace(BuscarPorID.Text) || !int.TryParse(BuscarPorID.Text, out int id))
        {
            // Entry está vacío, puedes mostrar un mensaje o simplemente salir del método
            await DisplayAlert("UPS!", "Ingresa un código válido.", "OK");
            return;
        }
        Accesorio p = await _ApiService.GetAccesorio(Int32.Parse(BuscarPorID.Text));
        await Navigation.PushAsync(new DetalleAccesorioPage(_ApiService)
        {
            BindingContext = p,
        });
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        List<Accesorio> listaAccesorios = await _ApiService.GetAllAccesorios();
        var accesorios1 = new ObservableCollection<Accesorio>(listaAccesorios);
        accesorios.ItemsSource = accesorios1;
    }

    private async void OnClickMostrarDetalles(object sender, SelectedItemChangedEventArgs e)
    {

        Accesorio accesorio = e.SelectedItem as Accesorio;
        await Navigation.PushAsync(new DetalleAccesorioPage(_ApiService)
        {
            BindingContext = accesorio,
        });
    }
}