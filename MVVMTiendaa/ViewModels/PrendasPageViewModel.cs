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

namespace MVVMTiendaa.ViewModels
{
    public partial class PrendasPageViewModel : ObservableObject
    {
        private APIService _ApiService;
        [ObservableProperty]
        public int buscarPorID;
        [ObservableProperty]
        public ObservableCollection<Prenda> listaPrenda;


        public PrendasPageViewModel()
        {
            
        }
        public void SetAPIService(APIService apiService)
        {
            _ApiService = apiService;
        }

        private async Task<Prenda> OnClickBuscar()
        {
            if (string.IsNullOrWhiteSpace(BuscarPorID) || !int.TryParse(BuscarPorID, out int id))
            {
              
                return null;
            }

            Prenda p = await _ApiService.GetPrenda(Int32.Parse(BuscarPorID));
            
            {
                BindingContext = p,
            });
        }

    }
}
