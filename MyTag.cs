using System;
using System.Collections.Generic;
using System.Text;

namespace z23
{
    enum TypeTag
    {
        None,Menu,Code
    }

    class MyTag
    {
        public TypeTag TypeTag { get; set; }
        public bool IsOpenTopTag { get; set; }
        public bool IsOpenBody { get; set; }
        public StringBuilder ATributesBuilder { get; set; } = new StringBuilder();
        public StringBuilder BodyBuilder { get; set; } = new StringBuilder();
        public Dictionary<string,string> DictionaryAttribute=new Dictionary<string, string>();

        public string GetOpenTag()
        {
            RefreshAttribute();

            switch (TypeTag)
            {
                
                case TypeTag.None:
                    break;
                case TypeTag.Menu:
                {
                    string text=string.Empty;
                    if (DictionaryAttribute.ContainsKey("text"))
                    {
                        text= DictionaryAttribute["text"];
                    }
                     
                    StringBuilder builder=new StringBuilder();
                    foreach (var pair in DictionaryAttribute)
                    {
                        if(pair.Key=="text") continue;
                        builder.Append($" {pair.Key} = \"{pair.Value}\" ");
                    }
                    string t = $"<h3 {builder} >{text}</h3>";
                    return t;
                }

                case TypeTag.Code:
                {
                    string @class = "class=\"language-csharp\"";
                    StringBuilder builder = new StringBuilder();
                    foreach (var pair in DictionaryAttribute)
                    {
                        if (pair.Key == "class")
                        {
                            @class = $"class=\"{pair.Value}\"";
                            continue;
                        }
                        builder.Append($" {pair.Key} = \"{pair.Value}\" ");
                    }
                    string t = $"<pre><code {@class} {builder} >";
                        return t;
                    }
                  
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return "444444444444444444444";
        }

        public string GetCloseTag()
        {
            switch (TypeTag)
            {
                case TypeTag.None:
                    return String.Empty;
                case TypeTag.Menu:
                    return string.Empty;
                case TypeTag.Code:
                    return "</code></pre>";
                default:
                    throw new ArgumentOutOfRangeException();
            }
           
        }

        private void RefreshAttribute()
        {
            string[] s = ATributesBuilder.ToString().Split(new[] { ' ', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s1 in s)
            {
                string[] sd = s1.Split(new[] { '=', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                if (sd.Length != 2) continue;
                DictionaryAttribute.Add(sd[0].Trim().Trim('"'),sd[1].Trim().Trim('"'));
            }
        }
        
    }
}