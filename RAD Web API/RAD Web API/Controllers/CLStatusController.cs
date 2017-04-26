using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using RAD_Web_API;
using RAD_Web_API.Models;
using System.Security.Principal;
using System.Threading;
using System.Net.Mail;
using System.Text;

namespace RAD_Web_API.Controllers
{
    public class CLStatusController : ApiControllerWithHub<RADHub>
    {
        private RAD_Web_APIContext db = new RAD_Web_APIContext();

        // GET: api/CLStatus
        public IQueryable<CLStatu> GetCLStatus()
        {
            return db.CLStatus;
        }

        // GET: api/CLStatus/5
        [ResponseType(typeof(CLStatu))]
        public async Task<IHttpActionResult> GetCLStatu(int id)
        {
            CLStatu cLStatu = await db.CLStatus.FindAsync(id);
            if (cLStatu == null)
            {
                return NotFound();
            }

            return Ok(cLStatu);
        }

        // PUT: api/CLStatus/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCLStatu(int id, CLStatu cLStatu)
        {
            //String UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            //System.IO.File.WriteAllText(@"C:\Users\Public\WhoAmI.txt", UserName);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cLStatu.CLStatuID)
            {
                return BadRequest();
            }

            db.Entry(cLStatu).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CLStatuExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return Unauthorized();
                }
            }

            var settings = db.Settings;
            
            List<string> clSubscribers = new List<string>();

            foreach (var setting in settings)
            {
                if(setting.UserSettings.Contains("clEmail"))
                {
                    clSubscribers.Add(setting.EmailID);
                }
            }

            if (clSubscribers.Count > 0)
            {
                MailMessage email;
                email = new MailMessage(new MailAddress("CASHLINK-do-not-reply@symetra.com"), new MailAddress(clSubscribers[0]));
                email.Subject = "Cashlink Status Update";
                if(clSubscribers.Count > 1)
                {
                    for(int i = 1; i < clSubscribers.Count; i++)
                    {
                        email.CC.Add(clSubscribers[i]);
                    }
                }

                StringBuilder msg = new StringBuilder("");

                msg.Append("\r\n");
                msg.Append("CashLink Status: " + cLStatu.Status);
                msg.Append("\r\nLast Updated: " + cLStatu.LastUpdate);
                email.Body = msg.ToString();

                SmtpClient smtp = new SmtpClient { Host = "smtp.corp.symetra.com" };
                smtp.Send(email);
            }

            Hub.Clients.All.clStatusUpdate(cLStatu.Status, cLStatu.LastUpdate);
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/CLStatus
        public IHttpActionResult PostCLStatu(CLStatu cLStatu)
        {
            return Unauthorized();
        }

        // DELETE: api/CLStatus/5
        [ResponseType(typeof(CLStatu))]
        public IHttpActionResult DeleteCLStatu(int id)
        {
            return Unauthorized();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CLStatuExists(int id)
        {
            return db.CLStatus.Count(e => e.CLStatuID == id) > 0;
        }
    }
}