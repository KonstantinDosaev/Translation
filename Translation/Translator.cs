using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Translation
{
    internal class Translator
    {
        private static bool _enOrRus;
        
        public static async Task<string?> Translate(string clipboardText)
        {
            
            GetTranslateDirection(clipboardText);
            var toLanguage = (_enOrRus) ? "ru" : "en";
            var fromLanguage = (_enOrRus) ? "en" : "ru";
            var url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl=" +
                      $"{fromLanguage}&tl={toLanguage}&dt=t&q={HttpUtility.UrlEncode(clipboardText)}";
            var httpClient = new HttpClient();
            var result = await httpClient.GetStringAsync(url); 

            try
            {
                return result.Substring(4, result.IndexOf("\"", 4, StringComparison.Ordinal) - 4);
            }
            catch
            {
                return default;
            }
        }

        private static void GetTranslateDirection(string clipboardText)
        {
          
            var bRus = 0;
            var bEn = 0;
            
            var bSuccess = (clipboardText.Length < 5) ? (byte)clipboardText.Length : (byte)5;

            Debug.Assert(clipboardText != null, nameof(clipboardText) + " != null");
            foreach (var c in clipboardText.ToUpper())
            {

                if (c is >= 'А' and <= 'Я') bRus++;
                else if (c is >= 'A' and <= 'Z') bEn++;
    
                if (bRus == bSuccess) { _enOrRus = false; break; }
                else if (bEn == bSuccess) { _enOrRus = true; break; }
            }
        }
    }
}
