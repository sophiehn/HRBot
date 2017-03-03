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
            context.Wait<string>(conversationStarted);
        }

        private async Task conversationStarted(IDialogContext context, IAwaitable<string> argument)
        {
            var message = context.MakeMessage();
            await context.PostAsync("Hello there!");
            await context.PostAsync("I am a Bot and I am here to help you with your questions concerning Microsoft Internship and MACHs programs.");
            message.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            var attachment = new List<Attachment>();
            var actions = new List<CardAction>()
            {
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
            if (message.Text == "mach")
            {
                await context.PostAsync("More info about MACH");
                context.Wait(InputGiven);
            }
            else if (message.Text == "internship")
            {
                await context.PostAsync("More info about internship");
                context.Wait(InputGiven);
            }
            else
                await context.Forward(new RootLuisDialog(), ResumeAfterLUIS, message, CancellationToken.None);
        }


        private async Task ResumeAfterLUIS(IDialogContext context, IAwaitable<object> result)
        {
            var action = await result;
            var message = context.MakeMessage();
            context.Wait(InputGiven);
        }

    }
}