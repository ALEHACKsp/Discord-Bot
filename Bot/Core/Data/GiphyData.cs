using System.Collections.Generic;
namespace DPP_Bot.Core.Data
{
    public class GiphyData
    {
        public List<Data> data { get; set; }
    }

    public class Data
    {
        public Images images { get; set; }
    }

    public class Images
    {
        public Original original { get; set; }
    }

    public class Original
    {
        public string url { get; set; }
    }

}
