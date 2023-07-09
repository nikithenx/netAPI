using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class MovieHallsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public MovieHallsController(ApplicationDbContext db)
        {
            _db = db;
        }

        
    }
}