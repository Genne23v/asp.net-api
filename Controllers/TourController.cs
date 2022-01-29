using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ExploreCalifornia.DataAccess;
using ExploreCalifornia.DataAccess.Models;
using ExploreCalifornia.DTOs;

namespace ExploreCalifornia.Controllers
{
    [RoutePrefix("api/tour")]
    public class TourController : ApiController
    {
        private AppDataContext _context = new AppDataContext();

        /// <summary>
        /// Gets a list of all tours
        /// </summary>
        /// <param name="freeOnly">Show free tours only?</param>
        /// <returns>List of all matching tours</returns>
        [HttpGet]
        public List<TourDto> GetAllTours([FromUri] bool freeOnly = false)
        {
            var query = _context.Tours.Select(i => new TourDto
            {
                Description = i.Description,
                Name = i.Name,
                Price = i.Price,
                TourId = i.TourId
            }).AsQueryable();

            if (freeOnly)
            {
                query = query.Where(i => i.Price == 0.0m);
            }
            return query.ToList();
        }

        [Route("{id:int}")]
        public Tour GetById(int id)
        {
            var item = _context.Tours.AsQueryable().Where(i => i.TourId == id).FirstOrDefault();
            return item;
        }

        [Route("{name}")]
        public Tour GetByName(string name)
        {
            var item = _context.Tours.Where(i => i.Name.Contains(name)).FirstOrDefault();
            return item;
        }

        [HttpPost]
        public List<Tour> SearchTour([FromBody] TourSearchRequestDto request)
        {
            if (request.MinPrice > request.MaxPrice)
            {
                /*return BadRequest("Minimum price must be less than max price");*/ //FOR IHttpActionResult
                throw new HttpResponseException(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent("Minimum price must be less than max price")
                });
            }
            var query = _context.Tours.AsQueryable().Where(i => i.Price >= request.MinPrice && i.Price <= request.MaxPrice);
            return query.ToList();
        }
        
        [HttpPut]
        public IHttpActionResult Put(int id, Tour tour)
        {
            return Ok($"{id}: {tour.Name}");
        }

        [HttpPatch]
        public IHttpActionResult Patch()
        {
            return Ok("Patch");
        }

        [HttpDelete]
        public IHttpActionResult Delete()
        {
            return Ok("Delete");
        }
    }
}