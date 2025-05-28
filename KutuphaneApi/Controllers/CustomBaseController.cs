using KutuphaneService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace KutuphaneApi.Controllers
{
    [Authorize]
    [EnableRateLimiting("RateLimiter")]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {     
    }
}