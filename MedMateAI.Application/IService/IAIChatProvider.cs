using MedMateAI.Application.DTOs.WebChatbot.Requests;
using MedMateAI.Application.DTOs.WebChatbot.Responses;

namespace MedMateAI.Application.IService;

public interface IAIChatProvider
{
    Task<AIProviderChatResult> GenerateAsync(
        AIProviderChatRequest request,
        CancellationToken cancellationToken = default);
}
