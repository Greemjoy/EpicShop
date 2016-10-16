using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using System.Net;
using System.Net.Mail;
namespace Domain.Concrete
{
    public class EmailSettings
    {
        public string MailToAddress = "orders@example.com";
        public string MailFromAddress = "epicstore@example.com";
        public bool UseSsl = true;
        public string Username = "MySmtpUsername";
        public string Password = "MySmtpPassword";
        public string ServerName = "smtp.example.com";
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
                smtpClien.EnableSsl = emailSettings.UseSsl;
                smtpClien.Host = emailSettings.ServerName;
                smtpClien.Port = emailSettings.ServerPort;
                smtpClien.UseDefaultCredentials = false;
                smtpClien.Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password);

                if (emailSettings.WriteAsFile)
                {
                    smtpClien.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClien.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClien.EnableSsl = false;
                }

                StringBuilder body = new StringBuilder()
                    .Append("New order is processed")
                    .Append("---")
                    .Append("Products:");

                foreach (var line in cart.Lines)
                {
                    var subtotal = line.Guitar.Price * line.Quantity;
                    body.AppendFormat("{0} x {1} (in total: {2:c}", line.Quantity, line.Guitar, subtotal);
                }

                body.AppendFormat("Total Price: {0:c}", cart.ComputeTotalValue())
                    .AppendLine("---")
                    .AppendLine("Delivery")
                    .AppendLine(shippingDetails.Name)
                    .AppendLine(shippingDetails.Line1)
                    .AppendLine(shippingDetails.Line2 ?? "")
                    .AppendLine(shippingDetails.Line3 ?? "")
                    .AppendLine(shippingDetails.City)
                    .AppendLine(shippingDetails.Country)
                    .AppendLine("---")
                    .AppendFormat("Gift wrap: {0}", shippingDetails.GiftWrap ? "Yes" : "No");

                MailMessage mailMessage = new MailMessage(
                    emailSettings.MailFromAddress,
                    emailSettings.MailToAddress,
                    "New order is shipped!",
                    body.ToString()
                    );

                if (emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.UTF8;
                }

                smtpClien.Send(mailMessage);
            }
        }
    }
}
