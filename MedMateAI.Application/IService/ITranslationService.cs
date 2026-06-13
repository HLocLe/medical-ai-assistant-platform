namespace MedMateAI.Application.IService;

public interface ITranslationService
{
    Task<string> TranslateToEnglishAsync(
        string text,
        CancellationToken cancellationToken = default);
}
