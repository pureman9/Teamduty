using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MyApplication.Repository
{
    public class DisplayBidRepository : IDisplayBidRepository
    {

        private MVCEntities context;
        public DisplayBidRepository(MVCEntities context)
        {
            this.context = context;
        }
        
        public IEnumerable<DisplayBid> GetUsers()
        {
            return context.DisplayBids.ToList();
        }
       

        public DisplayBid GetUserByID(int userId)
        {
            return context.DisplayBids.Find(userId);
        }

        public void InsertUser(DisplayBid user)
        {
            context.DisplayBids.Add(user);
        }

        public void UpdateUser(DisplayBid user)
        {
            context.Entry(user).State = EntityState.Modified;
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