using Azure.AI.TextAnalytics;
using Azure;
using System.Text;

namespace AiLanguage
{
    public class SentimentAnalyzer
    {
        public static string AnalyzeSentiment()
        {
            var document = "I recently had the pleasure of delving into the pages of 'Whispers of Serenity,' " +
             "and it was truly a captivating journey. The author's masterful storytelling and evocative " +
             "prose created a world that I found myself completely immersed in from start to finish." +
             "I am happy I bought that book on Amazon!.";

            Uri endpoint = new Uri("uri");
            AzureKeyCredential credential = new AzureKeyCredential("key");
            TextAnalyticsClient client = new(endpoint, credential);

            Response<DocumentSentiment> response = client.AnalyzeSentiment(document);
            DocumentSentiment docSentiment = response.Value;

            var result = new StringBuilder();
            result.AppendLine($"Document sentiment is {docSentiment.Sentiment} with: ");
            result.AppendLine($"  Positive confidence score: {docSentiment.ConfidenceScores.Positive}");
            result.AppendLine($"  Neutral confidence score: {docSentiment.ConfidenceScores.Neutral}");
            result.AppendLine($"  Negative confidence score: {docSentiment.ConfidenceScores.Negative}");

            return result.ToString();
        }
    }
}
