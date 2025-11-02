
namespace game.resource.dataController
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial struct FileList
    {
        public long size;
        public string path;
    }
    public partial class Model
    {
        [JsonProperty("file.list")]
        public List<FileList> FileList { get; set; }
        public static Model FromJson(string json) => JsonConvert.DeserializeObject<Model>(json);
    }
}
