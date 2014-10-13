using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


namespace MyApplication.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private MVCEntities context;
        public AccountRepository(MVCEntities context)
        {
            this.context = context;
        }
        public IEnumerable<Member> GetUsers()
        {
            return context.Members.ToList();
        }

        public Member GetUserByID(int userId)
        {
            return context.Members.Find(userId);
        }

        public void InsertUser(Member user)
        {
             context.Members.Add(user);
        }

        public void UpdateUser(Member user)
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