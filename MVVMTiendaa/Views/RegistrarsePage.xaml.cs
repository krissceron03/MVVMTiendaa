using MVVMTiendaa.Models;
using MVVMTiendaa.Services;

namespace MVVMTiendaa;

public partial class RegistrarsePage : ContentPage
{
    Label errorLabel;
    private readonly APIService _ApiService;
    
    public RegistrarsePage(APIService apiservice)
    {
        errorLabel = new Label();
        _ApiService = apiservice;
        InitializeComponent();
	}

    private async void OnClickRegistrarNuevoUsuario(object sender, EventArgs e)
    {
        string contrasenia1 = contrasenia.Text;
        string confirmarContrasenia1 = confirmarContrasenia.Text;


        if (contrasenia1 == confirmarContrasenia1)
        {
            Usuario nuevoUsuario = new Usuario
            {
                usuario = nombreUsuario.Text,
                contrasena = contrasenia.Text,
                correo = correo.Text,
                telefono = telefono.Text,
                direccion= direccion.Text

            };
            // Las contraseñas coinciden
            await _ApiService.PostUsuario(nuevoUsuario);
            await DisplayAlert("Éxito", "El usuario ha sido creado exitosamente", "OK");
            await Navigation.PopAsync();

            errorLabel.IsVisible = false; // Ocultar el mensaje de error


        }
        else
        {
            // Las contraseñas no coinciden, Se muestra un mensaje de error o tomar otras medidas
            errorLabel.IsVisible = true; // Mostrar el mensaje de error
        }
    }

    private async void OnClickCancelar(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
        
    }
}