using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace z23
{
    public class JsonCommentConverter : JsonConverter
    {
        private readonly string _comment;
        private readonly int _d;

        public JsonCommentConverter(string comment,int d)
        {
            _comment = comment;
            _d = d;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value);

            if (Program.Z23.IsShowCommentaryJson)
            {
                writer.WriteRaw(GetDelemiter(_d));
                writer.WriteComment(_comment);
            }

            
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType) => true;
        public override bool CanRead => false;
        string GetDelemiter(int d)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < d; i++)
            {
                builder.Append(" ");
            }

            return builder.ToString();
        }
    }
}
