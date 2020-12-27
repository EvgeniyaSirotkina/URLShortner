using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using URLShortner.Data.Interfaces;
using URLShortner.Data.Models;
using URLShortner.Service.Helpers;
using URLShortner.Service.Interfaces;
using URLShortner.Service.Models;

namespace URLShortner.Service.Services
{
    /// <summary>
    /// Definition UrlService type.
    /// </summary>
    public class UrlService : IUrlService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="repository">IRepository type.</param>
        public UrlService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Create new Url item.
        /// </summary>
        /// <param name="newUrl">New Url.</param>
        /// <returns>True or False.</returns>
        public async Task<bool> Create(string newUrl)
        {
            // check that url is not null or empty + that resource exist
            if (string.IsNullOrEmpty(newUrl) || !newUrl.CheckUrlExists())
            {
                return false;
            }

            var url = new Url
            {
                LongUrl = newUrl,
                ShortUrl = UrlHelper.GenerateShortUrl(),
                Hits = 0,
                GeneratedDate = DateTime.Now,
            };

            return await _repository.Create(url);
        }

        /// <summary>
        /// Delete existing URL by Id.
        /// </summary>
        /// <param name="id">Url Id.</param>
        /// <returns>True or False.</returns>
        public async Task<bool> Delete(int id)
                => await _repository.Delete(id);

        /// <summary>
        /// Get all Urls.
        /// </summary>
        /// <returns>List of UrlDto models.</returns>
        public IEnumerable<UrlDto> GetAll()
        {
            var urls = _repository.GetAll();

            return _mapper.Map<IQueryable<Url>, IEnumerable<UrlDto>>(urls);
        }

        /// <summary>
        /// Get Url by Id.
        /// </summary>
        /// <param name="id">Url Id.</param>
        /// <returns>UrlDto model.</returns>
        public async Task<UrlDto> GetById(int id)
        {
            var url = await _repository.GetById(id);
            if (url != null)
            {
                return _mapper.Map<Url, UrlDto>(url);
            }

            return null;
        }

        /// <summary>
        /// Update existing URL.
        /// </summary>
        /// <param name="url">Updated Url.</param>
        /// <returns>True or False.</returns>
        public async Task<bool> Update(UrlDto url)
        {
            var updatedUrl = _mapper.Map<UrlDto, Url>(url);

            return await _repository.Update(updatedUrl);
        }
    }
}
