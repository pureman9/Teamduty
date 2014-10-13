using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApplication.Repository
{
    public interface INotificationRepository :IDisposable
    {
        IEnumerable<Notification> GetNotifications();
        Notification GetNotificationByID(int userId);
        void InsertNotification(Notification user);
        void UpdateNotification(Notification user);
        void Save();
    }
}
