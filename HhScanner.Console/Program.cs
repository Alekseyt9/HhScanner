

namespace HhScanner
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var scanner = new HhScanner();
            await scanner.Scan();
        }
    }
}