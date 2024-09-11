using Azure.AI.TextAnalytics;
using Azure;

namespace AiLanguage
{
    public class LanguageDetector
    {
        public static string DetectLanguage()
        {
            var document = "I recently had the pleasure of delving into the pages of 'Whispers of Serenity,' " +
                "and it was truly a captivating journey. The author's masterful storytelling and evocative " +
                "prose created a world that I found myself completely immersed in from start to finish." +
                "I am happy I bought that book on Amazon!.";

            Uri endpoint = new Uri("uri");
            AzureKeyCredential credential = new AzureKeyCredential("key");
            TextAnalyticsClient client = new(endpoint, credential);

            Response<DetectedLanguage> response = client.DetectLanguage(document);
            DetectedLanguage language = response.Value;

            return $"Detected language is {language.Name} with a confidence score of {language.ConfidenceScore}.";
        }
    }
}
