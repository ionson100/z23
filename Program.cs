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
        private static bool _block;
        static readonly StringBuilder Bodybuilder = new StringBuilder();
        private static readonly Stack<MyTag> Stack = new Stack<MyTag>();
        static readonly StringBuilder Word = new StringBuilder(); 
        static void Main(string[] args)
        {
            var preCompiler= Utils.PreCompiler("templateindex.xml");
            Console.WriteLine(preCompiler);
            Console.WriteLine("***************precompiler*******************");
           
            bool w = false;
            for (int ir = 0; ir < preCompiler.Length; ir++)
            {
                char c = preCompiler[ir];
                Bodybuilder.Append(c);
               
                if (c == '<')
                {
                    Word.Clear();
                    w = true;
                }

                if (w)
                {
                    Word.Append(c);
                }

                AddCharToBody(c);
               
                string sddd = Bodybuilder.ToString();
                string sdsd = Word.ToString();

                switch (c)
                {
                       
                    case '>':
                    {

                       
                        
                            if (Stack.Any() == false)
                            {
                                break;
                            }
                            MyTag m = Stack.First();

                            if (Bodybuilder[Bodybuilder.Length - 1] != '/')
                            {

                                if (m.IsOpenTopTag)
                                {
                                    m.IsOpenTopTag = false;
                                    m.IsOpenBody = true;
                                    w = false;
                                    Word.Length = 0;
                                    int y = m.ATributesBuilder.Length;
                                    Bodybuilder.Remove(Bodybuilder.Length - y, y);
                                    m.ATributesBuilder.Remove(y - 1, 1);
                                    switch (m.TypeTag)
                                    {
                                        case TypeTag.None:
                                            break;
                                        case TypeTag.Menu:
                                            Bodybuilder.Append(m.GetOpenTag());
                                            break;
                                        case TypeTag.Code:
                                            Bodybuilder.Append(m.GetOpenTag());
                                            break;
                                        default:
                                            throw new ArgumentOutOfRangeException();
                                    }
                                   

                                }
                                else
                                {
                                    string dd = Word.ToString();
                                    if (Word.CompareWord("</menu>".ToCharArray()) && _block==false) 
                                    {
                                        Bodybuilder.Remove(Bodybuilder.Length - m.BodyBuilder.Length, m.BodyBuilder.Length);
                                        string cc = Bodybuilder.ToString();
                                        m.BodyBuilder.Remove(m.BodyBuilder.Length - Word.Length, Word.Length);
                                        string xx = m.BodyBuilder.ToString();
                                        Bodybuilder.Append(Environment.NewLine).Append("<diw>");
                                        Bodybuilder.Append(m.BodyBuilder).Append("</div>");
                                        Stack.Pop();
                                    }

                                    string sdd = Word.ToString();
                                    if (Word.CompareWord("</codes>".ToCharArray()))
                                    {
                                        Bodybuilder.Remove(Bodybuilder.Length - "</codes>".Length, "</codes>".Length);
                                        Bodybuilder.Append(m.GetCloseTag());
                                        Stack.Pop();
                                        _block = false;
                                    }
                                }

                                break;
                            }

                            if (Bodybuilder[Bodybuilder.Length - 1] == '/')
                            {
                                m.IsOpenTopTag = false;
                                m.IsOpenBody = true;
                                w = false;
                                Word.Length = 0;
                                int y = m.ATributesBuilder.Length;
                                Bodybuilder.Remove(Bodybuilder.Length - y, y);
                                m.ATributesBuilder.Remove(y - 1, 1);
                                string dd = Word.ToString();
                                if (m.TypeTag == TypeTag.Menu)
                                { 
                                    Bodybuilder.Append(m.GetOpenTag());
                                }
                                if (m.TypeTag == TypeTag.Code)
                                {
                                    Bodybuilder.Append("assssasss");
                                }
                                Stack.Pop();
                            }



                            break;
                        }
                    case ' ':
                        {
                            w = false;
                            CheckWord(Word);
                            break;
                        }
                    case '\r':
                        {
                            w = false;
                            CheckWord(Word);
                            break;
                        }
                }

            }
            Console.WriteLine(Bodybuilder);
            Console.ReadKey();
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

        static void CheckWord(StringBuilder word)
        {
            if (word.Length == 0)
            {
                return;
            }
            if (word.CompareWord("<menu".ToCharArray()) && _block == false)
            {
                MyTag tag = new MyTag();
                tag.TypeTag = TypeTag.Menu;
                tag.IsOpenTopTag = true;
                //tag.ATributesBuilder.Append(" <menu ");
                Stack.Push(tag);
                int r = Bodybuilder.Length;
                int y = word.Length;
                Bodybuilder.Remove(r - y, y);
                word.Clear();
            }

            if (word.CompareWord("<codes".ToCharArray()))
            {
              
                MyTag tag = new MyTag();
                tag.TypeTag = TypeTag.Code;
                tag.IsOpenTopTag = true;
                //tag.ATributesBuilder.Append(" <menu ");
                Stack.Push(tag);
                int r = Bodybuilder.Length;
                int y = word.Length;
                Bodybuilder.Remove(r - y, y);
                word.Clear(); 
                _block = true;
            }

            if (word.CompareWord("<codes/>".ToCharArray()))
            {
                var st = Stack.First();
                MyTag tag = new MyTag();
                tag.TypeTag = TypeTag.Code;
                tag.IsOpenTopTag = true;
                //tag.ATributesBuilder.Append(" <menu ");
                Stack.Push(tag);
                int r = Bodybuilder.Length;
                int y = word.Length;
                Bodybuilder.Remove(r - y, y);
                word.Clear();
                _block = true;
            }






        }

    }
}
