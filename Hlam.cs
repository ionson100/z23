using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace z23
{
    class Hlam
    {
        private static void NewMethod()
        {
            StringBuilder bodybuilder=new StringBuilder();
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
    }
}
