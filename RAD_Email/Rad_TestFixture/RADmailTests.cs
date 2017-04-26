using System;
using NUnit.Framework;
using RAD_Email;

namespace Rad_TestFixture
{
    [TestFixture]
    public class RADmailTests
    {
        [Test]
        public void SendRADmailTest()
        {
            RAD_Mail x = new RAD_Mail();
            x.SendRADMail("random parameter");
           
            
        }
    }
}
