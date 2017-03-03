﻿using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using LuisBot.Services;
using System.Web.Configuration;
using System.Diagnostics;
using Microsoft.Bot.Builder.Dialogs;

namespace HRBot.Client
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        private readonly BingSpellCheckService spellService = new BingSpellCheckService();

        private static readonly bool IsSpellCorrectionEnabled = Boolean.Parse(WebConfigurationManager.AppSettings["IsSpellCorrectionEnabled"]);

        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {

                ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                if (activity.Text.Equals("reset", StringComparison.OrdinalIgnoreCase))
                {
                    BotState state = activity.GetStateClient().BotState as BotState;
                    state.DeleteStateForUser(activity.ChannelId, activity.From.Id);
                }

                if (IsSpellCorrectionEnabled)
                {
                    try
                    {
                        activity.Text = await this.spellService.GetCorrectedTextAsync(activity.Text);
                    }
                    catch (Exception ex)
                    {
                        Trace.TraceError(ex.ToString());
                    }
                }

                await Conversation.SendAsync(activity, () => new RootDialog());
                
               // await Conversation.SendAsync(activity, () => new RootLuisDialog());
              
            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}