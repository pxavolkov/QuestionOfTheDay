using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace SoApi
{
    public class Config
    {
        private const string CategoryKeyName = "Category";

        public Config()
        {
            SelectedCategory = ConfigurationManager.AppSettings[CategoryKeyName];
            if (string.IsNullOrEmpty(SelectedCategory) || Categories.All(c => c != SelectedCategory))
            {
                SelectedCategory = Categories[0];
            }
        }

        public List<string> Categories = new List<string> {"c#", "javascript", "java", "python"};
        private string _selectedCategory;

        public string SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                try
                {
                    var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    var settings = configFile.AppSettings.Settings;
                    if (settings[CategoryKeyName] == null)
                    {
                        settings.Add(CategoryKeyName, value);
                    }
                    else
                    {
                        settings[CategoryKeyName].Value = value;
                    }
                    configFile.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
                }
                catch (ConfigurationErrorsException)
                {
                    //empty
                }
            }
        }
    }
}
