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
        /// <returns>Empty string or error message.</returns>
        Task<string> Create(UrlDto url);

        /// <summary>
        /// Delete existing URL by Id.
        /// </summary>
        /// <param name="id">Url Id.</param>
        /// <returns>Empty string or error message.</returns>
        Task<string> Delete(int id);

        /// <summary>
        /// Update existing URL.
        /// </summary>
        /// <param name="url">Updated Url.</param>
        /// <returns>Empty string or error message.</returns>
        Task<string> Update(UrlDto url);

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
