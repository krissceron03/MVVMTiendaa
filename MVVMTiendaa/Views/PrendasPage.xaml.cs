using MVVMTiendaa.Models;
using MVVMTiendaa.Services;
using System.Collections.ObjectModel;

namespace MVVMTiendaa;

public partial class PrendasPage : ContentPage
{
    private readonly APIService _ApiService;

    public PrendasPage(APIService apiservice)
    {

        InitializeComponent();
        _ApiService = apiservice;

        int usuarioid = Preferences.Get("idUsuario", 0);
        if (usuarioid == 0)
        {
            Preferences.Set("idUsuario", 0);
            Preferences.Set("idCarrito", 0);

        }
    }
    private async void OnClickBuscar(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(BuscarPorID.Text) || !int.TryParse(BuscarPorID.Text, out int id))
        {
            // Entry está vacío, puedes mostrar un mensaje o simplemente salir del método
            await DisplayAlert("UPS!", "Ingresa un código válido.", "OK");
            return;
        }

        Prenda p = await _ApiService.GetPrenda(Int32.Parse(BuscarPorID.Text));
        await Navigation.PushAsync(new DetallePrendaPage(_ApiService)
        {
            BindingContext = p,
        });
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        List<Prenda> listaPrenda = await _ApiService.GetAllPrendas();
        var prendas = new ObservableCollection<Prenda>(listaPrenda);
        productos.ItemsSource = prendas;
    }

    private async void OnClickMostrarDetalles(object sender, SelectedItemChangedEventArgs e)
    {

        Prenda prenda = e.SelectedItem as Prenda;
        await Navigation.PushAsync(new DetallePrendaPage(_ApiService)
        {
            BindingContext = prenda,
        });
    }


    
}