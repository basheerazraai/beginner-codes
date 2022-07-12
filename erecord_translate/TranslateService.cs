using Amazon.Extensions.NETCore.Setup;
using Amazon.Translate;
using Amazon.Translate.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace erecord_translate
{
    public class TranslateService
    {

        private static AWSOptions BuildAwsOptions()
        {

        var builder = new ConfigurationBuilder()
            .SetBasePath(Environment.CurrentDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
        return builder.GetAWSOptions();
        }

        private IAmazonTranslate translate;
        public TranslateService(IAmazonTranslate translate)
        {
            this.translate = translate;
        }

        public async Task<ImportTerminologyResponse> SetTerminology(string name, MemoryStream fileStream)
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
