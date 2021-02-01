using System;
using System.Linq;
using Flunt.Notifications;

namespace Usuarios.BL.Entities
{
    public abstract class Entity : Notifiable
    {

        public string NotificationsAsText()
        {
            if (this.Valid)
                return null;

            return string.Join("", this.Notifications.ToList()
                                                       .Select(n => $"{n.Property} : {n.Message}\n")
                                                       .ToArray()
            );
        }
    }
}
