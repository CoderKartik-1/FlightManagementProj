using System;

namespace FlightApi.Service
{

    public interface IUserSer<Kguser>
    {
        List<Kguser> GetAllUsers();
        void AddUser(Kguser e);
        void UpdateUser(int id, Kguser e);
        Kguser GetUserById(int id);
        void DeleteUser(int id);
    }
}