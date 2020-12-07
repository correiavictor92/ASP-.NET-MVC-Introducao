using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class PessoaModels
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage="CPF é obrigatório.")]
        [MaxLength(11)]
        public string Cpf { get; set; }

        [Required]
        [MaxLength(20)]
        public string Nome { get; set; }

        [Required]
        [MaxLength(20)]
        public string Sobrenome { get; set; }

        [Required]
        public DateTime DataNascimento { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [Phone]
        public string Telefone { get; set; }
    }
}