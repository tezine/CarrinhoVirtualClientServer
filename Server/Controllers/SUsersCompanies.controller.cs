using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLib.Codes;
using SharedLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Controllers {
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [AllowAnonymous]
    //    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SUsersCompaniesController: ControllerBase {

        #region Fields
        private readonly SUsersCompaniesService service;
        #endregion

        #region Constructor
        public SUsersCompaniesController(SUsersCompaniesService service) {
            this.service = service;
        }
        #endregion

        //=====================================================GETS BELOW=====================================================
        #region GetAllByUserID
        [HttpGet("GetAllByUserID/{userID}")]
        [RestInPeaceGet(1, "List<EUserCompany> GetAllByUserID(segment string userID)")]
        public ActionResult<List<EUserCompany>> GetAllByUserID(string userID) {
            return Ok(service.GetAllByUserID(userID));
        }
        #endregion

        #region AddCompany
        [HttpPost("AddCompany")]
        [RestInPeacePost(1, "EUserCompany AddCompany(body EUserCompany eUserCompany)")]
        public async Task<ActionResult<EUserCompany>> AddCompany([FromBody] EUserCompany eUserCompany) {
            var e = await service.AddCompanyAsync(eUserCompany);
            return Ok(e);
        }
        #endregion
    }
}
