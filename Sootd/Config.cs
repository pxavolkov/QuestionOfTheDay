using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Sootd
{
    public class Config
    {
        public Config()
        {
            SelectedCategory = ConfigurationManager.AppSettings["Category"];
            if (string.IsNullOrEmpty(SelectedCategory) || Categories.All(c => c != SelectedCategory))
            {
                SelectedCategory = Categories[0];
            }
        }

        public List<string> Categories = new List<string> {"c#", "javascript", "java", "python"};
        
        public string SelectedCategory { get; set; }
    }
}
