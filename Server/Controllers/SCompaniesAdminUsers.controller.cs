#region Imports
using Microsoft.AspNetCore.Authorization;
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
    public class SCompaniesAdminUsersController: ControllerBase {

        #region Fields
        private readonly SCompaniesAdminUsersService service;
        #endregion

        #region Constructor
        public SCompaniesAdminUsersController(SCompaniesAdminUsersService service) {
            this.service = service;
        }
        #endregion

        //=====================================================GETS BELOW=====================================================
        #region GetAll
        [HttpGet("GetAll/{companyID}")]
        [RestInPeaceGet(1, "List<ECompanyAdminUser> GetAll(segment string companyID, optional int listCount=-1, optional int pageNumber=0, optional string orderBy='id desc')")]
        public IActionResult GetAll(string companyID, int listCount = -1, int pageNumber = 0, string orderBy = "id desc") {
            var result = service.GetAll(companyID, listCount, pageNumber, orderBy);
            return Ok(result);
        }
        #endregion

        #region GetByID
        [HttpGet("GetByID/{id}")]
        [RestInPeaceGet(1, "ECompanyAdminUser GetByID(segment string id)")]
        public IActionResult GetByID(string id) {
            var bill = service.GetByID(id);
            return Ok(bill);
        }
        #endregion

        //=====================================================GETS ABOVE=====================================================

        #region Authenticate
        [HttpPost("Authenticate")]
        [AllowAnonymous]
        [RestInPeacePost(1, "ECompanyAdminUser Authenticate(body ECompanyAdminUser eCompanyAdminUser)")]
        public ActionResult<ECompanyAdminUser> Authenticate([FromBody] ECompanyAdminUser eCompanyAdminUser) {
            var e = service.Authenticate(eCompanyAdminUser);
            return Ok(e);
        }
        #endregion

        #region Save
        [HttpPut("Save")]
        [RestInPeacePut(1, "string Save(body ECompanyAdminUser eCompanyAdminUser)")]
        [ProducesResponseType(StatusCodes.Status201Created)]     // Created
        [ProducesResponseType(StatusCodes.Status400BadRequest)]  // BadRequest
        public async Task<IActionResult> Save([FromBody] ECompanyAdminUser eCompanyAdminUser) {
            var result = await service.SaveAsync(eCompanyAdminUser);
            return Ok(result);
        }
        #endregion

        #region Remove
        [HttpDelete("Remove/{id}")]
        [RestInPeaceDelete(1, "bool Remove(segment string id)")]
        public async Task<IActionResult> Remove(string id) {
            var result = await service.RemoveAsync(id);
            return Ok(result);
        }
        #endregion
    }
}
