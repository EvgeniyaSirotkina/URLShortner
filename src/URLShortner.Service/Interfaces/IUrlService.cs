using System.Collections.Generic;
using System.Threading.Tasks;
using URLShortner.Service.Models;

namespace URLShortner.Service.Interfaces
{
    /// <summary>
    /// Definition interface IUrlService.
    /// </summary>
    public interface IUrlService
    {
        /// <summary>
        /// Create new Url item.
        /// </summary>
        /// <param name="newUrl">New Url.</param>
        /// <returns>True or False.</returns>
        Task<bool> Create(string newUrl);

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
