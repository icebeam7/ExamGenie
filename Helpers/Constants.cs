using static LLama.Common.ChatHistory;

namespace AISchoolManagementApp.Helpers
{
    public class Constants
    {
        public static readonly string AzureOpenAIDeploymentName = "gpt-4";
        public static readonly string AzureOpenAIEndpoint = "https://utb-openai.openai.azure.com/";
        public static readonly string AzureOpenAIApiKey = "";
        public static readonly string AzureOpenAIEmbeddings = "text-embedding-ada-002";

        public const string AzureSearchKey = "";
        public const string AzureSearchQueryKey = "";
        public const string AzureSearchEndpoint = "https://school-search.search.windows.net";
        public const string AzureSearchIndex = "school-index";

        public static readonly string DbName = "schooldb-v01.db";

        public const string AzureStorageConnectionString = "";
        public const string AzureStorageDocsContainer = "docs";
        public const string AzureStorageExamContainer = "exams";

        public const string SystemMessage =
            "You are a friendly AI assistant that help professors to create " +
            "multiple-choice exams about a given topic. " +
            "Limit your answers to the information obtained from the documents. " +
        "Reply to the query in English language in a 'questions' array" +
        "in JSON format with the fields Number, Content, " +
        "OptionA, OptionB, OptionC, OptionD (add the letter " +
        "A, B, C o D according to the text of the option)," +
        "CorrectOption (which could be A, B, C o D), " +
        "Reference. " +
        "Don't include the citation in the content of the question, only in the" +
        "Reference field. " +
        "\n\nONLY JSON FORMAT IS ALLOWED in the answer. " +
        "No explanation or other text is allowed.";


    }
}
