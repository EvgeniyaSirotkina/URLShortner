using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using URLShortner.Data.EF;
using URLShortner.Data.Helpers;
using URLShortner.Data.Interfaces;
using URLShortner.Data.Models;

namespace URLShortner.Data.Repositories
{
    public class Repository : IRepository
    {
        private readonly UrlContext _db;

        public Repository(UrlContext db)
        {
            _db = db;
        }

        public async Task<Response> Create(Url url)
        {
            if (url == null)
            {
                return new Response
                {
                    IsSuccessful = false,
                    Message = "Argument is null. Can't create new record.",
                };
            }

            try
            {
                _db.Entry(url).State = EntityState.Added;
                await _db.SaveChangesAsync();

                return new Response
                {
                    IsSuccessful = true,
                    Message = string.Empty,
                };
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new Response
                {
                    IsSuccessful = false,
                    Message = ex.DbUpdateConcurrencyExceptionMessageBuilder(),
                };
            }
            catch (DbUpdateException ex)
            {
                return new Response
                {
                    IsSuccessful = false,
                    Message = ex.DbUpdateExceptionMessageBuilder(),
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response> Delete(int id)
        {
            if (id <= 0)
            {
                return new Response
                {
                    IsSuccessful = false,
                    Message = "Id must be a positive number.",
                };
            }

            try
            {
                var url = await _db.Urls.FirstOrDefaultAsync(u => u.UrlId == id);

                if (url == null)
                {
                    return new Response
                    {
                        IsSuccessful = false,
                        Message = $"Can't find Url object with {id} id.",
                    };
                }

                _db.Entry(url).State = EntityState.Deleted;
                await _db.SaveChangesAsync();

                return new Response
                {
                    IsSuccessful = true,
                    Message = string.Empty,
                };
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new Response
                {
                    IsSuccessful = false,
                    Message = ex.DbUpdateConcurrencyExceptionMessageBuilder(),
                };
            }
            catch (DbUpdateException ex)
            {
                return new Response
                {
                    IsSuccessful = false,
                    Message = ex.DbUpdateExceptionMessageBuilder(),
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                };
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

        public async Task<Response> Update(Url url)
        {
            if (url == null)
            {
                return new Response
                {
                    IsSuccessful = false,
                    Message = "Argument is null. Can't create new record.",
                };
            }

            try
            {
                var oldUrl = await _db.Urls.FirstOrDefaultAsync(u => u.UrlId == url.UrlId);

                if (oldUrl == null)
                {
                    return new Response
                    {
                        IsSuccessful = false,
                        Message = $"Can't find Url object with {url.UrlId} id.",
                    };
                }

                _db.Entry(url).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return new Response
                {
                    IsSuccessful = true,
                    Message = string.Empty,
                };
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new Response
                {
                    IsSuccessful = false,
                    Message = ex.DbUpdateConcurrencyExceptionMessageBuilder(),
                };
            }
            catch (DbUpdateException ex)
            {
                return new Response
                {
                    IsSuccessful = false,
                    Message = ex.DbUpdateExceptionMessageBuilder(),
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                };
            }
        }
    }
}
