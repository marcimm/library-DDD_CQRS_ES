using MMM.IStore.Core.Messages;
using System;

namespace MMM.Library.Domain.Core.Notifications
{
    public class Notification : Event
    {
        public Guid NotificationId { get; private set; }
        public string Key { get; private set; }
        public string Value { get; private set; }
        public int Version { get; private set; }

        public Notification(string key, string value)
        {
            NotificationId = Guid.NewGuid();
            Version = 1;
            Key = key;
            Value = value;
        }
    }
}
