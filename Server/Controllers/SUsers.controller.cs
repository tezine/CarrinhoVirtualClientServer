#region Imports
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedLib.Codes;
using SharedLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
#endregion

namespace Server.Controllers {
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [AllowAnonymous]
    //    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SUsersController : ControllerBase {

        #region Fields
        private readonly SUsersService usersService; 
        #endregion

        #region Constructor
        public SUsersController(SUsersService service) {
            usersService = service;
        } 
        #endregion

        //=====================================================GETS BELOW=====================================================
        #region GetAll
        [HttpGet("GetAll/{companyID}")]
        [RestInPeaceGet(1, "List<EUser> GetAll(segment string companyID, optional int listCount=-1, optional int pageNumber=0, optional string orderBy='id desc')")]
        public IActionResult GetAll(string companyID, int listCount = -1, int pageNumber = 0, string orderBy = "id desc") {
            return Ok(usersService.GetAll(companyID, listCount, pageNumber, orderBy));
        }
        #endregion

        #region GetByID
        [DisableCors]
        [HttpGet("GetByID/{id}")]
        [RestInPeaceGet(1, "EUser GetByID(segment string id)")]
        public IActionResult GetByID(string id) {
            var bill = usersService.GetByID(id);
            return Ok(bill);
        }
        #endregion

        //=====================================================GETS ABOVE=====================================================

        #region Authenticate
        [HttpPost("Authenticate")]
        [AllowAnonymous]
        [RestInPeacePost(1, "EUser Authenticate(body EUser eUser)")]
        public ActionResult<EUser> Authenticate([FromBody] EUser eUser) {
            var e = usersService.Authenticate(eUser);
            return Ok(e);
        }
        #endregion

        #region Save
        [HttpPut("Save")]
        [RestInPeacePut(1, "string Save(body EUser eClient)")]
        [ProducesResponseType(StatusCodes.Status201Created)]     // Created
        [ProducesResponseType(StatusCodes.Status400BadRequest)]  // BadRequest
        public async Task<IActionResult> Save([FromBody] EUser eClient) {
            var result = await usersService.SaveAsync(eClient);
            return Ok(result);
        }
        #endregion

        #region Remove
        [HttpDelete("Remove/{id}")]
        [RestInPeaceDelete(1, "bool Remove(segment string id)")]
        public async Task<IActionResult> Remove(string id) {
            var result = await usersService.RemoveAsync(id);
            return Ok(result);
        }
        #endregion
    }
}
