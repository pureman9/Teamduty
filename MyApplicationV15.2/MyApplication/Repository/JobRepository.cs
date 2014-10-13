using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MyApplication.Repository
{
    public class JobRepository : IJobRepository
    {
         private MVCEntities context;
         public JobRepository(MVCEntities context)
        {
            this.context = context;
        }


        public IEnumerable<Job> GetUsers()
        {
            return context.Jobs.ToList();
        }
      
        public Job GetUserByID(int userId)
        {
            return context.Jobs.Find(userId);
        }

        public void InsertUser(Job user)
        {
            context.Jobs.Add(user);
        }

        public void UpdateUser(Job user)
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