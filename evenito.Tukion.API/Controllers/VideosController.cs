using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using evenito.Tukion.Server.Data;
using evenito.Tukion.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace evenito.Tukion.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideosController : ControllerBase, IDisposable
    {
        public VideosController(IDataAdaptor<VideoModel> dataAdaptor)
        {
            _dataAdaptor = dataAdaptor;
        }

        private IDataAdaptor<VideoModel> _dataAdaptor;

        // GET api/values
        [HttpGet]
        public IEnumerable<VideoModel> Get()
        {
            return _dataAdaptor.LoadAll();
        }

        // GET api/values/<guid>
        [HttpGet("{id}")]
        public VideoModel Get(Guid id)
        {
            return _dataAdaptor.Load(id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        public void Dispose()
        {
            _dataAdaptor?.Dispose();
        }
    }
}
