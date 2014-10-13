using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyApplication.Repository
{
    interface IAccountRepository :IDisposable
    {

        IEnumerable<Member> GetUsers();
        Member GetUserByID(int userId);
        void InsertUser(Member user);
        void UpdateUser(Member user);
        void Save();
    }
}
