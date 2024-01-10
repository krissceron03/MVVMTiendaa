using MVVMTiendaa.Models;
using MVVMTiendaa.Services;
using MVVMTiendaa.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MVVMTiendaa;

public partial class UsuarioModificado : ContentPage
{
    private Usuario _usuario;
    private readonly APIService _ApiService;
    private readonly UsuarioModificadoViewModel _viewModel;

    public UsuarioModificado(APIService apiservice, Usuario usuario)
    {
        
        InitializeComponent();
        _usuario= usuario;
        _ApiService = apiservice;
        _viewModel = new UsuarioModificadoViewModel();
        _viewModel.SetAPIService(apiservice);
       
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        int resultado = await _viewModel.CargarDatos();
       

    }

    private async void OnClickGuardarCambios(object sender, EventArgs e)
    {
        int resultado = await _viewModel.OnClickGuardarCambios();
        if(resultado == 1)
        {
            await DisplayAlert("Éxito", "Se han guardado los cambios correctamente", "OK");
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("OH NO!", "Llene los campos", "OK");
        }
        

        

    }
}