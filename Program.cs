using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace z23
{
    class Program
    {
        static readonly List<MyMenu> ListMenu = new List<MyMenu>();
        private static bool _block;
        public static SettingsZ23 Z23;
        private const string Json = "settings.json";

        static readonly StringBuilder Bodybuilder = new StringBuilder();
        private static readonly Stack<MyTag> Stack = new Stack<MyTag>();
        private static readonly StringBuilder Word = new StringBuilder();
        private static bool _w;

        static void Main(string[] args)
        {
            try
            {
                var bodyCore = BodyCore();
                Console.WriteLine(bodyCore);
            }
            catch (Exception e)
            {
                Console.WriteLine(ResourceCore.error);
                Console.WriteLine(e);
            }

            if (Z23.AutoClose == false)
            {
                Console.ReadKey();
            }
        }

        private static string BodyCore()
        {
            if (File.Exists(Json) == false)
            {
                Z23 = new SettingsZ23();
                string json = JsonConvert.SerializeObject(Z23, Formatting.Indented);
                File.WriteAllText(Json, json);
                Console.WriteLine(@"Внимание! Компиляция с настройками по умолчанию.");
            }
            else
            {
                try
                {
                    Z23 = JsonConvert.DeserializeObject<SettingsZ23>(File.ReadAllText(Json));
                    Console.WriteLine(@"Компиляция с настройками пользователя.");
                }
                catch (Exception e)
                {
                    Z23 = new SettingsZ23();
                    Console.WriteLine(
                        $@"Ошибка создания пользовательскиз настроек из файла{Environment.NewLine}{e} {Environment.NewLine} Внимание! Компиляция с настройками по умолчанию.");
                }

                string json = JsonConvert.SerializeObject(Z23, Formatting.Indented);
                File.WriteAllText(Json, json);
            }

            Utils.CheckFileStatic();

            var preCompiler = Utils.PreCompiler(Z23.FileNameTemplate);
            Console.WriteLine(preCompiler);
            Console.WriteLine(ResourceCore.Program_Main________________precompiler___________________);

            string assas = preCompiler.ToString();
            for (int ir = 0; ir < preCompiler.Length; ir++)
            {
                char c = preCompiler[ir];
                Bodybuilder.Append(c);

                if (c == '<')
                {
                    Word.Clear();
                    _w = true;
                }

                if (_w)
                {
                    Word.Append(c);
                }

                AddCharToBody(c);
                string asas = Word.ToString();

                if (Word.CompareWord("<menu".ToCharArray()) && _block == false)
                {
                    Stack.Push(new MyTag(TypeTag.Menu) {IsOpenTopTag = true});
                    Bodybuilder.RemoveFromEnd(Word);
                    ClearWord();
                    continue;
                }

                if (Word.CompareWord("<list".ToCharArray()) && _block == false)
                {
                    Stack.Push(new MyTag(TypeTag.List) {IsOpenTopTag = true});
                    Bodybuilder.RemoveFromEnd(Word);
                    ClearWord();
                    continue;
                }

                if (Word.CompareWord("<codes".ToCharArray()))
                {
                    Stack.Push(new MyTag(TypeTag.Code) {IsOpenTopTag = true});
                    Bodybuilder.RemoveFromEnd(Word);
                    _block = true;
                    ClearWord();
                    continue;
                }

                if (Word.CompareWord("</menu>".ToCharArray()) && _block == false)
                {
                    MyTag m = Stack.First();
                    Bodybuilder.RemoveFromEnd(Word);
                    Bodybuilder.Append(m.GetCloseTag());
                    Stack.Pop();
                    ClearWord();
                    continue;
                }

                if (Word.CompareWord("</list>".ToCharArray()) && _block == false)
                {
                    MyTag m = Stack.First();
                    Bodybuilder.RemoveFromEnd(m.BodyBuilder);
                    m.BodyBuilder.RemoveFromEnd(Word);
                    Bodybuilder.Append(m.GetBody());
                    Bodybuilder.Append(m.GetCloseTag());
                    ClearWord();
                    continue;
                }

                if (Word.CompareWord("</codes>".ToCharArray()))
                {
                    MyTag m = Stack.First();
                    //string ass = Bodybuilder.ToString();
                    //string asas = m.BodyBuilder.ToString();
                    Bodybuilder.Replace(m.BodyBuilder.ToString(), "");

                    Bodybuilder.Append(m.GetBody());
                    Bodybuilder.Append(m.GetCloseTag());
                    string asss = Bodybuilder.ToString();
                    Stack.Pop();
                    _block = false;
                    ClearWord();
                    continue;
                }

                if (c == '>')
                {
                    if (Stack.Any() == false) continue;
                    MyTag m = Stack.First();
                    if (Bodybuilder[Bodybuilder.Length - 2] == '/')
                    {
                        m.IsOpenTopTag = false;
                        m.IsOpenBody = true;
                        _w = false;
                        int y = m.ATributesBuilder.Length;
                        Bodybuilder.RemoveFromEnd(m.ATributesBuilder);
                        if (y > 2) m.ATributesBuilder.Remove(y - 2, 2);
                        switch (m.TypeTag)
                        {
                            case TypeTag.None:
                                break;
                            case TypeTag.Menu:
                                Bodybuilder.Append(m.GetOpenTag());
                                Bodybuilder.Append(m.GetCloseTag());
                                ListMenu.Add(MyMenu.GetMyMenu(m));
                                break;
                            case TypeTag.Code:
                                Bodybuilder.Append(m.GetOpenTag());
                                Bodybuilder.Append(m.GetCloseTag());
                                break;
                            case TypeTag.List:
                                Bodybuilder.Append(m.GetOpenTag());
                                Bodybuilder.Append(m.GetCloseTag());
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }

                        Stack.Pop();

                        ClearWord();
                        continue;
                    }

                    if (Bodybuilder[Bodybuilder.Length - 2] != '/')
                    {
                        if (m.IsOpenTopTag)
                        {
                            m.IsOpenTopTag = false;
                            m.IsOpenBody = true;
                            _w = false;
                            Word.Length = 0;
                            int y = m.ATributesBuilder.Length;
                            Bodybuilder.RemoveFromEnd(m.ATributesBuilder);
                            m.ATributesBuilder.Remove(y - 1, 1);
                            switch (m.TypeTag)
                            {
                                case TypeTag.None:
                                    break;
                                case TypeTag.Menu:
                                    Bodybuilder.Append(m.GetOpenTag());
                                    ListMenu.Add(MyMenu.GetMyMenu(m));
                                    break;
                                case TypeTag.Code:
                                    Bodybuilder.Append(m.GetOpenTag());
                                    break;
                                case TypeTag.List:
                                    Bodybuilder.Append(m.GetOpenTag());
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }

                            ClearWord();
                        }
                    }
                }
            }

            Word.Clear();

            for (int i = 0; i < Bodybuilder.Length; i++)
            {
                char c = Bodybuilder[i];

                Word.Append(c);
            }

            string[] s = Word.ToString()
                .Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            Word.Clear();
            var addP = true;
            foreach (string s1 in s)
            {
                if (s1.Contains("<pre"))
                {
                    addP = false;
                }

                if (s1.Contains("</pre>"))
                {
                    addP = true;
                }

                if (s1.StartWitchOpenTag())
                {
                    Word.AppendLine(s1.Trim());
                }
                else
                {
                    if (addP)
                    {
                        Word.AppendLine(s1.TrimEnd().Replace(" ", Z23.Space) + "<br>");
                    }
                    else
                    {
                        Word.AppendLine(s1.TrimEnd());
                    }
                }
            }

            string html = Word.Replace("#####", string.Empty).ToString();

            string bodyCore = ResourceCore.innerindex;

            bodyCore = bodyCore.Replace("###menu###", Utils.BuildMenu(ListMenu));

            bodyCore = bodyCore.Replace("###body###", html);
            File.WriteAllText("index.html", bodyCore);
            System.Diagnostics.Process.Start("index.html");
            return bodyCore;
        }

        private static void AddCharToBody(char c)
        {
            if (Stack.Any())
            {
                if (Stack.First().IsOpenTopTag)
                {
                    Stack.First().ATributesBuilder.Append(c);
                }

                if (Stack.First().IsOpenBody)
                {
                    Stack.First().BodyBuilder.Append(c);
                }
            }
        }

        static void ClearWord()
        {
            Word.Clear();
            _w = false;
        }
    }
}