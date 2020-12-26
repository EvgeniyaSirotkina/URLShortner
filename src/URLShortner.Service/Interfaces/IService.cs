using System.Collections.Generic;
using System.Threading.Tasks;
using URLShortner.Service.Models;

namespace URLShortner.Service.Interfaces
{
    /// <summary>
    /// Definition interface IService.
    /// </summary>
    public interface IService
    {
        /// <summary>
        /// Create new Url item.
        /// </summary>
        /// <param name="url">UrlDto model.</param>
        /// <returns>True or False.</returns>
        Task<bool> Create(UrlDto url);

        /// <summary>
        /// Delete existing URL by Id.
        /// </summary>
        /// <param name="id">Url Id.</param>
        /// <returns>True or False.</returns>
        Task<bool> Delete(int id);

        /// <summary>
        /// Update existing URL.
        /// </summary>
        /// <param name="url">Updated Url.</param>
        /// <returns>True or False.</returns>
        Task<bool> Update(UrlDto url);

        /// <summary>
        /// Get Url by Id.
        /// </summary>
        /// <param name="id">Url Id.</param>
        /// <returns>UrlDto model.</returns>
        Task<UrlDto> GetById(int id);

        /// <summary>
        /// Get all Urls.
        /// </summary>
        /// <returns>List of UrlDto models.</returns>
        IEnumerable<UrlDto> GetAll();
    }
}
