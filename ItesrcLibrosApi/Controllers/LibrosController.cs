using ItesrcLibrosApi.Models.DTOs;
using ItesrcLibrosApi.Models.Entities;
using ItesrcLibrosApi.Models.Validators;
using ItesrcLibrosApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ItesrcLibrosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        LibrosRepository _repository { get; }
        public LibrosController(LibrosRepository _repos)
        {
            _repository = _repos;
        }

        [HttpPost]
        public IActionResult Post(LibroDTO _dto)
        {
            LibroValidator _validator = new();
            var _resultado = _validator.Validate(_dto);
            if (_resultado.IsValid)
            {
                Libros entidad = new()
                {
                    Id = 0,
                    Eliminado = false,
                    FechaActualizacion = _dto.Fecha,
                    Autor = _dto.Autor,
                    Portada = _dto.Portada,
                    Titulo = _dto.Titulo,
                    
                };
                _repository.Insert(entidad);
                return Ok("Se ha agregado el libro correctamente");
            }
            else
            {
                return BadRequest(_resultado.Errors.Select(x => x.ErrorMessage));
            }
        }
        [HttpGet("{fecha?}")]
        public IActionResult Get(DateTime? fecha)
        {
            var libros = _repository.GetAll().
                Where(x => fecha == null || x.FechaActualizacion > fecha).
                OrderBy(x => x.Titulo).
                Select(x => new LibroDTO
                {
                    Id = x.Id,
                    Titulo = x.Titulo,
                    Autor = x.Autor,
                    Eliminado = x.Eliminado,
                    Portada = x.Portada
                }) ;


            return Ok(libros);
        }
        [HttpPut]
        public IActionResult Put(LibroDTO dto)
        {
            LibroValidator _validator = new();
            var _resultado = _validator.Validate(dto);
            if (_resultado.IsValid)
            {
                var entidadLibro = _repository.GetById(dto.Id ?? 0);
                if(entidadLibro == null || entidadLibro.Eliminado == true)
                {
                    return NotFound();
                }
                else
                {
                    entidadLibro.Autor = dto.Autor;
                    entidadLibro.Titulo = dto.Titulo;
                    entidadLibro.Portada = dto.Portada;
                    entidadLibro.FechaActualizacion = DateTime.UtcNow;

                    _repository.Update(entidadLibro);
                    return Ok();
                }
                
                
            }
            else
            {
                return BadRequest(_resultado.Errors.Select(x => x.ErrorMessage));
            }
            
        }
        //[HttpPut("{Id}")]
        //public IActionResult Put(int Id, LibroDTO dto)
        //{
        //    return Ok();
        //}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var _entidad = _repository.GetById(id);
            if(_entidad == null || _entidad.Eliminado )
            {
                return NotFound();
            }
            _entidad.Eliminado = true;
            _entidad.FechaActualizacion = DateTime.UtcNow;
            _repository.Update(_entidad);
            return Ok();
        }
    }
}
