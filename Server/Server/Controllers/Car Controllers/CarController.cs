using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Server.Data;
using Server.Models;
using Server.Services;
using System.Web;
using static Server.Services.ExceptionHandler;

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

