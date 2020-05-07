using Server.Codes;
using SharedLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Controllers {
    public class SCompaniesService {


        #region Constructor
        public SCompaniesService(IBlazorAppDatabaseSettings settings) {
        } 
        #endregion

        #region GetByID
        public ECompany GetByID(string id) {
            using var context = new SMySQLContext();
            var e = context.Companies.SingleOrDefault(x => x.Id == id);
            return e;
        }
        #endregion

        #region GetAll
        public List<ECompany> GetAll(int listCount = -1, int pageNumber = 0, string orderBy = "id desc") {
            List<ECompany> list = null;
            using var context = new SMySQLContext();
            if (listCount == -1) list = context.Companies.OrderBy(x => x.Id).ToList();
            else list = context.Companies.Skip(pageNumber * listCount).Take(listCount).OrderBy(x => x.Id).ToList();
            return list;
        }
        #endregion

        //=====================================================GETS ABOVE=====================================================

        #region SaveAsync
        public async Task<string> SaveAsync(ECompany eCompany) {
            eCompany.ModificationDateUTC = DateTime.UtcNow;
            using var context = new SMySQLContext();
            if (string.IsNullOrEmpty(eCompany.Id)) {
                eCompany.Id = Guid.NewGuid().ToString();
                eCompany.CreationDateUTC = DateTime.UtcNow;
                var e = await context.Companies.AddAsync(eCompany);
                await context.SaveChangesAsync();
                return e.Entity.Id;
            } else {
                var e = context.Companies.Update(eCompany);
                await context.SaveChangesAsync();
                return e.Entity.Id;
            }
        }
        #endregion

        #region RemoveAsync
        static public async Task<bool> RemoveAsync(string id) {
            using var context = new SMySQLContext();
            var e = context.Companies.SingleOrDefault(x => x.Id == id);
            if (e == null) return false;
            context.Remove(e);
            await context.SaveChangesAsync();
            return true;
        }
        #endregion

    }
}