#region Imports
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic;
using Server.Codes;
using SharedLib.Entities;
#endregion

namespace Server.Controllers {
    public class SAdminUsersService {
        

        #region Constructor
        public SAdminUsersService(IBlazorAppDatabaseSettings settings) {
        } 
        #endregion

        #region GetByID
        public EAdminUser GetByID(string id) {
            using var context = new SMySQLContext();
            EAdminUser e = context.AdminUsers.SingleOrDefault(x => x.Id == id);
            return e;
        }
        #endregion

        #region GetAll
        public List<EAdminUser> GetAll(int listCount = -1, int pageNumber = 0, string orderBy = "id desc") {
            List<EAdminUser> list = null;
            using var context = new SMySQLContext();
            if (listCount == -1) list = context.AdminUsers.OrderBy(x=>x.Id).ToList();
            else list = context.AdminUsers.Skip(pageNumber * listCount).Take(listCount).OrderBy(x=>x.Id).ToList();
            return list;
        }
        #endregion

        #region Authenticate
        public EAdminUser Authenticate(EAdminUser e) {
            using var context = new SMySQLContext();

            //var eUser = new EAdminUser { Email = "bruno@tezine.com", Password = "tata" };
            //eUser.Id = Guid.NewGuid().ToString();
            //eUser.CreationDateUTC = DateTime.UtcNow;
            //context.AdminUsers.Add(eUser);
            //context.SaveChanges();

            var eAdminUser = context.AdminUsers.FirstOrDefault(x => x.Email == e.Email && x.Password == e.Password);
            return eAdminUser;
        }
        #endregion

        //=====================================================GETS ABOVE=====================================================

        #region SaveAsync
        public async Task<string> SaveAsync(EAdminUser eAdminUser) {
            eAdminUser.ModificationDateUTC = DateTime.UtcNow;
            using var context = new SMySQLContext();
            if (string.IsNullOrEmpty(eAdminUser.Id)) {
                eAdminUser.Id = Guid.NewGuid().ToString();
                eAdminUser.CreationDateUTC = DateTime.UtcNow;
                var e = await context.AdminUsers.AddAsync(eAdminUser);
                await context.SaveChangesAsync();
                return e.Entity.Id;
            } else {
                var e = context.AdminUsers.Update(eAdminUser);
                await context.SaveChangesAsync();
                return e.Entity.Id;
            }
        }
        #endregion

        #region RemoveAsync
        public async Task<bool> RemoveAsync(string id) {
            using var context = new SMySQLContext();
            EAdminUser e = context.AdminUsers.SingleOrDefault(x => x.Id == id);
            if (e == null) return false;
            context.Remove(e);
            await context.SaveChangesAsync();
            return true;
        }
        #endregion

    }
}
