using System.ComponentModel.DataAnnotations;

namespace MMM.Library.Domain.CQRS
{
    public enum EBookingStatus
    {
        NotSet = 0, // error

        [Display(GroupName = "Status", Name = "Rascunho", Description = "Reserva Iniciada como rascunho")]
         Draft = 1,

        [Display(GroupName = "Status", Name = "Reservado", Description = "Reservado")]
        Reservado = 2,
        [Display(GroupName = "Status", Name = "Retirado", Description = "Retirado")]
        Retirado = 5,
        [Display(GroupName = "Status", Name = "Entregue", Description = "Entregue")]
        Entregue = 7,

        [Display(GroupName = "Status", Name = "Indisponível", Description = "Não disponível")]
        Indisponivel = 9,

        [Display(GroupName = "Status", Name = "Descartado", Description = "Descartado")]
        Descartado = 10,
    }
}
