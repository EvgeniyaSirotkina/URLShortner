using System.Security.Policy;
using AutoMapper;
using URLShortner.Service.Models;

namespace URLShortner.Web.Infrastructure
{
    /// <summary>
    /// UrlProfile class that inherit from Profile class and put the configuration in the constructor.
    /// </summary>
    public class UrlProfile : Profile
    {
        /// <summary>
        /// Default constructor of UrlProfile class.
        /// </summary>
        public UrlProfile()
        {
            CreateMap<Url, UrlDto>()
                .ReverseMap();
        }
    }
}
