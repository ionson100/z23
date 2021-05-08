namespace z23
{
   public class SettingsBase
    {
        public string Tag { get; set; }
        public string UserAttribute { get; set; } = string.Empty;
    }

   public class SettingsMenu : SettingsBase
   {
       public bool UsageContainer { get; set; } = true;
       public SettingsBase Container { get; set; } = new SettingsBase {Tag = "div"};
   }

   public class SettingsCode : SettingsBase
   {
       public string PreUserAttribute { get; set; }
   }

   public class ListZ23 : SettingsBase
   {
       public SettingsBase Item { get; set; } = new SettingsBase {Tag ="li",UserAttribute = " type=\"1\" " };
   }

   public class SettingsZ23
   {
       public string Space { get; set; } = "&nbsp;";
       public string FileNameTemplate { get; set; } = "templateindex.xml";
       public ListZ23 List { get; set; } = new ListZ23 {Tag = "ul"};
       public SettingsMenu Menu { get; set; } = new SettingsMenu {Tag = "h3"};
       public SettingsCode Code { get; set; } = new SettingsCode {UserAttribute = " class=\"language-csharp\" ", Tag = "code", PreUserAttribute = " class=\"line-numbers\" " };
   }
}
