using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibrosMaui.Models.DTOs;
using LibrosMaui.Models.Entities;
using LibrosMaui.Models.Validators;
using LibrosMaui.Repositories;
using LibrosMaui.Services;
using LibrosMaui.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrosMaui.ViewModels
{
    public partial class LibrosViewModel:ObservableObject
    {
        LibrosRepository _repos = new();
        LibroService _service = new();
        public ObservableCollection<Libro> Libros { get; set; } = new();

        public LibrosViewModel()
        {
            
            ActualizarLibros();
            _service.GetLibros();
        }

        [ObservableProperty]
        public LibroDTO? libro;
        [ObservableProperty]
        private string error = "";
        [RelayCommand]
        public void VerNuevo()
        {
            Libro = new();
            Shell.Current.GoToAsync("//Agregar");
        }
        [RelayCommand]
        public void Cancelar()
        {
            Libro = null;
            Error = "";
            ActualizarLibros();
            Shell.Current.GoToAsync("//Lista");

        }
        LibroValidator _validator = new();
        [RelayCommand]
        public async Task Agregar()
        {
            try
            {
                if (Libro != null)
                {
                    var resultado = _validator.Validate(Libro);
                    if (resultado.IsValid)
                    {
                        Libro.Eliminado = false;
                        await _service.Agregar(Libro);
                        Cancelar();
                    }
                    else
                    {
                        Error = string.Join("\n", resultado.Errors.Select(x => x.ErrorMessage));
                    }
                }
            }
            catch(Exception ex)
            {
                Error = ex.Message;
            }
            
            
        }

        void ActualizarLibros()
        {
            Libros.Clear();
            var y = _repos.GetAll();
            foreach (var l in y)
            {
                Libros.Add(l);
            }
        }
    }
}
