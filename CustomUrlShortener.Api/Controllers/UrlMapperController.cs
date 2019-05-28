using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CustomUrlShortener.Helper;
using CustomUrlShortener.Model.DbModel;
using CustomUrlShortener.Repository;

namespace CustomUrlShortener.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlMapperController : ControllerBase
    {

        private DataContext _db;
        public UrlMapperController(DataContext db)
        {
            _db = db;

        }
        
        [HttpPost("GetCode")]
        public string GetCode([FromBody]string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                string code = RandomHelper.GetURL();
                var oldUrlMap = _db.UrlMaps.Where(p => p.Url == url || p.Code == code).ToList();
                if (oldUrlMap != null && oldUrlMap.Count > 0)
                {
                    if (oldUrlMap.Any(p => p.Url == url))
                    {
                        code = oldUrlMap.Where(p => p.Url == url).Select(p => p.Code).FirstOrDefault();
                        return code;
                    }
                    else
                    {
                        code = GetUniqueCode(RandomHelper.GetURL());
                    }
                }
                UrlMap urlMap = new UrlMap
                {
                    Code = code,
                    CreatedOn = DateTime.Now,
                    Id = Guid.NewGuid(),
                    Url = url,
                    VisitorCount = 0
                };
                _db.UrlMaps.Add(urlMap);
                _db.SaveChanges();
                return code;
            }
            else
            {
                return "";
            }
        }
        [HttpGet("GetUrl/{code}")]
        public string GetUrlFromCode(string code)
        {
            var urlMap = _db.UrlMaps.Where(p => p.Code == code).SingleOrDefault();
            if(urlMap != null)
            {
                urlMap.VisitorCount++;
                _db.SaveChanges();
                return urlMap.Url;
            }
            else
            {
                return "";
            }
        }
        public string GetUniqueCode(string code)
        {
            if(_db.UrlMaps.Any(p=> p.Code == code))
            {
                return GetUniqueCode(RandomHelper.GetURL());
            }
            else
            {
                return code;
            }
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<UrlMap>> Get()
        {
            //UrlMap urlMap = new UrlMap();
            //urlMap.Id = Guid.NewGuid();
            //urlMap.CreatedOn = DateTime.Now;
            //urlMap.Url = "http:Sdasdasdasdasdad";
            //urlMap.Code = "gdfsd";
            //db.UrlMaps.Add(urlMap);
            //db.SaveChanges();
            return _db.UrlMaps.ToList();
            //return new string[] { "value1", "value2" };
        }
    }
}