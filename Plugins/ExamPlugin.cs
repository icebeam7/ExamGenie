using System.ComponentModel;
using AISchoolManagementApp.Helpers;
using AISchoolManagementApp.Services;
using Microsoft.SemanticKernel;
using AISchoolManagementApp.Models;

namespace AISchoolManagementApp.Plugins
{
    public class ExamPlugin
    {
        IExamService examService;

        public ExamPlugin(IExamService examService)
        {
            this.examService = examService;
        }

        [KernelFunction]
        [Description("Generates exam questions from a topic in JSON format")]
        public async Task<Exam> GetExamQuestions(
            [Description("The name of the topic")] string topic)
        {
            return await examService.GenerateNewExamAsync(topic);
        }
    }
}
