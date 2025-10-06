using System.Collections.Generic;

namespace ComputerShop.Services
{
    internal interface IDatabase
    {
        DataView GetAllData();
        bool GetData(string username, string password);
        object AddRecord(string username, string password, string email, string fullname);
    }
}