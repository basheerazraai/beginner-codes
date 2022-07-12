using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Translate;
using Amazon.Translate.Model;
using Microsoft.Extensions.Configuration;


namespace amazon_translate_window
{
    class AWSTranslate
    {
        const string JapaneseText = @"[務歯]が壊れています。[蝶棒]を変えてください。";
        const string TerminologyName = "test_utf8";
        public AWSTranslate(string[] args)
        {
            var awsOptions = BuildAwsOptions();
            var service = new TranslateService(awsOptions.CreateServiceClient<IAmazonTranslate>());

            // translation with custom Terminology
            // read the csv terminology file
            var memoryStream = new MemoryStream();
            var fileStream = new FileStream("test_utf8.csv", FileMode.Open);
            fileStream.CopyTo(memoryStream);

            // set terminology
            service.SetTerminolgy(TerminologyName, memoryStream).Wait();

            // query with terminology
            var terminologies = new List<string>() { TerminologyName };
            var translateTask = service.TranslateText(JapaneseText, "ja", "en", terminologies);
            translateTask.Wait();
            var result = translateTask.Result;
            var translatedText = result.TranslatedText;

            translateTask.Result.AppliedTerminologies.ForEach(x => {
                Console.WriteLine(x.Name);
            });
            Console.WriteLine("Translation: {0}", translatedText);

            fileStream.Close();
            fileStream.Dispose();
        }

        private static AWSOptions BuildAwsOptions()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            return builder.GetAWSOptions();
        }
    }

    public class TranslateService
    {
        private IAmazonTranslate translate;
        public TranslateService(IAmazonTranslate translate)
        {
            this.translate = translate;
        }

        public async Task<ImportTerminologyResponse> SetTerminolgy(string name, MemoryStream fileStream)
        {
            return await this.translate.ImportTerminologyAsync(new ImportTerminologyRequest
            {
                Name = name,
                MergeStrategy = MergeStrategy.OVERWRITE,
                TerminologyData = new TerminologyData
                {
                    File = fileStream,
                    Format = TerminologyDataFormat.CSV
                }
            });
        }

        public async Task<TranslateTextResponse> TranslateText(string text, string sourceLanguage, string targetLanguage, List<string> terminologies)
        {
            var request = new TranslateTextRequest
            {
                SourceLanguageCode = sourceLanguage,
                TargetLanguageCode = targetLanguage,
                TerminologyNames = terminologies,
                Text = text
            };

            return await this.translate.TranslateTextAsync(request);
        }
    }
}
