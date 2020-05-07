#region Imports
using Server.Codes;
using SharedLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 
#endregion

namespace Server.Controllers {
    public class SCompaniesAdminUsersService {
        #region Constructor
        public SCompaniesAdminUsersService(IBlazorAppDatabaseSettings settings) {
        }
        #endregion

        #region GetByID
        public ECompanyAdminUser GetByID(string id) {
            using var context = new SMySQLContext();
            var e = context.CompanyAdminUsers.SingleOrDefault(x => x.Id == id);
            return e;
        }
        #endregion

        #region GetAll
        public List<ECompanyAdminUser> GetAll(string companyID, int listCount = -1, int pageNumber = 0, string orderBy = "id desc") {
            List<ECompanyAdminUser> list = null;
            using var context = new SMySQLContext();
            if (listCount == -1) list = context.CompanyAdminUsers.OrderBy(x => x.Id).ToList();
            else list = context.CompanyAdminUsers.Skip(pageNumber * listCount).Take(listCount).OrderBy(x => x.Id).ToList();
            return list;
        }
        #endregion

        //=====================================================GETS ABOVE=====================================================

        #region Authenticate
        public ECompanyAdminUser Authenticate(ECompanyAdminUser e) {
            using var context = new SMySQLContext();

            //var eUser = new ECompanyAdminUser { Name = "Bruno", Email="bruno@tezine.com", Password="tata", CompanyId = "3137acad-f0a2-475e-94f7-859849ed9172" };
            //eUser.Id = Guid.NewGuid().ToString();
            //eUser.CreationDateUTC = DateTime.UtcNow;
            //eUser.ModificationDateUTC = DateTime.UtcNow;
            //context.CompanyAdminUsers.Add(eUser);
            //context.SaveChanges();

            var eClient = context.CompanyAdminUsers.FirstOrDefault(x => x.Email == e.Email && x.Password == e.Password);
            return eClient;
        }
        #endregion

        #region SaveAsync
        public async Task<string> SaveAsync(ECompanyAdminUser eCompanyAdminUser) {
            eCompanyAdminUser.ModificationDateUTC = DateTime.UtcNow;
            using var context = new SMySQLContext();
            if (string.IsNullOrEmpty(eCompanyAdminUser.Id)) {
                eCompanyAdminUser.Id = Guid.NewGuid().ToString();
                eCompanyAdminUser.CreationDateUTC = DateTime.UtcNow;
                var e = await context.CompanyAdminUsers.AddAsync(eCompanyAdminUser);
                await context.SaveChangesAsync();
                return e.Entity.Id;
            } else {
                var e = context.CompanyAdminUsers.Update(eCompanyAdminUser);
                await context.SaveChangesAsync();
                return e.Entity.Id;
            }
        }
        #endregion

        #region RemoveAsync
        public async Task<bool> RemoveAsync(string id) {
            using var context = new SMySQLContext();
            var e = context.CompanyAdminUsers.SingleOrDefault(x => x.Id == id);
            if (e == null) return false;
            context.Remove(e);
            await context.SaveChangesAsync();
            return true;
        }
        #endregion
    }
}
