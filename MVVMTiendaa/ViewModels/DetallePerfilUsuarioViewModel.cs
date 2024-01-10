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
    public partial class DetallePerfilUsuarioViewModel: ObservableObject
    {
        private APIService _ApiService;
        [ObservableProperty]
        public Usuario usuario1;
        [ObservableProperty]
        public string usuario;
        [ObservableProperty]
        public string correo;
        [ObservableProperty]
        public string telefono;
        [ObservableProperty]
        public string direccion;
        public DetallePerfilUsuarioViewModel()
        {
            
        }
        public void SetAPIService(APIService apiService)
        {
            _ApiService = apiService;
        }

        public async Task CargarDatosPerfil()
        {
            int idUsuario = Preferences.Get("idUsuario", 0);
            Usuario1 = await _ApiService.GetUsuarioPorCodigo(idUsuario);
            
            Usuario = Usuario1.usuario;
            Correo= Usuario1.correo;
            Telefono = Usuario1.telefono;
            Direccion = Usuario1.direccion;
        }

    }
}
