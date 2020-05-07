#region Imports
using Server.Codes;
using SharedLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
#endregion

namespace Server.Controllers {
    public class SSearchService {


        #region Constructor
        public SSearchService(IBlazorAppDatabaseSettings settings) {
        }
        #endregion

        #region GetByID
        public ESearch GetByID(string id) {
            using var context = new SMySQLContext();
            ESearch e = context.Searches.SingleOrDefault(x => x.Id == id);
            return e;
        }
        #endregion

        #region GetAll
        public List<ESearch> GetAll(int listCount = -1, int pageNumber = 0, string orderBy = "id desc") {
            List<ESearch> list = null;
            using var context = new SMySQLContext();
            if (listCount == -1) list = context.Searches.OrderBy(x => x.Id).ToList();
            else list = context.Searches.Skip(pageNumber * listCount).Take(listCount).OrderBy(x => x.Id).ToList();
            return list;
        }
        #endregion

        //=====================================================GETS ABOVE=====================================================

        #region AddAsync
        public async Task<string> AddAsync(ESearch eSearch) {
            eSearch.CreationDateUTC = DateTime.UtcNow;
            eSearch.Id = Guid.NewGuid().ToString();
            using var context = new SMySQLContext();
            var e = await context.Searches.AddAsync(eSearch);
            await context.SaveChangesAsync();
            return e.Entity.Id;
        }
        #endregion

        #region RemoveAsync
        static public async Task<bool> RemoveAsync(string id) {
            using var context = new SMySQLContext();
            var e = context.Searches.SingleOrDefault(x => x.Id == id);
            if (e == null) return false;
            context.Remove(e);
            await context.SaveChangesAsync();
            return true;
        }
        #endregion

        public async Task<List<EProduct>> Search(ESearch eSearch) {
            List<EProduct> list = new List<EProduct>();
            //using var context = new SMySQLContext();
            //await AddAsync(eSearch);
            string pattern = @"(\w+)(\s+)?(\w+)?";
            RegexOptions regexOptions = RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.CultureInvariant;
            Regex rx = new Regex(pattern, regexOptions);
            MatchCollection matches = rx.Matches(eSearch.Text);
            if (matches.Count == 0) return list;
            GroupCollection groups = matches[0].Groups;//palavra+espaco+palavra
            if (groups.Count < 2) return list;
            string firstWord = groups[1].Value;
            double amount = GetAmount(firstWord);
            if (amount < 0) return list;
            var resultList = GetProductInSearch(groups);
            foreach(EProduct eProduct in resultList) {
                eProduct.Amount = amount;
                eProduct.TotalPrice = ((decimal)eProduct.Amount) * eProduct.UnitPrice;
            }
            return resultList;
        }

        private double GetAmount(string firstWord) {
            if(double.TryParse(firstWord,out double result)) {
                return result;
            }
            switch (firstWord.ToLower()) {
                case "um":
                case "uma":
                    return 1;
                case "dois":
                    return 2;
                case "tres":
                    return 3;
                case "quatro":
                    return 4;
                case "cinco":
                    return 5;
                case "seis":
                    return 6;
                case "sete":
                    return 7;
                case "oito":
                    return 8;
                case "nove":
                    return 9;
                case "dez":
                    return 10;
                case "onze":
                    return 11;
                case "doze":
                    return 12;
                case "treze":
                    return 13;
                case "quatorze":
                case "catorze":
                    return 14;
                case "quinze":
                    return 15;
                case "dezesseis":
                    return 16;
                case "dezessete":
                    return 17;
                case "dezoito":
                    return 18;
                case "dezenove":
                    return 19;
                case "vinte":
                    return 20;
            }
            return -1;
        }

        private List<EProduct> GetProductInSearch(GroupCollection groups) {
            var list = new List<EProduct>();
            if (groups.Count < 3) return list;
            string product=groups[3].Value;
            if (product.Length < 3) return list;
            using var context = new SMySQLContext();
            list = context.Products.Where(x => x.Tags.Contains(product)).ToList();
            return list;
        }
    }
}
