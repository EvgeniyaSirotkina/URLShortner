using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using URLShortner.Data.EF;
using URLShortner.Data.Helpers;
using URLShortner.Data.Interfaces;
using URLShortner.Data.Models;

namespace URLShortner.Data.Repositories
{
    public class Repository : IRepository
    {
        private readonly UrlContext _db;
        private readonly ILogger<Repository> _logger;

        public Repository(UrlContext db, ILogger<Repository> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<bool> Create(Url url)
        {
            if (url == null)
            {
                _logger.LogError("Argument is null. Can't create new record.");
                return false;
            }

            try
            {
                _db.Entry(url).State = EntityState.Added;
                await _db.SaveChangesAsync();

                _logger.LogInformation($"New Url was created with {url.UrlId} id.");
                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex.DbUpdateConcurrencyExceptionMessageBuilder());
                return false;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.DbUpdateExceptionMessageBuilder());
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            if (id <= 0)
            {
                _logger.LogError("Id must be a positive number.");
                return false;
            }

            try
            {
                var url = await _db.Urls.FirstOrDefaultAsync(u => u.UrlId == id);

                if (url == null)
                {
                    _logger.LogError($"Can't find Url object with {id} id.");
                    return false;
                }

                _db.Entry(url).State = EntityState.Deleted;
                await _db.SaveChangesAsync();

                _logger.LogInformation($"The Url with {id} id was successfully deleted.");
                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex.DbUpdateConcurrencyExceptionMessageBuilder());
                return false;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.DbUpdateExceptionMessageBuilder());
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public IQueryable<Url> GetAll()
        {
            return _db.Urls;
        }

        public async Task<Url> GetById(int id)
        {
            if (id > 0)
            {
                return await _db.Urls.FirstOrDefaultAsync(url => url.UrlId == id);
            }

            return null;
        }

        public async Task<bool> Update(Url url)
        {
            if (url == null)
            {
                _logger.LogError("Argument is null. Can't create new record.");
                return false;
            }

            try
            {
                var oldUrl = await _db.Urls.FirstOrDefaultAsync(u => u.UrlId == url.UrlId);

                if (oldUrl == null)
                {
                    _logger.LogError($"Can't find Url object with {url.UrlId} id.");
                    return false;
                }

                _db.Entry(url).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                _logger.LogError($"The Url with {url.UrlId} id was successfully updated.");
                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex.DbUpdateConcurrencyExceptionMessageBuilder());
                return false;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.DbUpdateExceptionMessageBuilder());
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
    }
}
