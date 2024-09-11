using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.CognitiveServices.Speech;

namespace AzureAi
{
    public static class TextToSpeech
    {
        public static async Task<string> TransformToSpeech()
        {
            var speechConfig = SpeechConfig.FromSubscription("key", "region");

            //więcej przykładowych głosów: https://learn.microsoft.com/pl-pl/azure/ai-services/speech-service/language-support?tabs=tts 
            speechConfig.SpeechSynthesisVoiceName = "en-US-JennyNeural";

            var audioConfig = AudioConfig.FromWavFileOutput("./tmp.wav");

            using (var speechSynthesizer = new SpeechSynthesizer(speechConfig, audioConfig))
            {
                var speechSynthesisResult = await speechSynthesizer.SpeakTextAsync("I am Kamil from Poland");
                return speechSynthesisResult.Reason.ToString(); 
            }
        }
    }
}
