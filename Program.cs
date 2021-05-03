using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace z23
{
    class Program
    {
        private static bool block;
        static StringBuilder bodybuilder = new StringBuilder();
        private static Stack<MyMeny> stack = new Stack<MyMeny>();

        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("templateindex.xml");
            StringBuilder word = new StringBuilder();
            bool w = false;
            while (!sr.EndOfStream)
            {
                char c = (char)sr.Read();
                bodybuilder.Append(c);
               
                if (c == '<')
                {
                    word.Clear();
                    w = true;
                }

                if (w)
                {
                    word.Append(c);
                }

                if (stack.Any())
                {
                    if (stack.First().IsOpenTopTag)
                    {
                        stack.First().ATributesBuilder.Append(c);
                    }

                    if (stack.First().isOpenBody)
                    {
                        stack.First().BodyBuilder.Append(c);
                    }
                }
               

                switch (c)
                {
                    case '>':
                        {
                            if (stack.Any() == false)
                            {
                                break;
                            }
                            MyMeny m = stack.First();

                            if (bodybuilder[bodybuilder.Length - 1] != '/')
                            {

                                if (m.IsOpenTopTag)
                                {
                                    m.IsOpenTopTag = false;
                                    m.isOpenBody = true;
                                    w = false;
                                    word.Length = 0;
                                    int y = m.ATributesBuilder.Length;
                                    bodybuilder.Remove(bodybuilder.Length - y, y);
                                    m.ATributesBuilder.Remove(y - 1, 1);
                                    switch (m.TypeTag)
                                    {
                                        case TypeTag.None:
                                            break;
                                        case TypeTag.Menu:
                                            bodybuilder.Append("menu***menu");
                                            break;
                                        case TypeTag.Code:
                                            bodybuilder.Append("code***code");
                                            break;
                                        default:
                                            throw new ArgumentOutOfRangeException();
                                    }
                                   

                                }
                                else
                                {
                                    string dd = word.ToString();
                                    if (word.CompareWord("</menu>".ToCharArray()) && block==false) 
                                    {
                                        bodybuilder.Remove(bodybuilder.Length - m.BodyBuilder.Length, m.BodyBuilder.Length);
                                        string cc = bodybuilder.ToString();
                                        m.BodyBuilder.Remove(m.BodyBuilder.Length - word.Length, word.Length);
                                        string xx = m.BodyBuilder.ToString();
                                        bodybuilder.Append(Environment.NewLine).Append("<diw>");
                                        bodybuilder.Append(m.BodyBuilder).Append("</div>");
                                        stack.Pop();
                                    }
                                    if (word.CompareWord("</codes>".ToCharArray()))
                                    {
                                        bodybuilder.Remove(bodybuilder.Length - "</codes>".Length, "</codes>".Length);
                                        bodybuilder.Append("**finishCode***");
                                        stack.Pop();
                                        block = false;
                                    }
                                }

                                break;
                            }

                            if (bodybuilder[bodybuilder.Length - 1] == '/')
                            {
                                m.IsOpenTopTag = false;
                                m.isOpenBody = true;
                                w = false;
                                word.Length = 0;
                                int y = m.ATributesBuilder.Length;
                                bodybuilder.Remove(bodybuilder.Length - y, y);
                                m.ATributesBuilder.Remove(y - 1, 1);
                                bodybuilder.Append("menu***menu");
                                stack.Pop();
                            }



                            break;
                        }
                    case ' ':
                        {
                            w = false;
                            CheckWord(word);
                            break;
                        }
                    case '\r':
                        {
                            w = false;
                            CheckWord(word);
                            break;
                        }
                }

            }
            Console.WriteLine(bodybuilder);
            Console.WriteLine("******************attribute********************");
            // Console.WriteLine(stack.First()?.ATributesBuilder);
            Console.WriteLine("******************body********************");
            // Console.WriteLine(stack.First()?.BodyBuilder);
            Console.ReadKey();
        }

        static void CheckWord(StringBuilder word)
        {
            if (word.Length == 0)
            {
                return;
            }
            if (word.CompareWord("<menu".ToCharArray()) && block == false)
            {
                MyMeny meny = new MyMeny();
                meny.TypeTag = TypeTag.Menu;
                meny.IsOpenTopTag = true;
                //meny.ATributesBuilder.Append(" <menu ");
                stack.Push(meny);
                int r = bodybuilder.Length;
                int y = word.Length;
                bodybuilder.Remove(r - y, y);
                word.Clear();
            }

            if (word.CompareWord("<codes".ToCharArray()))
            {
              
                MyMeny meny = new MyMeny();
                meny.TypeTag = TypeTag.Code;
                meny.IsOpenTopTag = true;
                //meny.ATributesBuilder.Append(" <menu ");
                stack.Push(meny);
                int r = bodybuilder.Length;
                int y = word.Length;
                bodybuilder.Remove(r - y, y);
                word.Clear(); 
                block = true;
            }
            





        }

        private static void NewMethod()
        {
            StreamReader sr = new StreamReader("templateindex.xml");
            while (!sr.EndOfStream)
            {
                char c = (char)sr.Read();
                bodybuilder.Append(c);
                switch (c)
                {
                    case '<':
                        {
                            int f = bodybuilder.Length - 1;
                            if ((f) >= 0)
                            {
                                if (bodybuilder[f - 1] == '\n')
                                {
                                    bodybuilder.Replace(Environment.NewLine, " ", f - 1, 2);
                                }
                                else if (bodybuilder[f - 1] != ' ')
                                {
                                    string sddd = bodybuilder[f].ToString();
                                    bodybuilder.Insert(f, $"{Environment.NewLine} ");
                                }
                            }

                            break;
                        }
                    case '>':
                        {
                            while (true)
                            {
                                int f = bodybuilder.Length - 2;
                                if ((f) >= 0)
                                {
                                    if (bodybuilder[f] == '\n')
                                    {
                                        string sddd = bodybuilder[f].ToString();
                                        bodybuilder.Replace(Environment.NewLine, " ", f - 1, 2);
                                    }
                                    else if (bodybuilder[f] == ' ')
                                    {
                                        string sddd = bodybuilder[f].ToString();
                                        bodybuilder.Remove(f, 1);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }

                            break;
                        }
                    case '\r':
                        {
                            break;
                        }
                    case '\n':
                        {
                            break;
                        }
                    default:
                        {
                            int f = bodybuilder.Length - 2;
                            if ((f) >= 0)
                            {
                                string sss = bodybuilder.ToString();

                                string dddd = bodybuilder[f].ToString();
                                if (bodybuilder[f] == '>')
                                {
                                    bodybuilder.Insert(f + 1, $"{Environment.NewLine} ");
                                }
                            }

                            break;
                        }
                }
            }

            Console.WriteLine(bodybuilder);
            Console.ReadKey();
        }

        private static void CheckWord()
        {
            Console.WriteLine("*****  " + bodybuilder.GetWord());
        }
    }


    static class Helper
    {
        private static StringBuilder Builder = new StringBuilder();
        public static StringBuilder GetWord(this StringBuilder builder)
        {
            for (int i = builder.Length - 2; i >= 0; i--)
            {
                if (builder[i] == ' ')
                {
                    return Builder;
                }

                Builder.Append(builder[i]);
            }

            return Builder;

        }

        public static bool CompareWord(this StringBuilder builder, char[] word)
        {
            if (builder.Length == 0) return false;
            if (builder.Length < word.Length) return false;
            bool result = false;
            for (var i = 0; i < word.Length; i++)
            {
                if (builder[i] == word[i])
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }

            return result;
        }

    }
}
