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
       // private const string EntityGeographyCity = "builtin.geography.city";

        //private const string EntityHotelName = "Hotel";

        //private const string EntityAirportCode = "AirportCode";

       // private IList<string> titleOptions = new List<string> { "“Very stylish, great stay, great staff”", "“good hotel awful meals”", "“Need more attention to little things”", "“Lovely small hotel ideally situated to explore the area.”", "“Positive surprise”", "“Beautiful suite and resort”" };

        [LuisIntent("")]
        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            string message = $"Sorry, I did not understand '{result.Query}'. Type 'help' if you need assistance.";

            await context.PostAsync(message);

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("Greeting")]
        public async Task Greeting(IDialogContext context, LuisResult result)
        {
            string message = "Hey mate :)";

            await context.PostAsync(message);

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("IntroduceBot")]
        public async Task IntroduceBot(IDialogContext context, LuisResult result)
        {
             await context.PostAsync("I am an a bot intended to answer your questions concerning Microsoft internship and MACHs programs.");

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("Benefits")]
        [LuisIntent("BusinessTravel")]
        [LuisIntent("LanguageCheck")]
        [LuisIntent("SalaryInfo")]
        [LuisIntent("SalaryRaise")]
        [LuisIntent("ApplyForJob")]
        public async Task Benefits(IDialogContext context,  LuisResult result)
        {
            // var message = await activity;
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
            if (entities.Count>0)
            await context.PostAsync($"We are analyzing your message: '{intents[0]} {entities[0]}'...");
            else
                await context.PostAsync($"We are analyzing your message: '{intents[0]}'...");

            context.Wait(this.MessageReceived);

            //var hotelsQuery = new HotelsQuery();

            //EntityRecommendation cityEntityRecommendation;

            //if (result.TryFindEntity(EntityGeographyCity, out cityEntityRecommendation))
            //{
            //    cityEntityRecommendation.Type = "Destination";
            //}

            //var hotelsFormDialog = new FormDialog<HotelsQuery>(hotelsQuery, this.BuildHotelsForm, FormOptions.PromptInStart, result.Entities);

            //context.Call(hotelsFormDialog, this.ResumeAfterHotelsFormDialog);
        }

        //[LuisIntent("ShowHotelsReviews")]
        //public async Task Reviews(IDialogContext context, LuisResult result)
        //{
        //    EntityRecommendation hotelEntityRecommendation;

        //    if (result.TryFindEntity(EntityHotelName, out hotelEntityRecommendation))
        //    {
        //        await context.PostAsync($"Looking for reviews of '{hotelEntityRecommendation.Entity}'...");

        //        var resultMessage = context.MakeMessage();
        //        resultMessage.AttachmentLayout = AttachmentLayoutTypes.Carousel;
        //        resultMessage.Attachments = new List<Attachment>();

        //        for (int i = 0; i < 5; i++)
        //        {
        //            var random = new Random(i);
        //            ThumbnailCard thumbnailCard = new ThumbnailCard()
        //            {
        //                Title = this.titleOptions[random.Next(0, this.titleOptions.Count - 1)],
        //                Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris odio magna, sodales vel ligula sit amet, vulputate vehicula velit. Nulla quis consectetur neque, sed commodo metus.",
        //                Images = new List<CardImage>()
        //                {
        //                    new CardImage() { Url = "https://upload.wikimedia.org/wikipedia/en/e/ee/Unknown-person.gif" }
        //                },
        //            };

        //            resultMessage.Attachments.Add(thumbnailCard.ToAttachment());
        //        }

        //        await context.PostAsync(resultMessage);
        //    }

        //    context.Wait(this.MessageReceived);
        //}

        //[LuisIntent("Help")]
        //public async Task Help(IDialogContext context, LuisResult result)
        //{
        //    await context.PostAsync("Hi! Try asking me things like 'search hotels in Seattle', 'search hotels near LAX airport' or 'show me the reviews of The Bot Resort'");

        //    context.Wait(this.MessageReceived);
        //}

        //private IForm<HotelsQuery> BuildHotelsForm()
        //{
        //    OnCompletionAsyncDelegate<HotelsQuery> processHotelsSearch = async (context, state) =>
        //    {
        //        var message = "Searching for hotels";
        //        if (!string.IsNullOrEmpty(state.Destination))
        //        {
        //            message += $" in {state.Destination}...";
        //        }
        //        else if (!string.IsNullOrEmpty(state.AirportCode))
        //        {
        //            message += $" near {state.AirportCode.ToUpperInvariant()} airport...";
        //        }

        //        await context.PostAsync(message);
        //    };

        //    return new FormBuilder<HotelsQuery>()
        //        .Field(nameof(HotelsQuery.Destination), (state) => string.IsNullOrEmpty(state.AirportCode))
        //        .Field(nameof(HotelsQuery.AirportCode), (state) => string.IsNullOrEmpty(state.Destination))
        //        .OnCompletion(processHotelsSearch)
        //        .Build();
        //}

    //    private async Task ResumeAfterHotelsFormDialog(IDialogContext context, IAwaitable<HotelsQuery> result)
    //    {
    //        try
    //        {
    //            var searchQuery = await result;

    //            var hotels = await this.GetHotelsAsync(searchQuery);

    //            await context.PostAsync($"I found {hotels.Count()} hotels:");

    //            var resultMessage = context.MakeMessage();
    //            resultMessage.AttachmentLayout = AttachmentLayoutTypes.Carousel;
    //            resultMessage.Attachments = new List<Attachment>();

    //            foreach (var hotel in hotels)
    //            {
    //                HeroCard heroCard = new HeroCard()
    //                {
    //                    Title = hotel.Name,
    //                    Subtitle = $"{hotel.Rating} starts. {hotel.NumberOfReviews} reviews. From ${hotel.PriceStarting} per night.",
    //                    Images = new List<CardImage>()
    //                    {
    //                        new CardImage() { Url = hotel.Image }
    //                    },
    //                    Buttons = new List<CardAction>()
    //                    {
    //                        new CardAction()
    //                        {
    //                            Title = "More details",
    //                            Type = ActionTypes.OpenUrl,
    //                            Value = $"https://www.bing.com/search?q=hotels+in+" + HttpUtility.UrlEncode(hotel.Location)
    //                        }
    //                    }
    //                };

    //                resultMessage.Attachments.Add(heroCard.ToAttachment());
    //            }

    //            await context.PostAsync(resultMessage);
    //        }
    //        catch (FormCanceledException ex)
    //        {
    //            string reply;

    //            if (ex.InnerException == null)
    //            {
    //                reply = "You have canceled the operation.";
    //            }
    //            else
    //            {
    //                reply = $"Oops! Something went wrong :( Technical Details: {ex.InnerException.Message}";
    //            }

    //            await context.PostAsync(reply);
    //        }
    //        finally
    //        {
    //            context.Done<object>(null);
    //        }
    //    }

    //    private async Task<IEnumerable<Hotel>> GetHotelsAsync(HotelsQuery searchQuery)
    //    {
    //        var hotels = new List<Hotel>();

    //        // Filling the hotels results manually just for demo purposes
    //        for (int i = 1; i <= 5; i++)
    //        {
    //            var random = new Random(i);
    //            Hotel hotel = new Hotel()
    //            {
    //                Name = $"{searchQuery.Destination ?? searchQuery.AirportCode} Hotel {i}",
    //                Location = searchQuery.Destination ?? searchQuery.AirportCode,
    //                Rating = random.Next(1, 5),
    //                NumberOfReviews = random.Next(0, 5000),
    //                PriceStarting = random.Next(80, 450),
    //                Image = $"https://placeholdit.imgix.net/~text?txtsize=35&txt=Hotel+{i}&w=500&h=260"
    //            };

    //            hotels.Add(hotel);
    //        }

    //        hotels.Sort((h1, h2) => h1.PriceStarting.CompareTo(h2.PriceStarting));

    //        return hotels;
    //    }
    }
}