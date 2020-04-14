using System.ComponentModel.DataAnnotations;
using ProductCatalog.Models;

namespace Shop.Models
{
    public class Product
    {
        [Key]
        public long Id { get; set; }
        
        [Required(ErrorMessage = "Campo Obrigatorio")]
        [MaxLength(60, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        public string Title { get; set; }

        [MaxLength(1024, ErrorMessage = "Este campo deve conte no maximo 1024 caracteres")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "O preço deve ser maior que zero")]

        public decimal Price { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Categoria invalida")]

        public int CategoryId { get; set;}
        public Category Category { get; set;}
    }
}