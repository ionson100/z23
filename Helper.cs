using System.Text;

namespace z23
{
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

        public static StringBuilder ToStringBuilder(this string str)
        {
            return Builder.Clear().Append(str);
        }

    }
}