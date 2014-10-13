using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApplication.Repository
{
    interface IDisplayBidRepository :IDisposable
    {
        IEnumerable<DisplayBid> GetUsers();
        DisplayBid GetUserByID(int userId);
        void InsertUser(DisplayBid user);
        void UpdateUser(DisplayBid user);
        void Save();
    }
}
