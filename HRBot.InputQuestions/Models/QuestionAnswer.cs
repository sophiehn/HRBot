﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace BotInputQuestions.Models
{
    public class QuestionAnswer
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "question")]
        public string Question { get; set; }

        [JsonProperty(PropertyName = "answer")]
        public string Answer { get; set; }

        [JsonProperty(PropertyName = "topic")]
        public string Topic { get; set; }

        [JsonProperty(PropertyName = "alternatives")]
        public List<string> AlternativeQuestion { get; set; }

        [JsonProperty(PropertyName = "added")]
        public bool AddedToBot { get; set; }
    }
}