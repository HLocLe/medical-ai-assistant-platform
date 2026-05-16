using MedMateAI.Application.DTOs.WebChatbot.Requests;
using MedMateAI.Application.DTOs.WebChatbot.Responses;

namespace MedMateAI.Application.IService;

public interface IWebChatbotService
{
    Task<WebChatbotResponse> SendMessageAsync(
        WebChatbotRequest request,
        CancellationToken cancellationToken = default);
}
