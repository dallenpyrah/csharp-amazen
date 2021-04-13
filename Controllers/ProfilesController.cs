using System.Collections.Generic;
using amazen.Models;
using amazen.Services;
using Microsoft.AspNetCore.Mvc;

namespace amazen.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
  public class ProfilesController : ControllerBase
  {

    
    private readonly ProfilesService _productservice;

        public ProfilesController(ProfilesService productservice)
        {
            _productservice = productservice;
        }

        [HttpGet("{id}")]
    public ActionResult<Profile> Get(string id)
    {
      try
      {
        return Ok(_productservice.GetProfileById(id));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }


    [HttpGet("{id}/products")]
    public ActionResult<IEnumerable<Product>> GetProducts(string id)
    {
      try
      {
        return Ok(_productservice.GetProductsByProfileId(id));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }
  }
}