using FlightApi.Models;
namespace FlightApi.Repository
{

    public interface IUserRepo<Kguser>
    {
        List<Kguser> GetAllUsers();
        void AddUser(Kguser e);
        void UpdateUser(int id, Kguser e);
        Kguser GetUserById(int id);
        void DeleteUser(int id);
    }
}