using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using CashAccountingEntities;
using CashAccountingIRepo;

namespace CashAccountingSvr.Controller {
    public class BankController : ApiController {

        IBankRepo BankenRepo = null;

        public BankController(IBankRepo bankenRepo) {
            BankenRepo = bankenRepo;
        }

        public IEnumerable<Bank> GetAllBanken() {
            throw new NotImplementedException($"Method '{MethodInfo.GetCurrentMethod().Name}' not implemented yet.");
        }

        public HttpResponseMessage GetBankById(int id) {
            try {
                Bank b = BankenRepo.GetBankById(id);
                if(b == null) {
                    HttpError hErr = new HttpError($"Bank mit Id '{id}' nicht gefunden");
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, hErr);
                }
                return Request.CreateResponse<Bank>(HttpStatusCode.OK, b);
            } catch(Exception ex) {
                HttpError hErr = new HttpError(ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, hErr);
            }
        }

        public IEnumerable<Bank> GetBankenBySelectionString() {
            throw new NotImplementedException($"Method '{MethodInfo.GetCurrentMethod().Name}' not implemented yet.");
        }

        public HttpResponseMessage PostBank(Bank bank) {
            try {
                int newId = BankenRepo.SaveBank(bank);

                HttpResponseMessage msg = Request.CreateResponse<Bank>(HttpStatusCode.Created, bank);

                string uri = Url.Link("BankenAPI", new { id = bank.BankID });
                msg.Headers.Location = new Uri(uri);
                return msg;
            } catch(Exception ex) {
                HttpError hErr = new HttpError(ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, hErr);
            }
            throw new NotImplementedException($"Method '{MethodInfo.GetCurrentMethod().Name}' not implemented yet.");
        }

        public HttpResponseMessage PutBank(int id, Bank bank) {
            throw new NotImplementedException($"Method '{MethodInfo.GetCurrentMethod().Name}' not implemented yet.");
        }

        public HttpResponseMessage DeleteBank(Bank bank) {
            throw new NotImplementedException($"Method '{MethodInfo.GetCurrentMethod().Name}' not implemented yet.");
        }



    }
}
