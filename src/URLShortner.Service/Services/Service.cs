using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using URLShortner.Data.Interfaces;
using URLShortner.Service.Interfaces;
using URLShortner.Service.Models;

namespace URLShortner.Service.Services
{
    /// <summary>
    /// Definition Service type.
    /// </summary>
    public class Service : IService
    {
        private readonly IRepository _repository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="repository">IRepository type.</param>
        public Service(IRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Create new Url item.
        /// </summary>
        /// <param name="url">UrlDto model.</param>
        /// <returns>Empty string or error message.</returns>
        public Task<string> Create(UrlDto url)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Delete existing URL by Id.
        /// </summary>
        /// <param name="id">Url Id.</param>
        /// <returns>Empty string or error message.</returns>
        public Task<string> Delete(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get all Urls.
        /// </summary>
        /// <returns>List of UrlDto models.</returns>
        public IEnumerable<UrlDto> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get Url by Id.
        /// </summary>
        /// <param name="id">Url Id.</param>
        /// <returns>UrlDto model.</returns>
        public Task<UrlDto> GetById(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update existing URL.
        /// </summary>
        /// <param name="url">Updated Url.</param>
        /// <returns>Empty string or error message.</returns>
        public Task<string> Update(UrlDto url)
        {
            throw new NotImplementedException();
        }
    }
}
