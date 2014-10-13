using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MyApplication.Repository
{
    public class BidRepository : IBidRepository
    {

           private MVCEntities context;
           public BidRepository(MVCEntities context)
        {
            this.context = context;
        }
        public IEnumerable<Bid> GetUsers()
        {
            return context.Bids.ToList();
        }

        public Bid GetUserByID(int userId)
        {
            return context.Bids.Find(userId);
        }

        public void InsertUser(Bid user)
        {
            context.Bids.Add(user);
        }

        public void UpdateUser(Bid user)
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