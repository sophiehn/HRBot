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
                AttachmentsHelper.CreateCardAction("Learn about Microsoft", "What is Microsoft", ActionTypes.PostBack),
                AttachmentsHelper.CreateCardAction("Learn about MACH", "More info for MACH", ActionTypes.PostBack),
                AttachmentsHelper.CreateCardAction("Learn about Internships", "More info for Intern", ActionTypes.PostBack),
                AttachmentsHelper.CreateCardAction("Apply for a Job", "Apply for Job", ActionTypes.PostBack)
            };
            var card = AttachmentsHelper.CreateHeroCardAttachment("What do you want to know?", "Click on the buttons below to get started or type your question below", null, null, actions);
            attachment.Add(card);
            message.Attachments = attachment;
            await context.PostAsync(message);
            context.Wait(this.MessageReceived);
        }

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

        [LuisIntent("Misc")]
        public async Task Misc(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Is there anything else I can help you with? Ask me anything :)");
            context.Wait(this.MessageReceived);
        }

        [LuisIntent("DressCode")]
        public async Task DressCode(IDialogContext context, LuisResult result)
        {
            string message = "We don’t have dress code. You are free to dress whatever you like. Most employees go with Business Casual when in the office. When we are meeting with a client, we just dress according to common sense.";
            await context.PostAsync(message);
            context.Wait(this.MessageReceived);
        }

        [LuisIntent("Colleagues")]
        public async Task Colleagues(IDialogContext context, LuisResult result)
        {
            string message = "Colleagues Info";
            await context.PostAsync(message);
            context.Wait(this.MessageReceived);
        }

        [LuisIntent("MicrosoftInfo")]
        public async Task MicrosoftInfo(IDialogContext context, LuisResult result)
        {
            string message = "What is microsoft..";
            await context.PostAsync(message);
            message = "Wanna know something specific? Just ask me :)";
            await context.PostAsync(message);
            context.Wait(this.MessageReceived);
        }

        [LuisIntent("WorkingForMicrosoft")]
        public async Task WorkingForMicrosoft(IDialogContext context, LuisResult result)
        {
            List<string> intents = new List<string>();
            List<string> entities = new List<string>();
            string message = "";
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
                if (entities[0].Contains("culture"))
                    message = "We have several values: Growth mindset. Customer obsessed. Diversity and inclusion. One Microsoft. Making a difference";
                else if (entities[0].Contains("mission") || entities[0].Contains("strategy"))
                    message = "Our mission/strategy is to empower every person and every organization on the planet to achieve more.";
            }
            else
            {
                message = "It is amazing to work at Microsoft, especially when you are at the start of your career. One of best things about working in the technology industry is its fast pace.With significant developments happening all the time, technology is changing and staying up-to - date is crucial, so people are looking for ways to keep themselves relevant. Microsoft provides great opportunities to learn about both the company and the technologies, and helps you to grow and learn more and more.";
            }
            await context.PostAsync(message);
            context.Wait(this.MessageReceived);
        }

        [LuisIntent("WorkingHours")]
        public async Task WorkingHours(IDialogContext context, LuisResult result)
        {
            List<string> intents = new List<string>();
            List<string> entities = new List<string>();
            string message = "";
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
                if (entities[0].Contains("weekend"))
                    message = "We have several values: Growth mindset. Customer obsessed. Diversity and inclusion. One Microsoft. Making a difference";
                else if (entities[0].Contains("overtime"))
                    message = "Microsoft employees have flexible working hours and we give them up to 3 additional days of vacation in this case.";
                else if (entities[0].Contains("home"))
                    message = "Of course. How often? It depends on your arrangement with your manager, work load, and outside factors like customer appointments.";
            }
            else
            {
                message = "We have flexible working hours. The work amount depends on the role and your ability to prioritize tasks. ";
            }
            await context.PostAsync(message);
            context.Wait(this.MessageReceived);
        }
        
        //[LuisIntent("DressCode")]
        //public async Task DressCode(IDialogContext context, LuisResult result)
        //{
        //    string message = "We don’t have dress code. You are free to dress whatever you like. Most employees go with Business Casual when in the office. When we are meeting with a client, we just dress according to common sense.";
        //    await context.PostAsync(message);
        //    context.Wait(this.MessageReceived);
        //}

        //[LuisIntent("DressCode")]
        //public async Task DressCode(IDialogContext context, LuisResult result)
        //{
        //    string message = "We don’t have dress code. You are free to dress whatever you like. Most employees go with Business Casual when in the office. When we are meeting with a client, we just dress according to common sense.";
        //    await context.PostAsync(message);
        //    context.Wait(this.MessageReceived);
        //}

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
                else if (entities[0].Contains("phone") || entities[0].Contains("mobile") || entities[0].Contains("cell"))
                    message = "For most positions, you get a smartphone and a corporate phone number once you are hired. You can also use your own phone if you like.";
                else if (entities[0].Contains("laptop") || entities[0].Contains("pc") || entities[0].Contains("computer"))
                    message = "We have broad pool of laptops. Sometimes you get to choose, depending on your role. You can also use your own device, if you like.";
                else if (entities[0].Contains("food") || entities[0].Contains("lunch") || entities[0].Contains("dinner"))
                    message = "In some countries, you will get a lunch allowance every month for free to use in the Microsoft Cafeteria.";
                else if (entities[0].Contains("perk") || entities[0].Contains("benefit") || entities[0].Contains("social"))
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
                        message = "What kind of product are you interested in?";
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
                    await context.PostAsync("The Microsoft Academy for College Hires (MACH) is as an accelerated career development program designed to recruit and hire top-performing graduates across a broad range of roles, and aims to cultivate your talent utilizing training, mentoring, and community support.");
                else if (entities[0].Contains("intern"))
                    await context.PostAsync("At Microsoft, our interns work on projects that matter – and your team will rely on your skills and insights to help deliver those projects to market. You’ll get the opportunity to work on real projects and have fun along the way. This is your chance to show off your skills and work on cutting-edge technology. We offer internships in all job families and product areas. Imagine yourself as a Microsoft intern. Join Microsoft today, and help us shape the business of tomorrow.");
                else if (entities[0].Contains("no"))
                    await context.PostAsync("Don't worry, you can still apply for all the other roles at https://careers.microsoft.com/");
            }
            else
            {
                var message = context.MakeMessage();
                message.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                var attachment = new List<Attachment>();
                var actions = new List<CardAction>()
            {
                AttachmentsHelper.CreateCardAction("MACH Opportunities", "more info for mach", ActionTypes.PostBack),
                AttachmentsHelper.CreateCardAction("Internship Opportunities", "more info for intern", ActionTypes.PostBack),
                AttachmentsHelper.CreateCardAction("Non-Student Opportunities", "i want to know more but no-student", ActionTypes.PostBack)
            };
                var card = AttachmentsHelper.CreateHeroCardAttachment("What do you want to know?", "Click on the buttons below to learn more about the opportunities", null, null, actions);
                attachment.Add(card);
                message.Attachments = attachment;
                await context.PostAsync(message);
            }
            context.Wait(this.MessageReceived);
        }

        [LuisIntent("DurationInfo")]
        public async Task DurationInfo(IDialogContext context, LuisResult result)
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
                    await context.PostAsync("Usually about 2 years.");
                else if (entities[0].Contains("intern"))
                    await context.PostAsync("Internships last from 3 to 6 months, depending on the country and position.");
            }
            else
                await context.PostAsync("Our MACH and MACH MBA Programs usually last for 2 years and the internship programs last from 3 to 6 months.");
            context.Wait(this.MessageReceived);
        }

        [LuisIntent("Trainings")]
        public async Task Trainings(IDialogContext context, LuisResult result)
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
                    await context.PostAsync("The MACH program includes 3 to 4 trainings abroad and other local trainings about products, personal development, etc.");
                else if (entities[0].Contains("intern"))
                    await context.PostAsync("It strongly depends, but internship periods are usually very short to incluse international trainings. They can participate in all local trainings regarding aspects of licensing, products ussies, personal development. Also they have their personal development plan, which they concentrate on.");
            }
            else
                await context.PostAsync("Microsoft stimulates the interest of its employees in learning by sending them to International conferences regarding their role, paying for technical certifications or providing instructor-led trainings. Interns and MACHS have specific trainings on top of that.");
            context.Wait(this.MessageReceived);
        }

        [LuisIntent("InterviewProccess")]
        public async Task InterviewProccess(IDialogContext context, LuisResult result)
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
                    await context.PostAsync("First of all you apply via our Careers Site, answer a few questions about your background and share your CV. Then the strongest candidates have a chance to go through a test, video and Skype interview. The finalists are invited to the face-to-face assessment with Microsoft managers in the local Microsoft Office. Usually the selection process starts in March and is finished by Summer. So all the stages from the application to the final stage can take up to 4 months.");
                else if (entities[0].Contains("intern"))
                    await context.PostAsync("First of all you apply via our Careers Site, answer a few questions about your background and share your CV. Then the strongest candidates have a chance to go through a test, video and Skype interview. The finalists are invited to the face-to-face assessment with Microsoft managers in the local Microsoft Office. Usually the selection process starts in March or September. So all the stages from the application to the final stage can take up to 4 months.");
            }
            else
                await context.PostAsync("First of all you apply via our Careers Site, answer a few questions about your background and share your CV. Then the strongest candidates have a chance to go through a test, video and Skype interview. The finalists are invited to the face-to-face assessment with Microsoft managers in the local Microsoft Office. Usually the selection process starts in March and finished in May/June. So all the stages from the application to the final stage can take up to 4 months.");
            context.Wait(this.MessageReceived);
        }

        [LuisIntent("Criteria")]
        public async Task Criteria(IDialogContext context, LuisResult result)
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
                    await context.PostAsync("For the MACH Program you need to be a final year student or fresh university graduate. If you have an MBA, you are elidgible for the MACH MBA Program. The detailed list of requirement is available for each open role.  In brief, we are looking for a final year student or recent graduates (bachelors or Masters), motivated, bright, curious and young, with strong passion for Microsoft and good English skills.");
                else if (entities[0].Contains("intern"))
                    await context.PostAsync("For internshop positions, you need to be a student or fresh university graduate. The detailed list of requirement is available for each open role.  In brief, we are looking for a final year student or recent graduates (bachelors or Masters), motivated, bright, curious and young, with strong passion for Microsoft and good English skills.");
            }
            else
                await context.PostAsync("The detailed list of requirement is available for each open role.  In brief, we are looking for motivated, bright and curious young people with strong passion for Microsoft and  good English skills.");
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
                var message = context.MakeMessage();
                message.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                var attachment = new List<Attachment>();
                var actions = new List<CardAction>()
            {
                AttachmentsHelper.CreateCardAction("MACH Positions", "apply for mach", ActionTypes.PostBack),
                AttachmentsHelper.CreateCardAction("Internships Positions", "apply for intern", ActionTypes.PostBack),
                AttachmentsHelper.CreateCardAction("Non-Student Positions", "apply but no-student", ActionTypes.PostBack)
            };
                var card = AttachmentsHelper.CreateHeroCardAttachment("For what position are you interested in?", "Click on the buttons below to learn more about the opportunities", null, null, actions);
                attachment.Add(card);
                message.Attachments = attachment;
                await context.PostAsync(message);
            }
            context.Wait(this.MessageReceived);

        }

    }
}