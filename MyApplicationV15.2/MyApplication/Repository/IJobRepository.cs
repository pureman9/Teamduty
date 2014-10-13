using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApplication.Repository
{
    interface IJobRepository : IDisposable
    {


        IEnumerable<Job> GetUsers();
      
        Job GetUserByID(int userId);
        void InsertUser(Job user);
        void UpdateUser(Job user);
      
        void Save();
    }
}
