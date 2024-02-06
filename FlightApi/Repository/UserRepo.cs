using System.Data.Common;
using FlightApi.Models;
using FlightApi.Repository;
 
namespace FlightApi.Repository{
    public class CustRepo : IUserRepo<Kguser>
    {
        public readonly Ace52024Context db;
        public CustRepo(){}
 
        public CustRepo(Ace52024Context _db){
            db = _db;
        }

        public void AddUser(Kguser e)
        {
            db.Kgusers.Add(e);
            db.SaveChanges();
        }


        public void DeleteUser(int id)
        {
            Kguser a = db.Kgusers.Find(id);
            db.Kgusers.Remove(a);
            db.SaveChanges();
        }

        public List<Kguser> GetAllUsers()
        {
            return db.Kgusers.ToList();
        }


        public Kguser GetUserById(int id)
        {
            return db.Kgusers.Find(id);
        }

        public void UpdateUser(int id, Kguser e)
        {
            db.Kgusers.Update(e);
            db.SaveChanges();
        }
    }
}