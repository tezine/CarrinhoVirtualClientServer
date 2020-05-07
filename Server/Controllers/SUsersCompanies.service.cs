#region Imports
using Server.Codes;
using SharedLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 
#endregion

namespace Server.Controllers {
    public class SUsersCompaniesService {

        #region GetAll
        public List<EUserCompany> GetAllByUserID(string userID) {
            using var context = new SMySQLContext();
            List<EUserCompany> list = context.UserCompanies.Where(x=>x.UserId==userID).ToList();
            foreach(EUserCompany eUserCompany in list) {
                eUserCompany.CompanyName = context.Companies.FirstOrDefault(x => x.Id == eUserCompany.CompanyId).Name;
            }
            list=list.OrderBy(x => x.CompanyName).ToList();
            return list;
        }
        #endregion

        //=====================================================GETS ABOVE=====================================================

        #region AddCompanyAsync
        public async Task<EUserCompany> AddCompanyAsync(EUserCompany e) {
            using var context = new SMySQLContext();

            //var ecompany = context.Companies.Where(x => x.Name == e.CompanyName).FirstOrDefault();
            //e.CompanyId = ecompany.Id;

            e.ModificationDateUTC = DateTime.UtcNow;
            e.CreationDateUTC = DateTime.UtcNow;
            var result = await context.UserCompanies.AddAsync(e);
            await context.SaveChangesAsync();
            return result.Entity; 
        }
        #endregion
    }
}
