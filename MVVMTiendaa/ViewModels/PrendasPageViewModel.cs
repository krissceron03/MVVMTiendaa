using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls;
using MVVMTiendaa.Models;
using MVVMTiendaa.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Windows.Services.Maps;

namespace MVVMTiendaa.ViewModels
{
    public partial class PrendasPageViewModel : ObservableObject
    {
        private APIService _ApiService;
        [ObservableProperty]
        public string buscarPorID;
        [ObservableProperty]
        public ObservableCollection<Prenda> listaPrenda;


        public PrendasPageViewModel()
        {
            
        }
        public void SetAPIService(APIService apiService)
        {
            _ApiService = apiService;
        }

        public async Task<Prenda> OnClickBuscar()
        {
            if (string.IsNullOrWhiteSpace(BuscarPorID) || !int.TryParse(BuscarPorID, out int id))
            {
              
                return null;
            }

            Prenda p = await _ApiService.GetPrenda(id);

            if (p != null)
            {

                return p;
            }
            else
            {
                return null;

            }
        }
        public async void CargarPrendasAsync()
        {
            List<Prenda> listaPrenda = await _ApiService.GetAllPrendas();
            ListaPrenda = new ObservableCollection<Prenda>(listaPrenda);
            foreach(var prenda in listaPrenda) { ListaPrenda.Add(prenda); }
        }

    }
}
