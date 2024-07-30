using Azure;
using Azure.AI.TextAnalytics;
using Chat.Application.Abstractions;
using Microsoft.Extensions.Configuration;

namespace Chat.Application.Services
{
    public class AnalysisService : IAnalysisService
    {
        private readonly TextAnalyticsClient _client;

        public AnalysisService(IConfiguration configuration)
        {
            string languageKey = configuration["AnalysisLanguageKey"];
            string languageEndpoint = configuration["AnalysisLanguageEndpoint"];

            var credentials = new AzureKeyCredential(languageKey);
            var endpoint = new Uri(languageEndpoint);
            _client = new TextAnalyticsClient(endpoint, credentials);
        }

        public TextSentiment MessageAnalysis(string message)
        {
            var documents = new List<string> { message };

            AnalyzeSentimentResultCollection reviews = _client.AnalyzeSentimentBatch(
                documents,
                options: new AnalyzeSentimentOptions { IncludeOpinionMining = true });

            return reviews.FirstOrDefault().DocumentSentiment.Sentiment;
        }
    }
}