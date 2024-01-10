using MVVMTiendaa.Models;
using MVVMTiendaa.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MVVMTiendaa;

public partial class UsuarioModificado : ContentPage
{
    private Usuario _usuario;
    private readonly APIService _ApiService;

    public UsuarioModificado(APIService apiservice)
    {

        InitializeComponent();
        _ApiService = apiservice;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _usuario = BindingContext as Usuario;
        if (_usuario != null)
        {
            usuario.Text = _usuario.usuario;
            correo.Text = _usuario.correo;
            telefono.Text = _usuario.telefono;
            direccion.Text = _usuario.direccion;
            contrasena.Text = _usuario.contrasena;
        }

    }

    private async void OnClickGuardarCambios(object sender, EventArgs e)
    {
        Usuario usuarionuevo = new Usuario
        {
            usuario = usuario.Text,
            correo = correo.Text,
            telefono= telefono.Text,
            direccion= direccion.Text,
            contrasena = contrasena.Text,
        };
        var respuesta = await _ApiService.UpdateUsuario(_usuario.idUsuario,usuarionuevo);
        if(respuesta!=null)
        {
            await DisplayAlert("Éxito", "Se han guardado los cambios correctamente", "OK");
            await Navigation.PopAsync();
        }

        

    }
}