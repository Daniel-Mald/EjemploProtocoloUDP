using LibrosMaui.Models.DTOs;
using LibrosMaui.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
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
        public async Task GetLibros()
        {
            try
            {
               var fecha = Preferences.Get("UltimaFechaActualizacion",DateTime.MinValue);
                var resposne = await _client.GetFromJsonAsync<List<LibroDTO>>($"api/libros/{fecha:yyyy-MM-dd}T{fecha:HH:mm:ss}");
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
                        }
                        else 
                        {
                            if(_entidad != null)
                            {
                                if (_entidad.Eliminado)
                                {
                                    _repos.Delete(_entidad);

                                }
                                else
                                {
                                    _repos.Update(_entidad);
                                }
                                
                            }
                            
                            
                            
                        }
                        
                    }
                    Preferences.Set("UltimaFechaActualizacion", resposne.Max(x => x.Fecha));
                }
                
            }
            catch (Exception ex)
            {

                
            }
        }

    }
}
