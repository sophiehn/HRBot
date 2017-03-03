using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRBot.Client
{

    public class AttachmentsHelper
    {
        public static Attachment CreateHeroCardAttachment(string actionTitle, string actionText,
         string actionSubtitle, string picURL, List<CardAction> actions)
        {
            var card = new HeroCard
            {
                Title = actionTitle,
            };
            if (actionText != null)
                card.Text = actionText;
            if (actionSubtitle != null)
                card.Subtitle = actionSubtitle;
            if (picURL != null)
                card.Images = new List<CardImage>
                         {
                                new CardImage
                                {
                                    Url = picURL
                                }
                         };
            card.Buttons = new List<CardAction>(actions);
            return card.ToAttachment();
        }

        public static CardAction CreateCardAction(string title, string value, string type)
        {
            return new CardAction()
            {
                Title = title,
                Value = value,
                Type = type
            };
        }

        public static Attachment CreateThumbnailCardAttachment(string actionTitle, string actionText,
         string actionSubtitle, string picURL)
        {
            HeroCard thumbnailCard = new HeroCard();
            thumbnailCard.Title = actionTitle;
            if (actionSubtitle != null)
                thumbnailCard.Subtitle = actionSubtitle;
            if (actionText != null)
                thumbnailCard.Text = actionText;
            if (picURL != null)
                thumbnailCard.Images = new List<CardImage>()
                        {
                            new CardImage() { Url = picURL }
                        };
            return thumbnailCard.ToAttachment();
        }
    }
}