using System.ComponentModel;

namespace AzureCache.WebRole.Models.HomeModels
{
    public class IndexModel
    {
        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Value")]
        public string Value { get; set; }

        public IndexModel()
        {
            Name = "Tim";
        }
    }
}