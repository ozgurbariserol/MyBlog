using Markdig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyBlog.Service.Helpers
{
    public class MarkDownHelper : IMarkDownHelper
    {

        public static string MarkDown(string content)
        {

            // URL'leri img tag'larına dönüştürmek için güncellenmiş Regex ve metod
            //var urlRegex = new Regex(@"\bhttps?:\/\/\S+\.(jpg|jpeg|png|gif)\b", RegexOptions.IgnoreCase);

            //// Regex.Replace ile doğrudan dönüşüm yapma
            //return urlRegex.Replace(content, m =>
            //{
            //    // URL içinde + karakterini kontrol et ve varsa kaldır
            //    string processedUrl = m.Value.Replace("+", "");
            //    // Eğer orijinal URL içinde + vardı ise display:inline, yoksa display:block kullan
            //    string displayStyle = m.Value.Contains("+") ? "inline-block" : "block";

            //    return $"<img src='{processedUrl}' alt='Image' style='width:auto; display:{displayStyle}; height:50%; max-width: 100%; margin: 5px;'>";
            //});

            var result =  Markdown.ToHtml($"{content}");
            return result;
        }
    }
}
