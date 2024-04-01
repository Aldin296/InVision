using System;
using System.Net;
using System.Net.Mail;
namespace InVision.Data.Service;

public class EmailService
{
    public void SendEmail(string recipientEmail, string subject, string body)
    {
        // Sender's email address and SMTP server details
        string senderEmail = "your_email@example.com";
        string password = "your_password"; // if you're using SMTP authentication
        string smtpServer = "smtp.example.com";
        int port = 587; // SMTP port (usually 587 for TLS/STARTTLS or 465 for SSL)

        // Create an instance of the SmtpClient
        SmtpClient smtpClient = new SmtpClient(smtpServer, port);

        // Enable SSL/TLS
        smtpClient.EnableSsl = true;

        // Provide credentials for the SMTP server if required
        smtpClient.Credentials = new NetworkCredential(senderEmail, password);

        // Create a MailMessage object
        MailMessage mailMessage = new MailMessage(senderEmail, recipientEmail, subject, body);

        try
        {
            // Send the email
            smtpClient.Send(mailMessage);
            Console.WriteLine("Email sent successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to send email. Error: " + ex.Message);
        }
        finally
        {
            // Clean up resources
            mailMessage.Dispose();
            smtpClient.Dispose();
        }
    }
}
