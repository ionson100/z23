using Newtonsoft.Json;

namespace z23
{
   public class SettingsBase
    {
        [JsonConverter(typeof(JsonCommentConverter), "Тег применяемый для разметки элемента пользователя", 30)]
        public string Tag { get; set; }
        [JsonConverter(typeof(JsonCommentConverter), "Атрибуты тега, такие как класс, стили и т.д.( пример \" class=\"myclass\"  \"",27)]
        public string UserAttribute { get; set; } = string.Empty;
    }

   public class SettingsMenu : SettingsBase
   {
       [JsonConverter(typeof(JsonCommentConverter), "Контейнер пункта меню, если тег разметки ограничивает контент ( default div )", 24)]
        public bool UsageContainer { get; set; } = true;
       public SettingsBase Container { get; set; } = new SettingsBase {Tag = "div",UserAttribute = " style=\"color:green;\" "};
   }

   public class SettingsCode : SettingsBase
   {
       [JsonConverter(typeof(JsonCommentConverter), "Атрибуты тега Pre (сырой код) как правило стили Prism", 5)]
        public string PreUserAttribute { get; set; }
   }

   public class ListZ23 : SettingsBase
   {
       public SettingsBase Item { get; set; } = new SettingsBase {Tag ="li",UserAttribute = " type=\"1\" " };
   }

   public class SettingsZ23
   {
       public bool IsShowCommentaryJson { get; set; } = true;
       [JsonConverter(typeof(JsonCommentConverter), "Закрытие консольной программы, не ждать клавишу ввода или ждать",30)]
        public bool AutoClose { get; set; }
        [JsonConverter(typeof(JsonCommentConverter), "Символ применяемого пробела",32)]
        public string Space { get; set; } = "&nbsp;";
        [JsonConverter(typeof(JsonCommentConverter), "Название файла шаблона, расширение может быть любым",9)]
        public string FileNameTemplate { get; set; } = "templateindex.xml";
       public ListZ23 List { get; set; } = new ListZ23 {Tag = "ul" ,UserAttribute = " style=\"color:red;\" "};
       public SettingsMenu Menu { get; set; } = new SettingsMenu {Tag = "h3"};
       public SettingsCode Code { get; set; } = new SettingsCode {UserAttribute = " class=\"language-csharp\" ", Tag = "code", PreUserAttribute = " class=\"line-numbers\" " };
   }
}
