using System;
using System.Net.Mail;
using Mailgun;
using Mailgun.Messages;
using System.Threading.Tasks;

public class EmailService
{
    private readonly string _apiKey;
    private readonly string _domain;

    public EmailService(string apiKey, string domain)
    {
        _apiKey = apiKey;
        _domain = domain;
    }
    /*
    public async Task SendEmailAsync(string recipientEmail, string recipientName, string subject, string body)
    {
        var message = new MailMessage();
        message.From = new MailAddress("your_email@example.com", "Your Name");
        message.To.Add(new MailAddress(recipientEmail, recipientName));
        message.Subject = subject;
        message.Body = body;
        message.IsBodyHtml = true; // Optionally, set to false if the body is plain text

        var client = new MailgunClient(_apiKey, _domain);
        var response = await client.SendEmailAsync(_domain, message);
        // Check response for any errors or handle as necessary
    }*/
}
