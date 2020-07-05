using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MMM.Library.Application.ViewModels
{
    public class BookWriteViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public Guid PublisherId { get; set; }

        public string ISBN { get; set; }

        [Required(ErrorMessage = "Campo Título do Livro é obrigatório")]
        public string Title { get; set; }

        public int Year { get; set; }

        [Required(ErrorMessage = "Ano do Livro é obrigatório", AllowEmptyStrings = false)]
        public string Language { get; set; }

       public ICollection<Guid> AuthorsId { get; set; }
    }
}
