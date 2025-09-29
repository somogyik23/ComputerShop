using System.Collections.Generic;

namespace ComputerShop.Services
{
    internal interface IDatabase
    {
        ICollection<object> GetAllData();
        object GetData(string username, string password);
    }
}