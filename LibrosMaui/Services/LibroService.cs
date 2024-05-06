using LibrosMaui.Models.DTOs;
using LibrosMaui.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LibrosMaui.Services
{
    public class LibroService
    {
        HttpClient _client ;
        Repositories.LibrosRepository _repos = new();
        public LibroService()
        {
            _client = new HttpClient() 
            { 
                BaseAddress = new Uri("https://libros.itesrc.net/")
            };

        }
        public async Task Agregar(LibroDTO dto)
        {
            // var json = JsonSerializer.Serialize(dto);
            //var response = await _client.PostAsync("api/libros",new StringContent(json, Encoding.UTF8,"application/json"));
            var response = await _client.PostAsJsonAsync("api/libros", dto);
            if(response.IsSuccessStatusCode)
            {
                await GetLibros();// baja los libros de la api a la bdlocal
            }
            else
            {
                var errores = await response.Content.ReadAsStringAsync();
                throw new Exception(errores);
            }
        }
        public event Action? DatosActualizados;
        public async Task GetLibros()
        {
            try
            {
               var fecha = Preferences.Get("UltimaFechaActualizacion",DateTime.MinValue);
                bool aviso = false;
                var resposne = await _client.GetFromJsonAsync<List<LibroDTO>>($"api/libros/{fecha:yyyy-MM-dd}/{fecha:HH}/{fecha:mm}");
                if(resposne != null)
                {
                    foreach (var item in resposne)
                    {
                        var _entidad = _repos.Get(item.Id??0);
                        if(_entidad == null && item.Eliminado == false)
                        {
                            _entidad = new()
                            {
                                Id = item.Id ?? 0,
                                Autor = item.Autor,
                                Portada = item.Portada,
                                Titulo = item.Titulo,
                            };
                            _repos.Insert(_entidad);
                            aviso = true;
                        }
                        else 
                        {
                            if (_entidad != null)
                            {
                                if (item.Eliminado)
                                {
                                    _repos.Delete(_entidad);
                                    aviso = true;
                                }
                                else
                                {
                                    if (item.Titulo != _entidad.Titulo || item.Autor!=_entidad.Autor|| item.Portada != _entidad.Portada)
                                    {
                                        _repos.Update(_entidad);
                                        aviso = true;
                                    }
                                    
                                }
                                
                            }
                            
                            
                            
                        }
                        
                    }
                    if (aviso)
                    {
                        _ =MainThread.InvokeOnMainThreadAsync(() =>
                        {

                             DatosActualizados?.Invoke();
                        });
                    }
                    Preferences.Set("UltimaFechaActualizacion", resposne.Max(x => x.Fecha));
                }
                
            }
            catch (Exception ex)
            {

                
            }
        }
        public async Task Eliminar(int idLibro)
        {
            var _response = await _client.DeleteAsync("api/libros/"+idLibro);
            if (_response.IsSuccessStatusCode)
            {
                await GetLibros();
            }
        }
        public async Task Editar(LibroDTO _libro)
        {
            var _response = await _client.PutAsJsonAsync("api/libros", _libro);
            if(_response.IsSuccessStatusCode)
            {
                await GetLibros();
            }
        }
    }
}
