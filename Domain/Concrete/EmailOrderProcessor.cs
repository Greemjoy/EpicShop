using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Concrete
{
    public class EmailSettings
    {
        public string MailToAdress = "orders@example.com";
        public string MailFrmoAress = "epicstore@example.com";
        public bool UseSsl = true;
        public string Username = "MySmtpUsername";
        public string Password = "MySmtpPassword";
        public int ServerPort = 587;
        public bool WriteAsFile = true;
        public string FileLocation = @"C:\Users\Gref\Documents\GitHub\EpicShop\emails";
    }
    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings emailSettings;
        public EmailOrderProcessor(EmailSettings settings)
        {
            emailSettings = settings;
        }
        public void ProcessorOrder(Cart cart, ShippingDetails shippingDetails)
        {
            using(var smtpClien = new SmtpClient())
            {

            }
        }
    }
}
