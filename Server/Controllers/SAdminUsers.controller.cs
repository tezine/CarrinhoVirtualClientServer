#region Imports
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Codes;
using SharedLib.Codes;
using SharedLib.Entities;
using System.Threading.Tasks;
#endregion

namespace Server.Controllers {
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [AllowAnonymous]
    //    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SAdminUsersController : ControllerBase {

        #region Fields
        private readonly SAdminUsersService adminUsersService;
        #endregion

        #region Constructor
        public SAdminUsersController(SAdminUsersService service) {
            adminUsersService = service;
        }
        #endregion

        //=====================================================GETS BELOW=====================================================

        #region GetByID
        [HttpGet("GetByID/{id}")]
        [RestInPeaceGet(1, "EAdminUser GetByID(segment string id)")]
        public ActionResult<EAdminUser> GetByID(string id) {
            var eAdminUser = adminUsersService.GetByID(id);
            return Ok(eAdminUser);
        }
        #endregion

        //=====================================================GETS ABOVE=====================================================

        #region Authenticate
        [HttpPost("Authenticate")]
        [AllowAnonymous]
        [RestInPeacePost(1, "EAdminUser Authenticate(body EAdminUser eAdminUser)")]
        public ActionResult<EAdminUser> Authenticate([FromBody] EAdminUser eAdminUser) {
            var e = adminUsersService.Authenticate(eAdminUser);
            return Ok(e);
        }
        #endregion

        #region Save
        [HttpPut("Save")]
        [RestInPeacePut(1, "string Save(body EAdminUser eAdminUser)")]
        [ProducesResponseType(StatusCodes.Status201Created)]     // Created
        [ProducesResponseType(StatusCodes.Status400BadRequest)]  // BadRequest
        public async Task<ActionResult<string>> Save([FromBody] EAdminUser eAdminUser) {
            var result = await adminUsersService.SaveAsync(eAdminUser);
            return Ok(result);
        }
        #endregion

        #region Remove
        [HttpDelete("Remove/{id}")]
        [RestInPeaceDelete(1, "bool Remove(segment string id)")]
        public async Task<ActionResult<bool>> Remove(string id) {
            var result = await adminUsersService.RemoveAsync(id);
            return Ok(result);
        }
        #endregion




    }
}
