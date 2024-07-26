using System;
using System.Collections.Generic;
using api.Exceptions;
using api.Services.Interfaces;
using Azure;
using Azure.Communication.Email;

namespace api.Services
{
    public class AzureMailService : IAzureMailService
    {
        public AzureMailService()
        {

        }

        public async Task<bool> Send(string to, string subject, string body)
        {
            try
            {
                string connectionString = Environment
                    .GetEnvironmentVariable("COMMUNICATION_SERVICES_CONNECTION_STRING")
                    ?? throw new EnvironmentVariableUndefinedException("COMMUNICATION_SERVICES_CONNECTION_STRING");

                string from = Environment.GetEnvironmentVariable("COMMUNICATION_SERVICES_FROM_EMAIL") 
                    ?? throw new EnvironmentVariableUndefinedException("COMMUNICATION_SERVICES_FROM_EMAIL");
                var emailClient = new EmailClient(connectionString);

                EmailSendOperation emailSendOperation = await emailClient.SendAsync(
                    WaitUntil.Completed,
                    senderAddress: from,
                    recipientAddress: to,
                    subject: subject,
                    htmlContent: body,
                    plainTextContent: "Hello world via email."
                    );

                Console.WriteLine($"Email operation id = {emailSendOperation.Id}");
                return true;
            }
            catch(EnvironmentVariableUndefinedException )
            {
                throw;
            }
            catch (Exception e)
            {
                throw new UnableToDoActionException("Unable to send email", e);
            }
        }
    }
}
