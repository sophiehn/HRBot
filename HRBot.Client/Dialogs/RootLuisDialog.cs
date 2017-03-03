using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRBot.Client
{

    [LuisModel("1aff8329-b6a4-42d7-bc26-2fb0cf9ccbdd", "61f964c98ea04db8995a1f420ae01492")]
    [Serializable()]
    public class RootLuisDialog : LuisDialog<string>
    {
        [LuisIntent("")]
        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            string message = $"Sorry, I did not understand '{result.Query}'. Type 'help' if you need assistance.";
            await context.PostAsync(message);
            context.Wait(this.MessageReceived);
        }

        [LuisIntent("Help")]
        public async Task Help(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Just type in your question or select one of the below options");
            var message = context.MakeMessage();
            message.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            var attachment = new List<Attachment>();
            var actions = new List<CardAction>()
            {
                AttachmentsHelper.CreateCardAction("Learn about MACH", "mach", ActionTypes.PostBack),
                AttachmentsHelper.CreateCardAction("Learn about Internships", "intern", ActionTypes.PostBack),
                AttachmentsHelper.CreateCardAction("Apply for a Job", "Apply for Job", ActionTypes.PostBack)
            };
            var card = AttachmentsHelper.CreateHeroCardAttachment("What do you want to know?", "Click on the buttons below to get started or type your question below", null, null, actions);
            attachment.Add(card);
            message.Attachments = attachment;
            await context.PostAsync(message);
            // context.Wait(InputGiven);
            context.Wait(this.MessageReceived);
        }

        //public async Task InputGiven(IDialogContext context, IAwaitable<IMessageActivity> argument)
        //{
        //    var message = await argument;
        //    if (message.Text == "mach")
        //    {
        //        await context.PostAsync("More info about MACH");
        //        context.Wait(InputGiven);
        //    }
        //    else if (message.Text == "intern")
        //    {
        //        await context.PostAsync("More info about internship");
        //        context.Wait(InputGiven);
        //    }
        //    else
        //    {
        //        context.Wait(this.MessageReceived);
        //    }

        //}

        [LuisIntent("Greeting")]
        public async Task Greeting(IDialogContext context, LuisResult result)
        {
            string message = "Hey there :) Need any help? Just ask for it!";
            await context.PostAsync(message);
            context.Wait(this.MessageReceived);
        }

        [LuisIntent("IntroduceBot")]
        public async Task IntroduceBot(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("I am a Bot and I am here to help you with your questions concerning Microsoft Internship and MACHs programs. What do you want to know?");
            context.Wait(this.MessageReceived);
        }

        [LuisIntent("SalaryInfo")]
        public async Task SalaryInfo(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("At Microsoft we offer competitive salary level to our employees. The exact amount depends on many factors as skills level, role, location, professional growth, contribution to the team and some other factors. Some roles have only fix salary and yearly bonus. Many roles have also quarter profit sharings.");
            context.Wait(this.MessageReceived);
        }

        [LuisIntent("LanguageCheck")]
        public async Task LanguageCheck(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("I can speak only English. You should be able to do so if you are applying Microsoft ;)");
            context.Wait(this.MessageReceived);
        }

        [LuisIntent("SalaryRaise")]
        public async Task SalaryRaise(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Your salary will be reviewed yearly, but we can not guarantee the growth as it depends on your professional growth, achievements, etc. ");
            context.Wait(this.MessageReceived);
        }

        [LuisIntent("Benefits")]
        public async Task Benefits(IDialogContext context, LuisResult result)
        {
            // var message = await activity;
            string message = "";
            List<string> intents = new List<string>();
            List<string> entities = new List<string>();
            foreach (var e in result.Intents)
            {
                intents.Add(e.Intent);
            }
            foreach (var e in result.Entities)
            {
                entities.Add(e.Entity);
            }
            if (entities.Count > 0)
            {
                if (entities[0].Contains("car"))
                    message = "Interns are not eligidble for corporate cars. If you are a MACH, you can get a corporate car but it depends on the role. You are elidgible for one if your role is customer facing (for example: Solution Sales, Account Manager, Technical Sales)";
                if (entities[0].Contains("phone") || entities[0].Contains("mobile") || entities[0].Contains("cell"))
                    message = "For most positions, you get a smartphone and a corporate phone number once you are hired. You can also use your own phone if you like.";
                if (entities[0].Contains("laptop") || entities[0].Contains("pc") || entities[0].Contains("computer"))
                    message = "We have broad pool of laptops. Sometimes you get to choose, depending on your role. You can also use your own device, if you like.";
                if (entities[0].Contains("food") || entities[0].Contains("lunch") || entities[0].Contains("dinner"))
                    message = "In some countries, you will get a lunch allowance every month for free to use in the Microsoft Cafeteria.";
                if (entities[0].Contains("perk") || entities[0].Contains("benefit") || entities[0].Contains("social"))
                    message = "Depending on the country you can be provided with life and medical Insurance for and your family, food and beverages, flexible hours and working from home facilities, Corp Car (if eligible), Latest Devices.";
                else
                    message = "Depending on the country and if you are an Intern or a MACH, you can be provided with life and medical Insurance for and your family, food and beverages, flexible hours and working from home facilities, Corp Car (if eligible), Latest Devices.";
            }

            await context.PostAsync(message);
            context.Wait(this.MessageReceived);

        }

        [LuisIntent("InfoForMSProducts")]
        public async Task InfoForMSProducts(IDialogContext context, LuisResult result)
        {
            string message = "";
            List<string> intents = new List<string>();
            List<string> entities = new List<string>();
            foreach (var e in result.Intents)
            {
                intents.Add(e.Intent);
            }
            foreach (var e in result.Entities)
            {
                entities.Add(e.Entity);
            }
            if (entities.Count > 0)
            {
                switch (entities[0])
                {
                    case "iot":
                        message = "The Internet of Things (IoT) is the internetworking of physical devices, vehicles (also referred to as 'connected devices' and 'smart devices'), buildings, and other items—embedded with electronics, software, sensors, actuators, and network connectivity that enable these objects to collect and exchange data.";
                        break;
                    case "machine learning":
                        message = "Machine learning is a type of artificial intelligence (AI) that provides computers with the ability to learn without being explicitly programmed. Machine learning focuses on the development of computer programs that can change when exposed to new data.  The process of machine learning is similar to that of data mining. Both systems search through data to look for patterns. However, instead of extracting data for human comprehension -- as is the case in data mining applications -- machine learning uses that data to detect patterns in data and adjust program actions accordingly.  Machine learning algorithms are often categorized as being supervised or unsupervized. Supervised algorithms can apply what has been learned in the past to new data. Unsupervised algorithms can draw inferences from datasets.";
                        break;
                    case "bot":
                        message = "Short for chat robot, a computer program that simulates human conversation, or chat, through artificial intelligence.Typically, a chat bot will communicate with a real person, but applications are being developed in which two chat bots can communicate with each other.Chat bots are used in applications such as ecommerce customer service, call centers and Internet gaming.Chat bots used for these purposes are typically limited to conversations regarding a specialized purpose and not for the entire range of human communication.";
                        break;
                    case "azure":
                        message = "Microsoft Azure is a growing collection of integrated cloud services that developers and IT professionals use to build, deploy, and manage applications through our global network of datacenters. With Azure, you get the freedom to build and deploy wherever you want, using the tools, applications, and frameworks of your choice. Find out more here: https://azure.microsoft.com/en-us/overview/what-is-azure/?b=17.02";
                        break;
                    case "xbox":
                        message = "XBOX Description";
                        break;
                    case "sway":
                        message = "Sway is story telling app, which is also part of Microsoft Office and designed for creating presentations, is in many ways an alternative to PowerPoint.But what is the difference and what can it do? The purpose of Sway is to convey concepts quickly, easily and clearly. Unlike PowerPoint, it is primarily for presenting ideas onscreen rather than to an audience.Tutorials, topic introductions and interactive reports are the sort of things to which it lends itself.Sway presentations are backed up to the cloud, and can be easily shared or embedded in websites.Check examples here: https://sway.com/";
                        break;
                    default:
                        message = "Default message on Info for MS Products";
                        break;
                }
            }

            await context.PostAsync(message);

            context.Wait(this.MessageReceived);

        }

        [LuisIntent("MoreInfoForPrograms")]
        public async Task MoreInfoForPrograms(IDialogContext context, LuisResult result)
        {
            List<string> intents = new List<string>();
            List<string> entities = new List<string>();
            foreach (var e in result.Intents)
            {
                intents.Add(e.Intent);
            }
            foreach (var e in result.Entities)
            {
                entities.Add(e.Entity);
            }
            if (entities.Count > 0)
            {
                if (entities[0] == "mach")
                    await context.PostAsync("More info about MACH");
                else if (entities[0].Contains("intern"))
                    await context.PostAsync("More info about internships");
                else if (entities[0].Contains("no"))
                    await context.PostAsync("Don't worry, you can still apply for all the other roles at https://careers.microsoft.com/");
            }
            else
            {
                await context.PostAsync("That's cool! Are you a student? If yes, there are two Graduate Opportunities for you.");
                var message = context.MakeMessage();
                message.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                var attachment = new List<Attachment>();
                var actions = new List<CardAction>()
            {
                AttachmentsHelper.CreateCardAction("MACH Opportunities", "more info for mach", ActionTypes.PostBack),
                AttachmentsHelper.CreateCardAction("Internships Opportunities", "more info for intern", ActionTypes.PostBack),
                AttachmentsHelper.CreateCardAction("I am not a Student", "i want to know more but no-student", ActionTypes.PostBack)
            };
                var card = AttachmentsHelper.CreateHeroCardAttachment("What do you want to know?", "Click on the buttons below to learn more about the opportunities", null, null, actions);
                attachment.Add(card);
                message.Attachments = attachment;
                await context.PostAsync(message);
            }
            context.Wait(this.MessageReceived);
        }

        [LuisIntent("BusinessTravel")]
        public async Task BusinessTravel(IDialogContext context, LuisResult result)
        {
            List<string> intents = new List<string>();
            List<string> entities = new List<string>();
            foreach (var e in result.Intents)
            {
                intents.Add(e.Intent);
            }
            foreach (var e in result.Entities)
            {
                entities.Add(e.Entity);
            }
            if (entities.Count > 0)
                if (entities[0] == "mach")
                    await context.PostAsync("MACH program includes some trainings abroad. When it takes to another business trips it depends on the role and the territory where you are working on.");
                else
                    await context.PostAsync("Most employees have business or training trips, both domestic and international. It depends on the role and the territory where you are working on.");
            context.Wait(this.MessageReceived);

        }

        [LuisIntent("OfficeInfo")]
        public async Task OfficeInfo(IDialogContext context, LuisResult result)
        {
            List<string> intents = new List<string>();
            List<string> entities = new List<string>();
            foreach (var e in result.Intents)
            {
                intents.Add(e.Intent);
            }
            foreach (var e in result.Entities)
            {
                entities.Add(e.Entity);
            }
            if (entities.Count > 0)
                await context.PostAsync($"You requested info for the Microsoft Office in {entities[0]}.");
            else
                await context.PostAsync("You requested info for the Microsoft Office.");
            context.Wait(this.MessageReceived);
        }

        [LuisIntent("ApplyForJob")]
        public async Task ApplyForJob(IDialogContext context, LuisResult result)
        {
            List<string> intents = new List<string>();
            List<string> entities = new List<string>();
            foreach (var e in result.Intents)
            {
                intents.Add(e.Intent);
            }
            foreach (var e in result.Entities)
            {
                entities.Add(e.Entity);
            }
            if (entities.Count > 0)
            {
                if (entities[0] == "mach")
                {
                    await context.PostAsync("Are you a fresh graduate? If yes, you can explore the Graduate Opportunities at https://careers.microsoft.com/ website, at the 'Students and Graduates' tab. if you are not a student, then you can explore the vacancies for experienced professionals in the same website. ");
                }
                else if (entities[0].Contains("intern"))
                {
                    await context.PostAsync("You can apply for internship if you are fresh graduate or final year student. You can explore the Graduate Opportunities at https://careers.microsoft.com/ website, at the 'Students and Graduates' tab.");
                }
                else if (entities[0].Contains("no"))
                    await context.PostAsync("Don't worry, you can still apply for all the other roles at https://careers.microsoft.com/");
            }
            else
            {
                await context.PostAsync("Are you a student? If yes, there are two Graduate Opportunities for you.");
                var message = context.MakeMessage();
                message.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                var attachment = new List<Attachment>();
                var actions = new List<CardAction>()
            {
                AttachmentsHelper.CreateCardAction("MACH Opportunities", "apply for mach", ActionTypes.PostBack),
                AttachmentsHelper.CreateCardAction("Internships Opportunities", "apply for intern", ActionTypes.PostBack),
                AttachmentsHelper.CreateCardAction("I am not a Student", "apply but no-student", ActionTypes.PostBack)
            };
                var card = AttachmentsHelper.CreateHeroCardAttachment("What do you want to know?", "Click on the buttons below to learn more about the opportunities", null, null, actions);
                attachment.Add(card);
                message.Attachments = attachment;
                await context.PostAsync(message);
            }
            context.Wait(this.MessageReceived);

        }

    }
}