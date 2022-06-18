using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndividualTask.Models
{
    [Serializable]
    public class News : BaseClass
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Author { get; set; }
        public string Date { get; set; }
        public string Url { get; set; }
    }
}
