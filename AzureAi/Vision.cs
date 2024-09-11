using Azure;
using Azure.AI.Vision.Common;
using Azure.AI.Vision.ImageAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureAi
{
    public static class Vision
    {
        public static string AnalyzeImage()
        {
            var serviceOptions = new VisionServiceOptions("url", new AzureKeyCredential("key"));

            using var imageSource = VisionSource.FromUrl(
                new Uri("https://images.unsplash.com/photo-1520880867055-1e30d1cb001c?q=80&w=1470&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"));

            var analysisOptions = new ImageAnalysisOptions()
            {
                Features = ImageAnalysisFeature.Caption,
                Language = "en",
                GenderNeutralCaption = false
            };

            using var analyzer = new ImageAnalyzer(serviceOptions, imageSource, analysisOptions);

            var result = analyzer.Analyze();

            if (result.Reason == ImageAnalysisResultReason.Analyzed && result.Caption != null)
            {
                return $"\"{result.Caption.Content}\", Confidence {result.Caption.Confidence:0.0000}";
            }

            return "";
        }
    }
}
