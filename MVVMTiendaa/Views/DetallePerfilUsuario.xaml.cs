using CommunityToolkit.Maui.Core;
using MVVMTiendaa.Models;
using MVVMTiendaa.Services;
using MVVMTiendaa.ViewModels;

namespace MVVMTiendaa;

public partial class DetallePerfilUsuario : ContentPage
{
    private Usuario _usuario;
    private readonly APIService _ApiService;
    private readonly DetallePerfilUsuarioViewModel _viewModel;

    public DetallePerfilUsuario(APIService apiservice)
    {

        InitializeComponent();
        _ApiService = apiservice;
        _viewModel = new DetallePerfilUsuarioViewModel();
        _viewModel.SetAPIService(apiservice);
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.CargarDatosPerfil();
    }

    private async void OnClickEditar(object sender, EventArgs e)
    {
        var toast = CommunityToolkit.Maui.Alerts.Toast.Make(_usuario.usuario, ToastDuration.Short, 14);

        await toast.Show();
        await Navigation.PushAsync(new UsuarioModificado(_ApiService,_usuario));
        
    }

    private async void OnClickSalir(object sender, EventArgs e)
    {

        await Navigation.PopAsync();

    }
}