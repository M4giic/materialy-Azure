using Azure.AI.TextAnalytics;
using Azure;
using System.Text;

namespace AiLanguage
{
    public class EntityRecognizer
    {
        public static string RecognizeEntity()
        {
            var document = "I recently had the pleasure of delving into the pages of 'Whispers of Serenity,' " +
                "and it was truly a captivating journey. The author's masterful storytelling and evocative " +
                "prose created a world that I found myself completely immersed in from start to finish." + 
                "I am happy I bought that book on Amazon!.";

            Uri endpoint = new Uri("uri");
            AzureKeyCredential credential = new AzureKeyCredential("key");
            TextAnalyticsClient client = new(endpoint, credential);

            Response<CategorizedEntityCollection> response = client.RecognizeEntities(document);
            CategorizedEntityCollection entitiesInDocument = response.Value;

            Console.WriteLine($"Recognized {entitiesInDocument.Count} entities:");
            var result = new StringBuilder();
            foreach (CategorizedEntity entity in entitiesInDocument)
            {
                result.AppendLine($"  Text: {entity.Text}");
                result.AppendLine($"  Offset: {entity.Offset}");
                result.AppendLine($"  Length: {entity.Length}");
                result.AppendLine($"  Category: {entity.Category}");
                if (!string.IsNullOrEmpty(entity.SubCategory))
                    Console.WriteLine($"  SubCategory: {entity.SubCategory}");
                result.AppendLine($"  Confidence score: {entity.ConfidenceScore}");
                result.AppendLine();
            }

            return result.ToString();
        }
    }
}
