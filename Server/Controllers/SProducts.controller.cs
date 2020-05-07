#region Imports
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLib.Codes;
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
    public class SProductsController: ControllerBase {

        private readonly SProductsService productsService;

        public SProductsController(SProductsService service) {
            this.productsService = service;
        }

        //=====================================================GETS BELOW=====================================================
        #region GetAll
        [HttpGet("GetAll/{companyID}")]
        [RestInPeaceGet(1, "List<EProduct> GetAll(segment string companyID, optional int listCount=-1, optional int pageNumber=0, optional string orderBy='id desc')")]
        public IActionResult GetAll(string companyID, int listCount = -1, int pageNumber = 0, string orderBy = "id desc") {
            var result = productsService.GetAll(companyID, listCount, pageNumber, orderBy);
            return Ok(result);
        }
        #endregion
    }
}
