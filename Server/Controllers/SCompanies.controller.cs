using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers {
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [AllowAnonymous]
    public class SCompaniesController: ControllerBase {
        private readonly SCompaniesService companiesService;

        public SCompaniesController(SCompaniesService service) {
            companiesService = service;
        }
    }
}