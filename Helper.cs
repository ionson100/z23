using System.Linq;
using System.Text;

namespace z23
{
    static class Helper
    {
        private static readonly char[] Deliter = new[] {'\r', '\n', '\t', ' '};
        public static bool CompareWord(this StringBuilder builder, char[] word)
        {
            //string asa=builder.ToString();
            if (builder.Length == 0) return false;
            if (builder.Length != word.Length) return false;
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
                    break;
                }
            }

            return result;
        }

        public static StringBuilder RemoveFromEnd(this StringBuilder builder, StringBuilder v)
        {
            int i = builder.Length, ii = v.Length;
            return builder.Remove(i - ii, ii);
        }

        public static StringBuilder RemoveFromEnd(this StringBuilder builder, string v)
        {
            int i = builder.Length, ii = v.Length;
            return builder.Remove(i - ii, ii);
        }

        public static bool StartWitchOpenTag(this string s)
        {
            foreach (var t in s.Where(t => !Deliter.Contains(t)))
            {
                if (t == '<')
                {
                    return true;
                }
                return false;
            }

            return true;
        }
    }
}