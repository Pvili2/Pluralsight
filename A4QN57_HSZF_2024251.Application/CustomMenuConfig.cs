using ConsoleTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4QN57_HSZF_2024251.Application
{
    public class CustomMenuConfig
    {
        private string title;
        MenuConfig config;

        public CustomMenuConfig(string title)
        {
            this.Config = new MenuConfig()
            {
                Title = title,
                Selector = "-->",
                EnableBreadcrumb = true,
                SelectedItemForegroundColor = ConsoleColor.Blue,
                SelectedItemBackgroundColor = ConsoleColor.Black,
                WriteHeaderAction = () => { }
            };
            this.Title = title;
        }

        public string Title { get => title; set => title = value; }
        public MenuConfig Config { get => config; set => config = value; }
        
        public Action<MenuConfig> generateConfig()
        {
            return (config) => { }; 
        }
    }
}
