using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApplication.Repository
{
    interface IBidRepository : IDisposable
    {
        IEnumerable<Bid> GetUsers();
        Bid GetUserByID(int userId);
        void InsertUser(Bid user);
        void UpdateUser(Bid user);
        void Save();
    }
}
