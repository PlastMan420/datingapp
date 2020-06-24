using datingapp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace datingapp.Data
{
    public interface IDatingRepository
    {
        // one generic method to deal with anything
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll(); // zero changes to save or more than 0 changes to save.
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(int id);
        Task<Photo> GetPhoto(int id);
        Task<Photo> GetMainPhoto(int userid);
    }
}
