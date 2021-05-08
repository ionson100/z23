using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace z23
{
    public enum TypeTag
    {
        None,
        Menu,
        Code,
        List
    }

    public class MyTag
    {
        private static int _innerid;
        public int Id { get; }
        public TypeTag TypeTag { get; }
        public bool IsOpenTopTag { get; set; }
        public bool IsOpenBody { get; set; }
        public StringBuilder ATributesBuilder { get; } = new StringBuilder();
        public StringBuilder BodyBuilder { get; set; } = new StringBuilder();
        private readonly StringBuilder _workerBuilder = new StringBuilder();
        public Dictionary<string, string> DictionaryAttribute = new Dictionary<string, string>();

        private static readonly string MenuContainerTagOpen =
            $"<{Program.Z23.Menu.Container.Tag} {Program.Z23.Menu.Container.UserAttribute} > ";

        private static readonly string MenuContainerTagClose = $"</{Program.Z23.Menu.Container.Tag}>";


        public MyTag(TypeTag typeTag)
        {
            TypeTag = typeTag;
            if (typeTag == TypeTag.Menu)
            {
                Id = ++_innerid;
            }
        }

        public string GetOpenTag()
        {
            RefreshAttribute();

            switch (TypeTag)
            {
                case TypeTag.None:
                    break;
                case TypeTag.Menu:
                {
                    string text = string.Empty;
                    if (DictionaryAttribute.ContainsKey("text"))
                    {
                        text = DictionaryAttribute["text"];
                    }

                    _workerBuilder.Clear();
                    foreach (var pair in DictionaryAttribute)
                    {
                        if (pair.Key == "text") continue;
                        _workerBuilder.Append($" {pair.Key} = \"{pair.Value}\" ");
                    }

                    string atrMenu = Program.Z23.Menu.UserAttribute + _workerBuilder;
                    string container = $"{Environment.NewLine}{MenuContainerTagOpen}";
                    if (Program.Z23.Menu.UsageContainer == false)
                    {
                        container = string.Empty;
                    }

                    string t =
                        $"<{Program.Z23.Menu.Tag} {atrMenu} id=\"{Id}\">{text}</{Program.Z23.Menu.Tag}>{container}";
                    return t;
                }

                case TypeTag.Code:
                {
                    _workerBuilder.Clear();
                    foreach (var pair in DictionaryAttribute)
                    {
                        _workerBuilder.Append($" {pair.Key} = \"{pair.Value}\" ");
                    }

                    string atr =
                        $"{_workerBuilder}{Program.Z23.Code.UserAttribute}";
                    string t = $"<pre{Program.Z23.Code.PreUserAttribute}\"><code {atr} {_workerBuilder} >";
                    return t;
                }

                case TypeTag.List:
                {
                    _workerBuilder.Clear();
                    foreach (var pair in DictionaryAttribute)
                    {
                        _workerBuilder.Append($" {pair.Key} = \"{pair.Value}\" ");
                    }

                    return $"<{Program.Z23.List.Tag}{_workerBuilder}{Program.Z23.List.UserAttribute}>";
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
                    if (Program.Z23.Menu.UsageContainer)
                    {
                        return $"{MenuContainerTagClose}";
                    }

                    return string.Empty;

                case TypeTag.Code:
                    return "</code></pre>";
                case TypeTag.List:
                    return $"</{Program.Z23.List.Tag}{Program.Z23.List.UserAttribute}>";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void RefreshAttribute()
        {
            if (DictionaryAttribute.Count > 0) return;

            Regex myRegex1 =
                new Regex(
                    "((?:(?!\\s|=).)*)\\s*?=\\s*?[\"']?((?:(?<=\")(?:(?<=\\\\)\"|[^\"])*|(?<=')(?:(?<=\\\\)'|[^'])*)|(?:(?!\"|')(?:(?!\\/>|>|\\s).)+))");
            var m1 = myRegex1.Match(ATributesBuilder.ToString());
            while (m1.Success)
            {
                string[] str = m1.Value.Split('=');
                if (str.Length == 2)
                {
                    DictionaryAttribute.Add(str[0].Replace("\"", string.Empty).Trim(),
                        str[1].Replace("\"", string.Empty).Trim());
                }

                m1 = m1.NextMatch();
            }
        }

        public StringBuilder GetBody()
        {
            switch (TypeTag)
            {
                case TypeTag.None:
                    break;
                case TypeTag.Menu:
                    break;
                case TypeTag.Code:
                    return BodyBuilder.RemoveFromEnd("</codes>").Replace("<", "&lt;").Replace(">", "&gt;");
                case TypeTag.List:
                {
                    RefreshAttribute();

                    _workerBuilder.Clear();
                    string[] s = BodyBuilder.ToString().Split(Environment.NewLine.ToCharArray(),
                        StringSplitOptions.RemoveEmptyEntries);


                    string t = string.Empty;
                    if (DictionaryAttribute.ContainsKey("type"))
                    {
                        t = $" type=\"{DictionaryAttribute["type"]}\"";
                    }

                    string atr =
                        $"{t}{Program.Z23.List.Item.UserAttribute}";

                    foreach (var s1 in s)
                    {
                        if(string.IsNullOrEmpty(s1.Trim())) continue;
                        _workerBuilder.Append($"<li{atr}>{s1.Trim()}</li>{Environment.NewLine}");
                    }

                    return _workerBuilder;
                }

                default:
                    throw new ArgumentOutOfRangeException();
            }

            return _workerBuilder.Clear().Append(";;;;;;;;;;;;;;;;;");
        }
    }
}