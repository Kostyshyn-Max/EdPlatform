﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EdPlatform.Business.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EdPlatform.Business.Services
{
    public class SendLanguageData
    {
        public string Language { get; set; }
        public string VersionIndex { get; set; }
    }

    public class CodeExecutingService : ICodeExecutingService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CodeExecutingService> _logger;
        private readonly Dictionary<string, SendLanguageData> _languagesData = new Dictionary<string, SendLanguageData>()
        {
            { "java", new SendLanguageData() { Language = "java", VersionIndex = "4"} },
            { "python", new SendLanguageData() { Language = "python3", VersionIndex = "4" } },
            { "c_cpp", new SendLanguageData() { Language = "g++ 17 GCC 9.1.0", VersionIndex = "0" } },
            { "csharp", new SendLanguageData() { Language = "csharp", VersionIndex = "4" } }
        };
        public CodeExecutingService(HttpClient httpClient, ILogger<CodeExecutingService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<bool>> ExecuteCode(CodeModel codeModel)
        {
            List<string> responses = new List<string>();

            foreach (var input in codeModel.InputDatas)
            {
                string responseString = await SendRequest(codeModel.Code, codeModel.Language, input);

                responses.Add(responseString);
            }

            List<bool> results = new List<bool>();
            for (int i = 0; i < responses.Count(); i++)
            {
                JObject? jObject = JObject.Parse(responses[i]);
                Regex pattern = new Regex("[\a\b\t\r\v\f\n]");
                string output = pattern.Replace(jObject["output"]?.Value<string>(), "");

                results.Add(codeModel.OutputDatas[i].Equals(output));
            }

            return results;
        }

        private async Task<string> SendRequest(string code, string language, string input)
        {
            var values = new
            {
                clientId = "a650e82082a88c3f8f777c6fe27f3bbb",
                clientSecret = "51f0ca0f197a896ee7af15f898cf388f36bc744ae095c82d1fc6f0a2873637af",
                script = code,
                language = _languagesData[language].Language,
                versionIndex = _languagesData[language].VersionIndex,
                stdin = (input != null) ? input : "",
            };
            var content = new StringContent(JsonConvert.SerializeObject(values));
            content.Headers.ContentType.MediaType = "application/json";
            var response = await _httpClient.PostAsync("https://api.jdoodle.com/execute", content);
            var responseString = await response.Content.ReadAsStringAsync();

            return responseString;
        }
    }
}
