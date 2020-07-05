using MMM.IStore.Core.Messages;
using System;

namespace MMM.Library.Domain.CQRS.Commands
{
    public class BookingCommandStart : Command
    {
        public Guid UserId { get; private set; }

        public BookingCommandStart(Guid userId)
        {
            UserId = userId;
        }
    }
}
