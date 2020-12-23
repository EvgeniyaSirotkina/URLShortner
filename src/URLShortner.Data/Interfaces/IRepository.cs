using System.Linq;
using System.Threading.Tasks;
using URLShortner.Data.Models;

namespace URLShortner.Data.Interfaces
{
    public interface IRepository
    {
        Task<Response> Create(Url url);

        Task<Response> Delete(int id);

        Task<Response> Update(Url url);

        Task<Url> GetById(int id);

        Task<IQueryable<Url>> GetAll();
    }
}
