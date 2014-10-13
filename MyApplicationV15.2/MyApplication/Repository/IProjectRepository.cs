using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApplication.Repository
{
    interface IProjectRepository :IDisposable
    {

        IEnumerable<Project> GetUsers();
        Project GetUserByID(int userId);
        void InsertUser(Project user);
        void UpdateUser(Project user);
        void Save();
    }
}
