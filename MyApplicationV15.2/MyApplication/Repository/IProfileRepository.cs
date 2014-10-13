using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApplication.Repository
{
    interface IProfileRepository : IDisposable
    {
        IEnumerable<Profile> GetUsers();
        Profile GetUserByID(int profileId);
        void InsertUser(Profile profile);
        void UpdateUser(Profile profile);
        void Save();

    }
}
