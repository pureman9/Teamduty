using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MyApplication.Repository
{
    public class ProjectRepository : IProjectRepository
    {
     private MVCEntities context;
     public ProjectRepository(MVCEntities context)
        {
            this.context = context;
        }


     public IEnumerable<Project> GetUsers()
     {
         return context.Projects.ToList();
     }

     public Project GetUserByID(int userId)
     {
         return context.Projects.Find(userId);
     }

     public void InsertUser(Project user)
     {
         context.Projects.Add(user);
     }

     public void UpdateUser(Project user)
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