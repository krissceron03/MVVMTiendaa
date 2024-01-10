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
    public partial class UsuarioModificadoViewModel: ObservableObject
    {
        private APIService _ApiService;
        [ObservableProperty]
        public Usuario usuario1;
        [ObservableProperty]
        public string usuario;
        [ObservableProperty]
        public int idUsuario;
        [ObservableProperty]
        public string correo;
        [ObservableProperty]
        public string telefono;
        [ObservableProperty]
        public string direccion;
        [ObservableProperty]
        public string contrasenia;
        public UsuarioModificadoViewModel()
        {
            
        }
        public void SetAPIService(APIService apiService)
        {
            _ApiService = apiService;
        }

        public async Task<int> CargarDatos()
        {
            int idUsuario = Preferences.Get("idUsuario", 0);
            Usuario1 = await _ApiService.GetUsuarioPorCodigo(idUsuario);
            Usuario = Usuario1.usuario;
            Correo = Usuario1.correo;
            Telefono = Usuario1.telefono;
            Direccion = Usuario1.direccion;
            Contrasenia = Usuario1.contrasena;
            return 1;
        }

        public async Task<int> OnClickGuardarCambios()
        {
            if(string.IsNullOrWhiteSpace(Usuario)|| string.IsNullOrWhiteSpace(Correo) ||
                string.IsNullOrWhiteSpace(Telefono) || string.IsNullOrWhiteSpace(Direccion) ||
                string.IsNullOrWhiteSpace(Contrasenia))
            {
                return -1;
            }
            else
            {

            }
            Usuario usuarionuevo = new Usuario
            {
                usuario = Usuario,
                correo = Correo,
                telefono = Telefono,
                direccion = Direccion,
                contrasena = Contrasenia,
            };
            int idUsuario = Preferences.Get("idUsuario", 0);
            await _ApiService.UpdateUsuario(idUsuario, usuarionuevo);
            return 1;
            



        }
    }
}
