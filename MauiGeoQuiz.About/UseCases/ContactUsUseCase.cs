using MauiGeoQuiz.About.Models;
using MauiGeoQuiz.Domain.Architecture;

namespace MauiGeoQuiz.About.UseCases;
public class ContactUsUseCase : ICommandUseCase<ContactUsModel>
{
    public async Task Execute(ContactUsModel request)
    {
        if (Email.Default.IsComposeSupported)
        {
            var message = new EmailMessage
            {
                Subject = $"{request.AppName} {request.AppVersion}",
                Body = "",
                BodyFormat = EmailBodyFormat.PlainText,
                To = [request.EmailAddress]
            };

            await Email.Default.ComposeAsync(message);
        }
    }
}
