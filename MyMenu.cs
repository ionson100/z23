using System;
using System.Collections.Generic;
using System.Text;

namespace z23
{
    public class MyMenu
    {
       
        public MyMenu(int id)
        {
            Id = id;
        }

        public string Parent { get; set; }
       
        public string Text { get; set; }
        public int Id { get; set; }
        public string IdUser { get; set; } = "-312873344";
        public List<MyMenu> ListChildren { get; set; } = new List<MyMenu>();

        public static MyMenu GetMyMenu(MyTag myTag)
        {
            MyMenu menu=new MyMenu(myTag.Id);
            myTag.RefreshAttribute();
            if (myTag.DictionaryAttribute.ContainsKey("id"))
            {
                menu.IdUser = myTag.DictionaryAttribute["id"];
            }
            if (myTag.DictionaryAttribute.ContainsKey("parent"))
            {
                menu.Parent = myTag.DictionaryAttribute["parent"];
            }
            if (myTag.DictionaryAttribute.ContainsKey("text"))
            {
                menu.Text = myTag.DictionaryAttribute["text"];
            }
            if (myTag.DictionaryAttribute.ContainsKey("textmenu"))
            {
                menu.Text = myTag.DictionaryAttribute["textmenu"];
            }

            return menu;


        }
        public string MenuTag => $"<li><a href = \"#{Id}\" class=\"active\">{Text}</a></li>";

        public string MenuTagChildren()
        {

            StringBuilder menu = new StringBuilder();
            menu.Append(
                $"<li>{Environment.NewLine}<a href=\"#homeSubmenu{Id}\" data-toggle=\"collapse\" aria-expanded=\"false\" class=\"dropdown-toggle collapsed\">{Text}</a>{Environment.NewLine}");
            menu.Append($"<ul class=\"collapse list-unstyled\" id=\"homeSubmenu{Id}\">{Environment.NewLine}");
            foreach (MyMenu child in ListChildren)
            {
                menu.Append($"<li><a href = \"#{child.Id}\" >{child.Text}</a></li>").Append(Environment.NewLine);
            }

            menu.Append($"</ul>{Environment.NewLine}</li>");
            return menu.ToString();

        }
    }
}