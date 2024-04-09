﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using MagicQuizDesktop.Models;
//
//    var user = User.FromJson(jsonString);

namespace MagicQuizDesktop.Models
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class User
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("avatar")]
        public string Avatar { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("is_active")]
        public long IsActive { get; set; }

        [JsonProperty("is_admin")]
        public long IsAdmin { get; set; }

    }

    public partial class User
    {
        public static User FromJson(string json) => JsonConvert.DeserializeObject<User>(json, MagicQuizDesktop.Models.Converter.Settings);
    }
}
