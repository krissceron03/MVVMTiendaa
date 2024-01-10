using MVVMTiendaa.Models;
using MVVMTiendaa.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MVVMTiendaa;

public partial class DetallePrendaPage : ContentPage
{
    private Prenda _prenda;
    private readonly APIService _ApiService;
    public List<string> num { get; set; }
    private int cantidadMaximaProductos;

    public DetallePrendaPage(APIService apiservice)
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
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _prenda = BindingContext as Prenda;
        Nombre.Text = _prenda.nombre;
        Cantidad.Text = _prenda.cantidad.ToString();
        cantidadMaximaProductos = _prenda.cantidad;
        Descripcion.Text = _prenda.descripcion;
        Marca.Text = _prenda.marca.nombre;
        Categoria.Text = _prenda.categoria.nombre;
        MarcaDescripcion.Text = _prenda.marca.descripcion;
        CategoriaDescripcion.Text = _prenda.categoria.descripcion;
        CargarStock();
    }

    private async void OnClickSalir(object sender, EventArgs e)
    {

        await Navigation.PopAsync();

    }




    private async void OnClickAnadir(object sender, EventArgs e)
    {
       
        int usuarioid = Preferences.Get("idUsuario", 0);
        if (usuarioid == 0)
        {
            await Navigation.PushAsync(new LoginPage(_ApiService));
        }
        else 
        {
            //toma el indice del picker y suma uno para saber la cantidad que mando el usuario
            var cantidadSeleccionada = cantidad.SelectedIndex;
            var cantidad1 = cantidadSeleccionada + 1;

            int carritoid = Preferences.Get("idCarrito", 0);



            DetalleCarritoUsuario detalleCarritoUsuario = new DetalleCarritoUsuario
            {
                cantidad = cantidad1,
                prendaIdPrenda = _prenda.idPrenda,
                carritoIdCarrito = carritoid,
            };

            DetalleCarrito detallecarrito = await _ApiService.PostDetalleCarrito(detalleCarritoUsuario);
            await DisplayAlert("Exito!", "Agregaste el producto a tu carrito con éxito", "OK");
            await Navigation.PopAsync();

            //var toast = CommunityToolkit.Maui.Alerts.Toast.Make("Usuario o contraseña incorrecta", ToastDuration.Short, 14);

            //await toast.Show();
        }
    }



    private void CargarStock()
    {
        // cargamos la lista de numeros
        num = new List<string>();
        for (int i = 1; i <= cantidadMaximaProductos; i++)
        {
            num.Add(i.ToString());
        }

        // asignamos al item source de la vista
        cantidad.ItemsSource = num;
    }


}