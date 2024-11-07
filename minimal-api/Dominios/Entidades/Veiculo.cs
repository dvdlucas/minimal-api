using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace minimal_api.Dominios.Entidades
{
    public class Veiculo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string? Nome { get; set; }

        [Required]
        [StringLength(50)]
        public string? Marca { get; set; }

        [Required]
        public int? Ano { get; set; }

    }
}