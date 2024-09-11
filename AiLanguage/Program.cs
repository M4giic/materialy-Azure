using AiLanguage;

//1. Detekcja języka
var result = LanguageDetector.DetectLanguage();
Console.WriteLine(result);

//2. Analiza sentymentu
//var result = SentimentAnalyzer.AnalyzeSentiment();
//Console.WriteLine(result);

//3. Rozpoznawanie encji
//var result = EntityRecognizer.RecognizeEntity();
//Console.WriteLine(result);