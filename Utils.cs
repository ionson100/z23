using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace z23
{
    internal class Utils
    {
        public static readonly string FolderStatic = "static";
        private static readonly string FolderContent = "content";

        public static StringBuilder PreCompiler(string file)
        {
            StringBuilder builder = new StringBuilder();
            StreamReader sr = new StreamReader(file);
            while (!sr.EndOfStream)
            {
                char c = (char) sr.Read();
                builder.Append(c);
                switch (c)
                {
                    case '>':
                    {
                        while (true)
                        {
                            int f = builder.Length - 2;
                            if (f >= 0)
                            {
                                if (builder[f] == '\n')
                                {
                                    builder.Replace(Environment.NewLine, " ", f - 1, 2);
                                }
                                else if (builder[f] == ' ')
                                {
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

            string[] d = builder.ToString().Split(Environment.NewLine.ToCharArray(), StringSplitOptions.None);
            builder.Clear();
            bool add = true;
            for (int i = 0; i < d.Length; i++)
            {
                if (i == 0)
                {
                    builder.AppendLine(d[0]);
                    continue;
                }

                

               
                if (i<d.Length-1 &&string.IsNullOrEmpty(d[i].Trim()) &&string.IsNullOrEmpty(d[i + 1].Trim()) && string.IsNullOrEmpty(d[i - 1].Trim()))
                {
                    builder.AppendLine("#####");
                    continue;
                }
                builder.AppendLine(d[i]);
            }

            return builder;
        }

        public static void CheckFileStatic()
        {
            if (Directory.Exists(FolderStatic) == false)
            {
                Directory.CreateDirectory(FolderStatic);
            }

            string p = Path.Combine(FolderStatic, "arrow-up-left-circle.svg");
            if (File.Exists(p) == false)
            {
                File.WriteAllBytes(p, ResourceCore.arrow_up_left_circle);
            }

            p = Path.Combine(FolderStatic, "bootstrap.css");
            if (File.Exists(p) == false)
            {
                File.WriteAllText(p, ResourceCore.bootstrap);
            }

            p = Path.Combine(FolderStatic, "bootstrap.js");
            if (File.Exists(p) == false)
            {
                File.WriteAllText(p, ResourceCore.bootstrap1);
            }

            p = Path.Combine(FolderStatic, "jquery-1.js");
            if (File.Exists(p) == false)
            {
                File.WriteAllText(p, ResourceCore.jquery_1);
            }

            p = Path.Combine(FolderStatic, "main.css");
            if (File.Exists(p) == false)
            {
                File.WriteAllText(p, ResourceCore.main);
            }

            p = Path.Combine(FolderStatic, "main2.css");
            if (File.Exists(p) == false)
            {
                File.WriteAllText(p, ResourceCore.main2);
            }

            p = Path.Combine(FolderStatic, "prism.css");
            if (File.Exists(p) == false)
            {
                File.WriteAllText(p, ResourceCore.prism);
            }

            p = Path.Combine(FolderStatic, "prism.js");
            if (File.Exists(p) == false)
            {
                File.WriteAllText(p, ResourceCore.prism1);
            }

            if (Directory.Exists(FolderContent) == false)
            {
                Directory.CreateDirectory(FolderContent);
            }

            if (File.Exists(Program.Z23.FileNameTemplate) == false)
            {
                File.WriteAllText(Program.Z23.FileNameTemplate, ResourceCore.templateindex);
            }
        }

        public static string BuildMenu(List<MyMenu> menus)
        {
            foreach (MyMenu menuTemp in menus)
            {
                if (menuTemp == null) continue;
                var l = menus.Where(a => a != null && a.Parent == menuTemp.IdUser).ToList();
                menuTemp.ListChildren = l;
            }

            StringBuilder menu = new StringBuilder();
            HashSet<int> set = new HashSet<int>();
            foreach (var menuTemp in menus)
            {
                if (menuTemp == null) continue;
                if (set.Contains(menuTemp.Id)) continue;
                if (menuTemp.Parent != null) continue;
                set.Add(menuTemp.Id);
                if (menuTemp.ListChildren.Count == 0)
                {
                    menu.Append(menuTemp.MenuTag).Append(Environment.NewLine);
                }
                else
                {
                    menu.Append(menuTemp.MenuTagChildren());
                }
            }

            return menu.ToString();
        }
    }
}