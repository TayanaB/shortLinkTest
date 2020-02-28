using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShortLinks.Models;
using ShortLinks.Services;

namespace ShortLinks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinkController : ControllerBase
    {
        /// <summary>
        /// Метод создания сокращенной ссылки по полной
        /// </summary>
        /// <param name="link">полная ссылка</param>
        /// <param name="sessionId">ид сессии пользователя</param>
        /// <returns></returns>
        [HttpPost("сreateShortLink/{link}")]
        public async Task<string> CreateShortLink([FromServices] IShortLinksService linkService, string link, string sessionId = null)
        {
            return await linkService.CreateShortLinkAsync(link, sessionId);

        }

        /// <summary>
        /// Метод получения оригинала по сокращенной, с увеличением счетчика посещений
        /// </summary>
        /// <param name="shortLink"></param>
        /// <returns></returns>
        [HttpGet("GetLink/{shortLink}")]
        public async Task<string> GetLink([FromServices] IShortLinksService linkService, string shortLink)
        {
            return await linkService.GetLinkAsync(shortLink);
        }

        /// <summary>
        /// Метод получения списка всех сокращенных ссылок с количеством переходов
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        [HttpGet("GetAllShortLink")]
        public async Task<IEnumerable<ShortLink>> GetAllShortLink([FromServices] IShortLinksService linkService, string sessionId = null)
        {
            return await linkService.GetAllShortLinkAsync(sessionId);
        }
    }
}