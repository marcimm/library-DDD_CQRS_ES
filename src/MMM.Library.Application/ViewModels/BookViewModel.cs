using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MMM.Library.Application.ViewModels
{
    public class BookViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public string ISBN { get; set; }
        [Required(ErrorMessage = "Campo Título do Livro é obrigatório")]
        public string Title { get; set; }
        public int Year { get; set; }
        [Required(ErrorMessage = "Ano do Livro é obrigatório", AllowEmptyStrings = false)]
        public string Language { get; set; }
        public CategoryViewModel Category { get; set; }
        public PublisherViewModel Publisher { get; set; }
        public IEnumerable<AuthorViewModel> Authors { get; set; }
        public AuditViewModel Audit { get; set; }
    }
}
