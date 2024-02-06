using FlightApi.Models;
using FlightApi.Repository;
using FlightApi.Service;
using System.Collections.Generic;
 
namespace FlightApi.Service
{
    public class CustServ : IUserSer<Kguser>
    {
        private readonly IUserRepo<Kguser> _kgRepo;
 
        public CustServ(){}
        public CustServ(IUserRepo<Kguser> kgRepo)
        {
            _kgRepo = kgRepo;
        }
 
        public List<Kguser> GetAllUsers()
        {
            return _kgRepo.GetAllUsers();
        }

        public void AddUser(Kguser e)
        {
           _kgRepo.AddUser(e);
        }

        public void UpdateUser(int id, Kguser e)
        {
            Console.WriteLine($"In Service interface: {e.Uid} {e.Ulocation}");
            _kgRepo.UpdateUser(id, e);
        }

        public Kguser GetUserById(int id)
        {
            return _kgRepo.GetUserById(id);
        }

        public void DeleteUser(int id)
        {
            _kgRepo.DeleteUser(id);
        }
    }
}