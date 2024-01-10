using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using MVVMTiendaa.Models;
using MVVMTiendaa.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMTiendaa.ViewModels
{
    public partial class DetallePrendaPageViewModel: ObservableObject  
    {
        private APIService _ApiService;
        [ObservableProperty]
        public Prenda prenda;
        [ObservableProperty]
        public string nombre;
        [ObservableProperty]
        public string cantidad;
        [ObservableProperty]
        public int cantidadPicker;
        [ObservableProperty]
        public string descripcion;
        [ObservableProperty]
        public string categoria;
        [ObservableProperty]
        public string categoriaDescripcion;
        [ObservableProperty]
        public string marca;
        [ObservableProperty]
        public string marcaDescripcion;
        [ObservableProperty]
        public List<string> num;
        [ObservableProperty]
        public int cantidadMaximaProductos;


        public DetallePrendaPageViewModel()
        {
            
        }

        public void SetAPIService(APIService apiService)
        {
            _ApiService = apiService;
        }

        [RelayCommand]
        public async Task<int> OnClickAnadir()
        {

            int usuarioid = Preferences.Get("idUsuario", 0);
            if (usuarioid == 0)
            {
                return -1;
            }
            else
            {
                //toma el indice del picker y suma uno para saber la cantidad que mando el usuario
                var cantidadSeleccionada = CantidadPicker;
                if (cantidadSeleccionada == 0)
                {
                    return 0;
                }
                else
                {
                    cantidadSeleccionada = CantidadPicker + 1;

                    int carritoid = Preferences.Get("idCarrito", 0);
                    DetalleCarritoUsuario detalleCarritoUsuario = new DetalleCarritoUsuario
                    {
                        cantidad = cantidadSeleccionada,
                        prendaIdPrenda = Prenda.idPrenda,
                        carritoIdCarrito = carritoid,
                    };
                    DetalleCarrito detallecarrito = await _ApiService.PostDetalleCarrito(detalleCarritoUsuario);
                    return 1;
                }
              
            }
        }
        public async Task CargarPrendasAsync(int idPrenda)
        {
            Prenda = await _ApiService.GetPrenda(idPrenda);
            //actualización

            Nombre = Prenda.nombre;
            Cantidad = Prenda.cantidad.ToString();
            CantidadMaximaProductos = Prenda.cantidad;
            Descripcion = Prenda.descripcion;
            Marca = Prenda.marca.nombre;
            Categoria = Prenda.categoria.nombre;
            MarcaDescripcion = Prenda.marca.descripcion;
            CategoriaDescripcion = Prenda.categoria.descripcion;
            
        }

        public async Task<List<string>> CargarStock(int idPrenda)
        {
            Prenda = await _ApiService.GetPrenda(idPrenda);
            CantidadMaximaProductos = Prenda.cantidad;
            // cargamos la lista de numeros
            List<string> num = new List<string>();

                for (int i = 1; i <= CantidadMaximaProductos; i++)
                {
                    num.Add(i.ToString());
                }

                // asignamos al item source de la vista
                return num;
            
        }
    }
}
