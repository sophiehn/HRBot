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

        [LuisIntent("ApplicationStatus")]
        public async Task ApplicationStatus(IDialogContext context, LuisResult result)
        {
            string message = "Once you apply, you will receive information about everyting. You are also welcome to contact us via  ceeuni@microsoft.com";
            await context.PostAsync(message);
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
                if (entities[0].Contains("mach"))
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

        [LuisIntent("AssessmentCenter")]
        public async Task AssessmentCenter(IDialogContext context, LuisResult result)
        {
            string message = "This is about a whole day event when final candidates go through various exercises and managers from the business assess them. Don't be nervous, come prepared, be ready to show your maximum. We’ll provide more advice once you reach this stage.   ";
            await context.PostAsync(message);
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
                if (entities[0].Contains("mach"))
                    await context.PostAsync("MACH program includes some trainings abroad. When it takes to another business trips it depends on the role and the territory where you are working on.");
                else
                    await context.PostAsync("Most employees have business or training trips, both domestic and international. It depends on the role and the territory where you are working on.");
            context.Wait(this.MessageReceived);

        }

        [LuisIntent("ChooseRole")]
        public async Task ChooseRole(IDialogContext context, LuisResult result)
        {
            string message = "We have a number of roles which are focused on specific technologies/products – from XBOX to Azure. Those roles might be quite diverse, from technical sales, consultant to marketer. You can chose any position which is available at that moment and relevant to your skills and needs.";
            await context.PostAsync(message);
            context.Wait(this.MessageReceived);
        }

        [LuisIntent("Competition")]
        public async Task Competition(IDialogContext context, LuisResult result)
        {
            string message = "Well, the competition is quite strong :) But don’t be afraid to try – anyway it is a useful experience. And make sure you do your best and stand out of the crowd during all the stages of the selection process. Then you have nothing to worry about";
            await context.PostAsync(message);
            context.Wait(this.MessageReceived);
        }

        [LuisIntent("Contact")]
        public async Task Contact(IDialogContext context, LuisResult result)
        {
            string message = "You can reach us by sending an email at ceeuni@microsoft.com";
            await context.PostAsync(message);
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
                if (entities[0].Contains("mach"))
                    await context.PostAsync("For the MACH Program you need to be a final year student or fresh university graduate. If you have an MBA, you are elidgible for the MACH MBA Program. The detailed list of requirement is available for each open role.  In brief, we are looking for a final year student or recent graduates (bachelors or Masters), motivated, bright, curious and young, with strong passion for Microsoft and good English skills.");
                else if (entities[0].Contains("intern"))
                    await context.PostAsync("For internshop positions, you need to be a student or fresh university graduate. The detailed list of requirement is available for each open role.  In brief, we are looking for a final year student or recent graduates (bachelors or Masters), motivated, bright, curious and young, with strong passion for Microsoft and good English skills.");
            }
            else
                await context.PostAsync("The detailed list of requirement is available for each open role.  In brief, we are looking for motivated, bright and curious young people with strong passion for Microsoft and  good English skills.");
            context.Wait(this.MessageReceived);
        }

        [LuisIntent("DressCode")]
        public async Task DressCode(IDialogContext context, LuisResult result)
        {
            string message = "We don’t have dress code. You are free to dress whatever you like. Most employees go with Business Casual when in the office. When we are meeting with a client, we just dress according to common sense.";
            await context.PostAsync(message);
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
                if (entities[0].Contains("mach"))
                    await context.PostAsync("Usually about 2 years.");
                else if (entities[0].Contains("intern"))
                    await context.PostAsync("Internships last from 3 to 6 months, depending on the country and position.");
            }
            else
                await context.PostAsync("Our MACH and MACH MBA Programs usually last for 2 years and the internship programs last from 3 to 6 months.");
            context.Wait(this.MessageReceived);
        }

        [LuisIntent("EmployeeRelationships")]
        public async Task EmployeeRelationships(IDialogContext context, LuisResult result)
        {
            string message = "It is not forbidden to have relationships between coworkers, until there is a conflict of interest. For example, it is not ideal to have close relationships between managers and their subordinates or have a relationship with a supplier/external partner.";
            await context.PostAsync(message);
            context.Wait(this.MessageReceived);
        }

        [LuisIntent("FreeTime")]
        public async Task FreeTime(IDialogContext context, LuisResult result)
        {
            string message = "During our free time, we have yoga lessons, volley-ball, foot-ball and other sports in the office,  depending on the country. A lot of colleagues enjoy running and climbing. Each of Microsoft employer is different and has a great interest in various hobbies.";
            await context.PostAsync(message);
            context.Wait(this.MessageReceived);
        }

        [LuisIntent("Goodbye")]
        public async Task Goodbye(IDialogContext context, LuisResult result)
        {
            string message = "Goodbye";
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
                if (entities[0].Contains("mach"))
                    await context.PostAsync("First of all you apply via our Careers Site, answer a few questions about your background and share your CV. Then the strongest candidates have a chance to go through a test, video and Skype interview. The finalists are invited to the face-to-face assessment with Microsoft managers in the local Microsoft Office. Usually the selection process starts in March and is finished by Summer. So all the stages from the application to the final stage can take up to 4 months.");
                else if (entities[0].Contains("intern"))
                    await context.PostAsync("First of all you apply via our Careers Site, answer a few questions about your background and share your CV. Then the strongest candidates have a chance to go through a test, video and Skype interview. The finalists are invited to the face-to-face assessment with Microsoft managers in the local Microsoft Office. Usually the selection process starts in March or September. So all the stages from the application to the final stage can take up to 4 months.");
            }
            else
                await context.PostAsync("First of all you apply via our Careers Site, answer a few questions about your background and share your CV. Then the strongest candidates have a chance to go through a test, video and Skype interview. The finalists are invited to the face-to-face assessment with Microsoft managers in the local Microsoft Office. Usually the selection process starts in March and finished in May/June. So all the stages from the application to the final stage can take up to 4 months.");
            context.Wait(this.MessageReceived);
        }

        [LuisIntent("Interviews")]
        public async Task Interviews(IDialogContext context, LuisResult result)
        {
            string message = "It can be from 1 to 4, depending on the position. You will definitely pass 1 online/skype interview if you are selected by our recruiters.";
            await context.PostAsync(message);
            context.Wait(this.MessageReceived);
        }

        [LuisIntent("IntroduceBot")]
        public async Task IntroduceBot(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("I am a Bot and I am here to help you with your questions concerning Microsoft Internship and MACHs programs. What do you want to know?");
            context.Wait(this.MessageReceived);
        }

        [LuisIntent("LanguageCheck")]
        public async Task LanguageCheck(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("I can speak only English. You should be able to do so if you are applying Microsoft ;)");
            context.Wait(this.MessageReceived);
        }

        [LuisIntent("MACHMBA")]
        public async Task MACHMBA(IDialogContext context, LuisResult result)
        {
            string message = "MACH MBA Info";
            await context.PostAsync(message);
            context.Wait(this.MessageReceived);
        }

        [LuisIntent("ManyRoles")]
        public async Task ManyRoles(IDialogContext context, LuisResult result)
        {
            string message = "You can apply for as many roles as you wish. But please be aware you might need to pass separate selection processes for each role. ";
            await context.PostAsync(message);
            context.Wait(this.MessageReceived);
        }

        [LuisIntent("MicrosoftRussiaInfo")]
        public async Task MicrosoftRussiaInfo(IDialogContext context, LuisResult result)
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
                if (entities[0].Contains("russia"))
                    await context.PostAsync("Microsoft Russia Info");
            }
            else
            {
                message = "Wanna know something specific? Just ask me :)";
                await context.PostAsync(message);
            }
            context.Wait(this.MessageReceived);
        }

        [LuisIntent("MicrosoftInfo")]
        public async Task MicrosoftInfo(IDialogContext context, LuisResult result)
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
                if (entities[0].Contains("russia"))
                    await context.PostAsync("Microsoft Russia info");
            }
            else
            {
                message = "Wanna know something specific? Just ask me :)";
                await context.PostAsync(message);
            }
            context.Wait(this.MessageReceived);
        }

        [LuisIntent("Misc")]
        public async Task Misc(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Is there anything else I can help you with? Ask me anything :)");
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
                if (entities[0].Contains("mach"))
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
                if (entities[0].Contains("food"))
                    await context.PostAsync("In the office you can always find fruit, soft drinks (Coca-Cola, Fanta, Sprite, various juices) and hot drinks like tea or coffee");
                else if (entities[0].Contains("xbox"))
                    await context.PostAsync("In some offices, even more than one ;)");
                else
                    await context.PostAsync($"You requested info for the Microsoft Office in {entities[0]}.");
            else
                await context.PostAsync("You requested info for the Microsoft Office.");
            context.Wait(this.MessageReceived);
        }

        [LuisIntent("OnlineTest")]
        public async Task OnlineTest(IDialogContext context, LuisResult result)
        {
            string message = "You will be asked to pass a situational strength-based test at first. it includes a number of cases you might face in your job with us. You will be asked to share your view how to react on this of that situation. The test doesn’t have right or wrong answers – it’s totally up to you want to choose and we would like to identify your natural approach, strategy and priorities in various situations. ";
            await context.PostAsync(message);
            context.Wait(this.MessageReceived);
        }

        [LuisIntent("PositionsOpen")]
        public async Task PositionsOpen(IDialogContext context, LuisResult result)
        {
            string message = "Usually we announce the new opportunities once or twice per year, in March or September. Internships usually start in September or January, depending on the processes and country. For MACH positions, it can be the same or random, since they are not so popular in all countries. ";
            await context.PostAsync(message);
            context.Wait(this.MessageReceived);
        }

        [LuisIntent("RoleSuggestion")]
        public async Task RoleSuggestion(IDialogContext context, LuisResult result)
        {
            string message = "Great to hear you have this experience! Please feel free to browse our site to explore what openings we have at the moment. Most our vacancies are in sales or business development, technical sales and marketing areas. We focus on hiring based mostly on your soft skills, motivation for Microsoft and desire to learn. So if you don’t have practical experience in specific area but you are willing to learn – feel free to apply!";
            await context.PostAsync(message);
            context.Wait(this.MessageReceived);
        }

        [LuisIntent("SalaryInfo")]
        public async Task SalaryInfo(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("At Microsoft we offer competitive salary level to our employees. The exact amount depends on many factors as skills level, role, location, professional growth, contribution to the team and some other factors. Some roles have only fix salary and yearly bonus. Many roles have also quarter profit sharings.");
            context.Wait(this.MessageReceived);
        }

        [LuisIntent("SalaryRaise")]
        public async Task SalaryRaise(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Your salary will be reviewed yearly, but we can not guarantee the growth as it depends on your professional growth, achievements, etc. ");
            context.Wait(this.MessageReceived);
        }

        [LuisIntent("Screening")]
        public async Task Screening(IDialogContext context, LuisResult result)
        {
            string message = "The first stage of the selection process is CV screening. We assess your eligibility for the program (if you meet the key criteria) as well as your educational and professional background, skills and motivation. Please make sure that your CV is up-to-date and relevant to the position you apply for. Don't forget to include all the relevant experience and skills you have as well as you that you show your motivation for the position you apply for.";
            await context.PostAsync(message);
            context.Wait(this.MessageReceived);
        }

        [LuisIntent("SkypeInterview")]
        public async Task SkypeInterview(IDialogContext context, LuisResult result)
        {
            string message = "You will be asked to have a Skype call with a recruiter.  In general, same as the video interview, be ready to talk in English, be relaxed and prepared and share your sincere thoughts! We’ll provide more advice once you reach this stage. ";
            await context.PostAsync(message);
            context.Wait(this.MessageReceived);
        }

        [LuisIntent("TechnologiesPriority")]
        public async Task TechnologiesPriority(IDialogContext context, LuisResult result)
        {
            string message = "It is quite difficult to provide a comprehensive list of all trending technologies and areas of focus, but  Microsoft is definitely obsessed with Cloud and Office. I am sure that you have heard about machine learning systems with chat bots and IoT sensors (internet of things), the power of cloud computing with Azure or new ways to visualize data with Power BI or Sway. Haven't you?";
            await context.PostAsync(message);
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
                if (entities[0].Contains("mach"))
                    await context.PostAsync("The MACH program includes 3 to 4 trainings abroad and other local trainings about products, personal development, etc.");
                else if (entities[0].Contains("intern"))
                    await context.PostAsync("It strongly depends, but internship periods are usually very short to incluse international trainings. They can participate in all local trainings regarding aspects of licensing, products ussies, personal development. Also they have their personal development plan, which they concentrate on.");
            }
            else
                await context.PostAsync("Microsoft stimulates the interest of its employees in learning by sending them to International conferences regarding their role, paying for technical certifications or providing instructor-led trainings. Interns and MACHS have specific trainings on top of that.");
            context.Wait(this.MessageReceived);
        }

        [LuisIntent("VideoInterview")]
        public async Task VideoInterview(IDialogContext context, LuisResult result)
        {
            string message = "You will have to give an online interview that you will record on your own, as video, where you will be answering a few questions. In general, be ready to talk in English, be relaxed and prepared! We’ll provide more advice once you reach this stage. ";
            await context.PostAsync(message);
            context.Wait(this.MessageReceived);
        }

        [LuisIntent("WorkAbroad")]
        public async Task WorkAbroad(IDialogContext context, LuisResult result)
        {
            string message = "Of course. Feel free to explore the vacancies we have across the globe and apply. Keep in mind that in most cases we are looking for candidates with fluent local language skills and work permits. ";
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

        [LuisIntent("Colleagues")]
        public async Task Colleagues(IDialogContext context, LuisResult result)
        {
            string message = "Colleagues Info";
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


    }
}