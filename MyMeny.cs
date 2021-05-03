using System.Text;

namespace z23
{
    class MyMeny
    {
        public bool IsOpenTopTag { get; set; }
        public bool isOpenBody { get; set; }
        public StringBuilder ATributesBuilder { get; set; } = new StringBuilder();
        public StringBuilder BodyBuilder { get; set; } = new StringBuilder();
    }
}