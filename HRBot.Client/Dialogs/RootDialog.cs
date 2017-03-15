using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.Bot.Connector;
using System.Threading;

namespace HRBot.Client
{
    [Serializable()]
    public class RootDialog : IDialog<string>
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait<string>(ConversationStarted);
        }

        private async Task ConversationStarted(IDialogContext context, IAwaitable<string> argument)
        {
            var message = context.MakeMessage();
            await context.PostAsync("Hello there!");
            await context.PostAsync("I am a Bot and I am here to help you with your questions concerning Microsoft Internship and MACHs programs.");
            message.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            var attachment = new List<Attachment>();
            var actions = new List<CardAction>()
            {
                AttachmentsHelper.CreateCardAction("Learn about Microsoft", "What is Microsoft", ActionTypes.PostBack),
                AttachmentsHelper.CreateCardAction("Learn about MACH", "mach", ActionTypes.PostBack),
                AttachmentsHelper.CreateCardAction("Learn about Internships", "internship", ActionTypes.PostBack),
                AttachmentsHelper.CreateCardAction("Apply for a Job", "Apply for Job", ActionTypes.PostBack)
            };
            var card = AttachmentsHelper.CreateHeroCardAttachment("What do you want to know?", "Click on the buttons below to get started or type your question below", null, null, actions);
            attachment.Add(card);
            message.Attachments = attachment;
            await context.PostAsync(message);
            context.Wait(InputGiven);
        }

        public async Task InputGiven(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;
            if (message.Text.Contains("mach"))
            {
                await context.PostAsync("The MACH program is a customized learning experience designed for our newest university hires in various full-time roles within the Evangelism, Finance, IT, Marketing, Operations, Sales and Services organizations. This two-year development experience complements your job and the support you receive from your manager and team. We do this by offering you world-class training, online resources, coaching from some of the brightest minds in the industry and a global community of MACHs from more than 70 countries. The content focuses on starting strong, accelerating your impact, building your network and driving your career at Microsoft.");
                //var message2 = context.MakeMessage();
                //message2.AttachmentLayout = AttachmentLayoutTypes.List;
                //var attachment = new List<Attachment>();
                //var actions = new List<CardAction>()
                //{
                //    AttachmentsHelper.CreateCardAction("Evangelism", "Evangelism", ActionTypes.PostBack),
                //    AttachmentsHelper.CreateCardAction("IT", "IT", ActionTypes.PostBack),
                //    AttachmentsHelper.CreateCardAction("Operations", "Operations", ActionTypes.PostBack),
                //    AttachmentsHelper.CreateCardAction("Marketing", "Marketing", ActionTypes.PostBack),
                //    AttachmentsHelper.CreateCardAction("Sales", "Sales", ActionTypes.PostBack),
                //    AttachmentsHelper.CreateCardAction("Services", "Services", ActionTypes.PostBack),
                //    AttachmentsHelper.CreateCardAction("Opportunities for MBAs", "Opportunities for MBAs", ActionTypes.PostBack)
                //};
                //var card = AttachmentsHelper.CreateHeroCardAttachment("The MACH Experience-Tracks", "If you’re ready to empower your career, we welcome you to apply for a role and join our MACH program to help us help the world achieve more.", null, "https://careers.microsoft.com/content/images/students/mach.jpg", actions);
                //attachment.Add(card);
                //message2.Attachments = attachment;
                //await context.PostAsync(message2);
                //context.Wait(MoreInfoMACH);
                context.Wait(InputGiven);
            }
            else if (message.Text.Contains("internship"))
            {
                await context.PostAsync("At Microsoft, our interns work on projects that matter – and your team will rely on your skills and insights to help deliver those projects to market. You’ll get the opportunity to work on real projects and have fun along the way. This is your chance to show off your skills and work on cutting-edge technology. We offer internships in all job families and product areas. Imagine yourself as a Microsoft intern. Join Microsoft today, and help us shape the business of tomorrow.");
                context.Wait(InputGiven);
            }
            else
                await context.Forward(new RootLuisDialog(), ResumeAfterLUIS, message, CancellationToken.None);
        }

        public async Task MoreInfoMACH(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;
            if (message.Text.Contains("move on"))
                context.Wait(InputGiven);
            else
            {
                var message2 = MACHTracks(message);
                if (message2 == "move on")
                    context.Wait(InputGiven);
                else
                {
                    await context.PostAsync(message2);
                    context.Wait(MoreInfoMACH);
                }
            }
        }

        private static string MACHTracks(IMessageActivity message)
        {
            if (message.Text.Contains("Evangelism"))
                message.Text = "We’re passionate about working with developers in a technical advisory role to help them deliver next-generation software solutions and applications. Your influence will extend to developers both in person and at regional events. You will grow your skills in both technology and communication. You will become comfortable presenting the overall business story to a technical audience as well as rolling up your sleeves and talking technology with developers of some of the most interesting applications being created today.";
            else if (message.Text.Contains("IT"))
                message.Text = "We focus on building breadth and depth in modern technologies to help Microsoft solve big problems and remain competitive in a cloud-first, mobile-first world though an immersive two-year developmental experience. At the end of the program, the objective is for you to have: developed a strong network of peers, built a breadth of knowledge across the Microsoft IT ecosystem, increased your self-awareness and accelerated your career trajectory. We can’t wait for you to experience Modern IT!";
            else if (message.Text.Contains("Operations"))
                message.Text = "We focus on the strategy, development and execution of Microsoft’s physical and digital supply chain solutions in mature and emerging markets. This includes the operationalization of new business models in the cloud transformation and support for our teams and field for product planning, launch, release, and order management.";
            else if (message.Text.Contains("Marketing"))
                message.Text = "We are avid marketers, passionate about telling the Microsoft story and the way our products can help people and businesses throughout the world achieve more.";
            else if (message.Text.Contains("Sales"))
                message.Text = "We’re passionate about interacting with our customers and partners to bring the magic of Microsoft to consumers and businesses.";
            else if (message.Text.Contains("Services"))
                message.Text = "We are leaders with a passion for serving our customers and partners, helping them realize their full potential. We deliver consulting, support and customer services to businesses around the globe.";
            else if (message.Text.Contains("Opportunities for MBAs"))
                message.Text = "We hire MBAs into Evangelism, Finance, Marketing, Operations, Sales and Services disciplines in more than 70 countries. If you are currently working toward your full-time MBA degree and have up to seven years of professional work experience, we have an opportunity just for you. The focus the MACH MBA experience is on accelerating your time to impact through targeted training and resources that build on your prior work experience and MBA. The roadmap includes online and classroom training and resources that build a strong understanding of Microsoft and your organization and provides a place to build your global MACH MBA community.";
            else
                message.Text = "move on";
            return message.Text;
        }

        private async Task ResumeAfterLUIS(IDialogContext context, IAwaitable<object> result)
        {
            var action = await result;
            var message = context.MakeMessage();
            context.Wait(InputGiven);
        }

    }
}