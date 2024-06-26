using AISchoolManagementApp.Helpers;
using AISchoolManagementApp.Plugins;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;

using Microsoft.KernelMemory;
using Constants = AISchoolManagementApp.Helpers.Constants;
using Microsoft.KernelMemory.AI.OpenAI;
using Microsoft.Extensions.Configuration;

#pragma warning disable SKEXP0001 
#pragma warning disable SKEXP0010
#pragma warning disable SKEXP0020 

namespace AISchoolManagementApp.Services
{
    public class SemanticKernelService : ISemanticKernelService
    {

        Kernel kernel;
        MemoryServerless memory;

        public SemanticKernelService()
        {
            var kb = Kernel.CreateBuilder();

            kb.Services
                .AddAzureOpenAIChatCompletion(
                    Constants.AzureOpenAIDeploymentName,
                    Constants.AzureOpenAIEndpoint,
                    Constants.AzureOpenAIApiKey);

            kb.Plugins.AddFromType<ExamPlugin>();
            kernel = kb.Build();

            var azureOpenAITextConfig = new AzureOpenAIConfig()
            {
                APIKey = Constants.AzureOpenAIApiKey,
                Endpoint = Constants.AzureOpenAIEndpoint,
                APIType = AzureOpenAIConfig.APITypes.ChatCompletion
            };

            var azureAISearchConfigWithHybridSearch = new AzureAISearchConfig()
            {
                APIKey = Constants.AzureOpenAIApiKey,
                Endpoint = Constants.AzureSearchEndpoint,
                UseHybridSearch = true
            };

            var azureOpenAIEmbeddingConfig = new AzureOpenAIConfig()
            {
                APIKey = Constants.AzureOpenAIApiKey,
                Endpoint = Constants.AzureOpenAIEndpoint,
                Deployment = Constants.AzureOpenAIEmbeddings
            };

            var kmb = new KernelMemoryBuilder()
                .WithAzureOpenAITextGeneration(
                    azureOpenAITextConfig, new DefaultGPTTokenizer())
                .WithAzureOpenAITextEmbeddingGeneration(
                    azureOpenAIEmbeddingConfig, new DefaultGPTTokenizer())
                .WithAzureAISearchMemoryDb(
                    azureAISearchConfigWithHybridSearch)
                .WithSearchClientConfig(
                    new SearchClientConfig
                    {
                        MaxMatchesCount = 2,
                        Temperature = 0,
                        TopP = 0
                    });

            memory = kmb.Build<MemoryServerless>();

        }

        public async Task<string> GetExamQuestions(string topic)
        {
            OpenAIPromptExecutionSettings settings = new()
            {
                ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
            };

            var examPrompt = $"Generate an exam about {topic}";

            KernelArguments examArguments = new(settings) { { "topic", topic } };
            var examResult = await kernel.InvokePromptAsync(examPrompt, examArguments);

            return examResult.ToString();
        }

        public async Task AddDocument(string filename)
        {
            // Import a file

                    //, tags: new() { { "user", "Blake" } });


        }

        private async Task<string> AskQuestion(string question)
        {
            var answer = await memory.AskAsync(question, index: Constants.AzureSearchIndex);
            return answer.Result;
        }

        private static async Task FE(IKernelMemory memory)
        {
            /*
            await memory.DeleteIndexAsync(IndexName);

            var data = """
                   aaa bbb ccc 000000000
                   C B A   .......
                   ai bee cee  Something else
                   XY.  abc means  'Aliens Brewing Coffee'
                   abeec abecedario
                   A B C D  first 4 letters
                   """;

            var rows = data.Split("\n");
            foreach (var acronym in rows)
            {
                await memory.ImportTextAsync(acronym, index: IndexName);
            }
            */
        }
    }
}
