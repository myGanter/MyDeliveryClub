using System.IO;
using System.Text;
using StajAppCore.Models;
using Microsoft.AspNetCore.Html;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace StajAppCore.Helpers
{
    public static class MyHTMLHelpers
    {
        public static string RootPath = "";

        public static HtmlString BuildMenu(this IHtmlHelper html, IEnumerable<MenuItem> menyItems)
        {
            StringBuilder result = new StringBuilder();

            result.Append("<ul class=\"navbar-nav mr-auto\">");

            if (menyItems != null)
                foreach (var i in menyItems)
                {
                    result.Append($"<li class=\"nav-item\"><a href=\"#\" v-on:click=\"swichView('{i.Url}')\" class=\"nav-link tabs-collor\">{i.Name}</a></li>");
                    if (i.LowMenuItem != null)
                        result.Append(BuildMenu(html, i.LowMenuItem).ToString()); 
                }

            result.Append("</ul>");
            return new HtmlString(result.ToString());
        }

        private static Dictionary<string, string> VueTemplaits = new Dictionary<string, string>();        
        public static HtmlString VueRender(this IHtmlHelper html, string dirName)
        {
            if (VueTemplaits.ContainsKey(dirName))
                return new HtmlString(VueTemplaits[dirName]);

            StringBuilder sB = new StringBuilder();
            if (Directory.Exists($"{RootPath}//Vue//Templaits//{dirName}"))
            {
                string[] files = Directory.GetFiles($"{RootPath}//Vue//Templaits//{dirName}", "*.vue");

                for (int i = 0; i < files.Length; i++)
                    sB.Append(File.ReadAllText(files[i]));

                if (files.Length > 0)
                    VueTemplaits.Add(dirName, sB.ToString());
            }
            
            return new HtmlString(sB.ToString());
        }
    }
}
