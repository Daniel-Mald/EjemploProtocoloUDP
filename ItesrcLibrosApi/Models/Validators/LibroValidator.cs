using FluentValidation;
using ItesrcLibrosApi.Models.DTOs;

namespace ItesrcLibrosApi.Models.Validators
{
    public class LibroValidator:AbstractValidator<LibroDTO>
    {
        public LibroValidator()
        {
            RuleFor(x => x.Titulo).NotEmpty().WithMessage("El titulo no debe de estar vacio");
            RuleFor(x => x.Autor).NotEmpty().WithMessage("El autor del libro no debe de estar vacio");
            RuleFor(x => x.Portada).NotEmpty().WithMessage("La imagen de la portada no debe estar vacia");
            RuleFor(x => x.Portada).Must(ValidarURL).WithMessage("Escribe una direccion URL de una imagen JPEG");
        }
        private bool ValidarURL(string url)
        {
            return url.StartsWith("https://") && url.EndsWith(".jpg");
        }
    }
}
