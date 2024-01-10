using MVVMTiendaa.Models;
using MVVMTiendaa.Services;
using MVVMTiendaa.ViewModels;

namespace MVVMTiendaa;

public partial class RegistrarsePage : ContentPage
{
   // Label errorLabel;
    private readonly APIService _ApiService;
    private readonly RegistrarsePageViewModel _viewModel;

    public RegistrarsePage(APIService apiservice)
    {
        //errorLabel = new Label();
        
        InitializeComponent();
        _ApiService = apiservice;
        _viewModel = new RegistrarsePageViewModel();
        _viewModel.SetAPIService(apiservice);
        BindingContext = _viewModel;
    }

    private async void OnClickRegistrarNuevoUsuario(object sender, EventArgs e)
    {
        int resultado= await _viewModel.OnClickRegistrarNuevoUsuario();
        if(resultado==-1)
        {
            await DisplayAlert("Campos incompletos", "Completa todos los campos.", "OK");
        }
        else if(resultado==1)
        {
            await DisplayAlert("Éxito", "El usuario ha sido creado exitosamente", "OK");
            await Navigation.PopAsync();
           // errorLabel.IsVisible = false;
        }
        else
        {
            await DisplayAlert("OH NO", "Las constraseñas no coinciden", "OK");
        }
        
    }

    private async void OnClickCancelar(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
        
    }
}