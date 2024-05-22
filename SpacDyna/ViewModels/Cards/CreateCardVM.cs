using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SpacDyna.ViewModels.Cards
{
    public class CreateCardVM
    {
        [Required,MaxLength(32,ErrorMessage ="max length 32 symbol"),MinLength(3, ErrorMessage = "min length 3 symbol")]
        public string Name { get; set; }
        [Required, MaxLength(64,ErrorMessage = "max length 64 symbol"), MinLength(3, ErrorMessage = "min length 3 symbol")]
        public string Description { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}
