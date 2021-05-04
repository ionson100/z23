using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace z23
{
    class Utils
    {
       public static StringBuilder PreCompiler(string file)
        {
            StringBuilder builder = new StringBuilder();
            StreamReader sr = new StreamReader(file);
            while (!sr.EndOfStream)
            {
                char c = (char)sr.Read();
                builder.Append(c);
                switch (c)
                {
                    case '>':
                    {

                        while (true)
                        {
                            int f = builder.Length - 2;
                            if ((f) >= 0)
                            {
                                if (builder[f] == '\n')
                                {
                                    string sddd = builder[f].ToString();
                                    builder.Replace(Environment.NewLine, " ", f - 1, 2);
                                }
                                else if (builder[f] == ' ')
                                {
                                    string sddd = builder[f].ToString();
                                    builder.Remove(f, 1);
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

                }

                builder.Replace("<menu>", "<menu >").Replace("<codes>", "<codes >");


            }
            return builder;

        }
    }
}
