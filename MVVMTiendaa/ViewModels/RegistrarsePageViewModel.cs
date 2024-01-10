using CommunityToolkit.Mvvm.ComponentModel;
using MVVMTiendaa.Models;
using MVVMTiendaa.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMTiendaa.ViewModels
{
    public partial class RegistrarsePageViewModel: ObservableObject
    {
        private APIService _ApiService;
        [ObservableProperty]
        public string nombreUsuario;
        [ObservableProperty]
        public string correo;
        [ObservableProperty]
        public string telefono;
        [ObservableProperty]
        public string direccion;
        [ObservableProperty]
        public string contrasenia;
        [ObservableProperty]
        public string confirmarContrasenia;
        public RegistrarsePageViewModel()
        {
            
        }
        public void SetAPIService(APIService apiService)
        {
            _ApiService = apiService;
        }


        public async Task<int> OnClickRegistrarNuevoUsuario()
        {
            
            if(string.IsNullOrWhiteSpace(NombreUsuario)|| string.IsNullOrWhiteSpace(Correo) ||
                string.IsNullOrWhiteSpace(Telefono) || string.IsNullOrWhiteSpace(Direccion) ||
                string.IsNullOrWhiteSpace(Contrasenia) || string.IsNullOrWhiteSpace(ConfirmarContrasenia))
            {
                return -1;
            }
            else
            {
                if (Contrasenia == ConfirmarContrasenia)
                {
                    Usuario nuevoUsuario = new Usuario
                    {
                        usuario = NombreUsuario,
                        contrasena = Contrasenia,
                        correo = Correo,
                        telefono = Telefono,
                        direccion = Direccion

                    };
                    // Las contraseñas coinciden
                    await _ApiService.PostUsuario(nuevoUsuario);
                    return 1;

                }
                else
                {
                    return 0;
                }
                

            }

            
        }
    }
}
