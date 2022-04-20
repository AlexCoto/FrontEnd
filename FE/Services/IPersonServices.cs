using FE.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FE.Services
{
    public interface IPersonServices
    {
        void Insert(Person t);
        void Update(Person t);
        void Delete(Person t);
        IEnumerable<Person> GetAll();
        Person GetOneById(int id);
        Task<IEnumerable<Person>> GetAllAsync();
        Task<Person> GetOneByIdAsync(int id);
    }
}
