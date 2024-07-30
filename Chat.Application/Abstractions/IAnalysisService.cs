using Azure.AI.TextAnalytics;

namespace Chat.Application.Abstractions;

public interface IAnalysisService
{
    TextSentiment MessageAnalysis(string message);
}