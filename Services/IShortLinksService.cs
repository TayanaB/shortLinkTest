using ShortLinks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShortLinks.Services
{
    public interface IShortLinksService
    {
        /// <summary>
        /// создание сокращенной ссылки по полной
        /// </summary>
        /// <param name="link"></param>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        Task<string> CreateShortLink(string link, string sessionId);

        /// <summary>
        /// получение оригинала по сокращенной, с увеличением счетчика посещений
        /// </summary>
        /// <param name="shortLink"></param>
        /// <returns></returns>
        Task<string> GetLink(string shortLink);

        /// <summary>
        /// получение списка всех сокращенных ссылок с количеством переходов
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        Task<IEnumerable<ShortLink>> GetAllShortLink(string sessionId);
    }
}
