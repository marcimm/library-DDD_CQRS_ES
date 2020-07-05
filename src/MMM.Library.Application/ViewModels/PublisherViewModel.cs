using System;
using System.ComponentModel.DataAnnotations;

namespace MMM.Library.Application.ViewModels
{
    public class PublisherViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
