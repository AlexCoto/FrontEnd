using FE.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FE.Services
{
    public interface IDepartmentServices
    {
        void Insert(Department t);
        void Update(Department t);
        void Delete(Department t);
        IEnumerable<Department> GetAll();
        Department GetOneById(int id);
        Task<IEnumerable<Department>> GetAllAsync();
        Task<Department> GetOneByIdAsync(int id);
    }
}
