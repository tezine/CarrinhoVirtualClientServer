#region Imports
using Server.Codes;
using SharedLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 
#endregion

namespace Server.Controllers {
    public class SUsersService {

        #region Constructor
        public SUsersService(IBlazorAppDatabaseSettings settings) {
        } 
        #endregion

        #region GetByID
        public EUser GetByID(string id) {
            using var context = new SMySQLContext();
            var e = context.Users.SingleOrDefault(x => x.Id == id);
            return e;
        }
        #endregion

        #region GetAll
        public List<EUser> GetAll(string companyID, int listCount = -1, int pageNumber = 0, string orderBy = "id desc") {
            List<EUser> list = null;
            using var context = new SMySQLContext();
            if (listCount == -1) list = context.Users.OrderBy(x => x.Id).ToList();
            else list = context.Users.Skip(pageNumber * listCount).Take(listCount).OrderBy(x => x.Id).ToList();
            return list;
        }
        #endregion

        //=====================================================GETS ABOVE=====================================================

        #region Authenticate
        public EUser Authenticate(EUser e) {
            using var context = new SMySQLContext();

            //var eUser = new EProduct { Name = "Amora",CompanyID= "3137acad-f0a2-475e-94f7-859849ed9172" };
            //eUser.Id = Guid.NewGuid().ToString();
            //eUser.CreationDateUTC = DateTime.UtcNow;
            //context.Products.Add(eUser);
            //context.SaveChanges();

            var eClient = context.Users.FirstOrDefault(x => x.Email == e.Email && x.Password == e.Password);
            return eClient;
        }
        #endregion

        #region SaveAsync
        public async Task<string> SaveAsync(EUser eClient) {
            eClient.ModificationDateUTC = DateTime.UtcNow;
            using var context = new SMySQLContext();
            if (string.IsNullOrEmpty(eClient.Id)) {
                eClient.Id = Guid.NewGuid().ToString();
                eClient.CreationDateUTC = DateTime.UtcNow;
                var e = await context.Users.AddAsync(eClient);
                await context.SaveChangesAsync();
                return e.Entity.Id;
            } else {
                var e = context.Users.Update(eClient);
                await context.SaveChangesAsync();
                return e.Entity.Id;
            }
        }
        #endregion

        #region RemoveAsync
        public async Task<bool> RemoveAsync(string id) {
            using var context = new SMySQLContext();
            var e = context.Users.SingleOrDefault(x => x.Id == id);
            if (e == null) return false;
            context.Remove(e);
            await context.SaveChangesAsync();
            return true;
        }
        #endregion
    }
}
