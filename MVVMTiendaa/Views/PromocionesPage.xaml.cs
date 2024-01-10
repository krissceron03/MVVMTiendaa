using MVVMTiendaa.Models;
using MVVMTiendaa.Services;
using System.Collections.ObjectModel;

namespace MVVMTiendaa;

public partial class PromocionesPage : ContentPage
{
    private readonly APIService _ApiService;

    public PromocionesPage(APIService apiservice)
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

        Promocion p = await _ApiService.GetPromocion(Int32.Parse(BuscarPorID.Text));
        await Navigation.PushAsync(new DetallePromocionPage(_ApiService)
        {
            BindingContext = p,
        });

    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        List<Promocion> listaPromociones = await _ApiService.GetAllPromociones();
        var promos = new ObservableCollection<Promocion>(listaPromociones);
        promociones.ItemsSource = promos;
    }

    private async void OnClickMostrarDetalles(object sender, SelectedItemChangedEventArgs e)
    {

        Promocion promocion = e.SelectedItem as Promocion;
        await Navigation.PushAsync(new DetallePromocionPage(_ApiService)
        {
            BindingContext = promocion,
        });
    }
}