using System;
using System.ComponentModel.DataAnnotations;

namespace MMM.Library.Application.ViewModels
{
    public class AuthorViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Nationality { get; set; }


    }
}
