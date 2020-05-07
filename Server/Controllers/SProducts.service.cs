#region Imports
using Server.Codes;
using SharedLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 
#endregion

namespace Server.Controllers {
    public class SProductsService {

        #region Constructor
        public SProductsService(IBlazorAppDatabaseSettings settings) {
        }
        #endregion

        #region GetByID
        public EProduct GetByID(string id) {
            using var context = new SMySQLContext();
            var e = context.Products.SingleOrDefault(x => x.Id == id);
            return e;
        }
        #endregion

        #region GetAll
        public List<EProduct> GetAll(string companyID, int listCount = -1, int pageNumber = 0, string orderBy = "id desc") {
            List<EProduct> list = null;
            using var context = new SMySQLContext();
            if (listCount == -1) list = context.Products.OrderBy(x => x.Id).ToList();
            else list = context.Products.Skip(pageNumber * listCount).Take(listCount).OrderBy(x => x.Id).ToList();
            return list;
        }
        #endregion

        //=====================================================GETS ABOVE=====================================================

        #region SaveAsync
        public async Task<string> SaveAsync(EProduct eProduct) {
            eProduct.ModificationDateUTC = DateTime.UtcNow;
            using var context = new SMySQLContext();
            if (string.IsNullOrEmpty(eProduct.Id)) {
                eProduct.Id = Guid.NewGuid().ToString();
                eProduct.CreationDateUTC = DateTime.UtcNow;
                var e = await context.Products.AddAsync(eProduct);
                await context.SaveChangesAsync();
                return e.Entity.Id;
            } else {
                var e = context.Products.Update(eProduct);
                await context.SaveChangesAsync();
                return e.Entity.Id;
            }
        }
        #endregion

        #region RemoveAsync
        static public async Task<bool> RemoveAsync(string id) {
            using var context = new SMySQLContext();
            var e = context.Products.SingleOrDefault(x => x.Id == id);
            if (e == null) return false;
            context.Remove(e);
            await context.SaveChangesAsync();
            return true;
        }
        #endregion
    }
}
