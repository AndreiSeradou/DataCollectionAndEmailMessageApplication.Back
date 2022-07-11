using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OmegaSoftware.TestProject.BL.App.DTOs.Request;
using OmegaSoftware.TestProject.BL.App.Interfaces.Services;
using OmegaSoftware.TestProject.Configuration;

namespace OmegaSoftware.TestProject.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ApplicationConfiguration.AdminRole)]
    public class ApiController : ControllerBase
    {
        private readonly IApiService _apiService;

        public ApiController(IApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
        [Route("all")]
        [AllowAnonymous]
        public IActionResult GetAllApis()
        {
            try
            {
                var apis = _apiService.GetAll().Select(x => x.Name).ToList();

                if (apis == null)
                    return NotFound();

                return Ok(apis);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("create")]
        public IActionResult CreateApi([FromBody] ApiRequest model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = _apiService.Create(model);

                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return BadRequest(ApplicationConfiguration.InvalidModel);
        }

        [HttpPut]
        [Route("update")]
        public IActionResult UpdateApi([FromBody] ApiRequest model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = _apiService.Update(model);

                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return BadRequest(ApplicationConfiguration.InvalidModel);
        }

        [HttpDelete]
        [Route("delete")]
        public IActionResult DeleteApi([FromHeader] ApiRequest model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = _apiService.Delete(model);

                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return BadRequest(ApplicationConfiguration.InvalidModel);
        }
    }
}
