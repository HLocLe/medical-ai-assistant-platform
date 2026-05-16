using MedMateAI.Application.DTOs.AIConfigs.Requests;
using MedMateAI.Application.DTOs.AIConfigs.Responses;
using MedMateAI.Application.DTOs.Common;

namespace MedMateAI.Application.IService;

public interface IAIConfigService
{
    Task<PagedResponse<AIConfigResponse>> ListAIConfigsAsync(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<AIConfigResponse>> ListActiveAIConfigsAsync(
        CancellationToken cancellationToken = default);

    Task<AIConfigResponse?> GetAIConfigByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<AIConfigResponse?> GetActiveAIConfigByTaskTypeAsync(
        string taskType,
        CancellationToken cancellationToken = default);

    Task<AIConfigResponse> CreateAIConfigAsync(
        CreateAIConfigRequest request,
        CancellationToken cancellationToken = default);

    Task<AIConfigResponse?> UpdateAIConfigAsync(
        Guid id,
        UpdateAIConfigRequest request,
        CancellationToken cancellationToken = default);

    Task<AIConfigResponse?> UpdateAIConfigStatusAsync(
        Guid id,
        UpdateAIConfigStatusRequest request,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteAIConfigAsync(
        Guid id,
        CancellationToken cancellationToken = default);
}
