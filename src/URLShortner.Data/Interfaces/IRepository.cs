using System.Linq;
using System.Threading.Tasks;
using URLShortner.Data.Models;

namespace URLShortner.Data.Interfaces
{
    public interface IRepository
    {
        Task<bool> Create(Url url);

        Task<bool> Delete(int id);

        Task<bool> Update(Url url);

        Task<Url> GetById(int id);

        IQueryable<Url> GetAll();
    }
}
