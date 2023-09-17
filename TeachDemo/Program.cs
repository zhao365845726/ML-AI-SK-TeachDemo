// See https://aka.ms/new-console-template for more information
//using System.Text;
//using Microsoft.SemanticKernel;
////using Microsoft.SemanticKernel.KernelExtensions;
//using Microsoft.SemanticKernel.Orchestration;
////using MyOpenAI.Shared;

//初始化Kernel
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Orchestration;
using System.Text;

var myKernel = Kernel.Builder.Build();
myKernel.Config.AddAzureOpenAITextCompletionService(
    "davinci-azure",
    "text-davinci-003",
    "{your azure openai endpoint}",
    "{your azure openai key}");
//导入技能    
var mySkill = myKernel.ImportSemanticSkillFromDirectory("Skills", "Learning");
var myContext = new ContextVariables();
StringBuilder histories = new StringBuilder();
Console.WriteLine("Say anything to start practicing English.");
while (true)
{
    Console.ForegroundColor = ConsoleColor.DarkRed;
    var input = Console.ReadLine();
//填充变量
    myContext.Set("history", histories.ToString());
    myContext.Set("input", input);
//运行技能
    var myResult = await myKernel.RunAsync(myContext, mySkill["LearningEnglishSkill"]);
    histories.AppendLine(input);
    histories.AppendLine(myResult.Result.ToString());
    Console.WriteLine(myResult);
}
