using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MyApplication.Repository
{
    public class ProfileRepository : IProfileRepository
    {
        private MVCEntities context;

        public ProfileRepository(MVCEntities context)
        {
            this.context = context;
        }
        public IEnumerable<Profile> GetUsers()
        {
            return context.Profiles.ToList();
        }

        public Profile GetUserByID(int profileId)
        {
            return context.Profiles.Find(profileId);
        }

        public void InsertUser(Profile profile)
        {
            context.Profiles.Add(profile);
        }

        public void UpdateUser(Profile profile)
        {
            context.Entry(profile).State = EntityState.Modified;
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