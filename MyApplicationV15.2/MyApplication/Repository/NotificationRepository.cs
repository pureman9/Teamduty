using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MyApplication.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        
        private MVCEntities context;
        public NotificationRepository(MVCEntities context)
        {
            this.context = context;
        }


        public IEnumerable<Notification> GetNotifications()
        {
            return context.Notifications.ToList();
        }

        public Notification GetNotificationByID(int userId)
        {
            return context.Notifications.Find(userId);
        }

        public void InsertNotification(Notification user)
        {
            context.Notifications.Add(user);
        }

        public void UpdateNotification(Notification not)
        {
            context.Entry(not).State = EntityState.Modified;
        }
        
        public void Save()
        {
            context.SaveChanges();
        }
         private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    
    }
}