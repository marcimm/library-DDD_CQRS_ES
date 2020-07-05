using FluentValidation;
using MMM.IStore.Core.Messages;
using System;

namespace MMM.Library.Domain.CQRS.Commands
{
    public class BookingItemCommandAdd : Command
    {
        public Guid UserId { get; private set; }
        public Guid StockId { get; private set; }            
        public string BookName { get; private set; }
        public DateTime DateStart { get; private set; }
        public DateTime DateEnd { get; private set; }
        public string Notes { get; private set; }
        public DateTime RegisterDay { get; private set; }

        public BookingItemCommandAdd(Guid userId, Guid stockId, string bookName, DateTime dateStart, 
            DateTime dateEnd, string notes, DateTime registerDay)
        {
            UserId = userId;
            StockId = stockId;
            BookName = bookName;
            DateStart = dateStart;
            DateEnd = dateEnd;
            Notes = notes;
            RegisterDay = registerDay;

        }

        public override bool IsValid()
        {
            ValidationResult = new BookingItemCommandAddValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class BookingItemCommandAddValidation : AbstractValidator<BookingItemCommandAdd>
    {
        public BookingItemCommandAddValidation()
        {
            RuleFor(p => p.UserId).NotEqual(Guid.Empty)
                .WithMessage("Id do usuário inválido");
            
            RuleFor(p => p.StockId).NotEqual(Guid.Empty)
                .WithMessage("Id do Estoque inválido");

            RuleFor(c => c.BookName).NotEmpty()
                .WithMessage("O nome do libro não foi informado");

            RuleFor(c => c.DateStart).NotEmpty().GreaterThanOrEqualTo(DateTime.Now)
                .WithMessage("O dia de início da reserva não pode ser antes de hoje!");

            RuleFor(c => c.DateStart).NotEmpty().GreaterThan(c => c.DateStart)
                .WithMessage("O dia final da reserva tem que após o dia inicial!");

        }
    }
}
