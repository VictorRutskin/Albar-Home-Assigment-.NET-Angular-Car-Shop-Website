using Microsoft.AspNetCore.Mvc;
using Server.Data;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public partial class CarController : Controller
    {
        private readonly MyDbContext mydbcontext;

        public CarController(MyDbContext mydbcontext)
        {
            this.mydbcontext = mydbcontext;

        }
    }
}

