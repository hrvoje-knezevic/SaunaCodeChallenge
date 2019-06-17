using SaunaCodeChallengeMapPath.Shared;
using System.IO;

namespace SaunaCodeChallengeTest
{
    public class TestResource
    {
        public static Stream GetResourceMap(MapSelector mapsSelector)
        {
            Stream resourceMap = new MemoryStream();

            switch (mapsSelector)
            {
                case MapSelector.Map1:
                    resourceMap = GetResourceTestMap1();
                    break;
                case MapSelector.Map2:
                    resourceMap = GetResourceTestMap2();
                    break;
                case MapSelector.Map3:
                    resourceMap = GetResourceTestMap3();
                    break;
                case MapSelector.Map4:
                    resourceMap = GetResourceTestMap4();
                    break;
                default:
                    break;
            }

            return resourceMap;
        }

        private static Stream GetResourceTestMap1()
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write("@---A---+\n");
            writer.Write("        |\n");
            writer.Write("x-B-+   C\n");
            writer.Write("    |   |\n");
            writer.Write("    +---+\n");
            writer.Flush();
            stream.Position = 0;

            return stream;
        }

        private static Stream GetResourceTestMap2()
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write("@         \n");
            writer.Write("| C----+  \n");
            writer.Write("A |    |  \n");
            writer.Write("+---B--+  \n");
            writer.Write("  |      x\n");
            writer.Write("  |      |\n");
            writer.Write("  +---D--+\n");
            writer.Flush();
            stream.Position = 0;

            return stream;
        }

        private static Stream GetResourceTestMap3()
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write("  @---+   \n");
            writer.Write("      B   \n");
            writer.Write("K-----|--A\n");
            writer.Write("|     |  |\n");
            writer.Write("|  +--E  |\n");
            writer.Write("|  |     |\n");
            writer.Write("+--E--Ex C\n");
            writer.Write("   |     |\n");
            writer.Write("   +--F--+\n");
            writer.Flush();
            stream.Position = 0;

            return stream;
        }

        private static Stream GetResourceTestMap4()
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write("   S-A+    \n");
            writer.Write("   |  |    \n");
            writer.Write("   @  +-U  \n");
            writer.Write("        |  \n");
            writer.Write("        N  \n");
            writer.Write("    x-A-+  \n");
            writer.Flush();
            stream.Position = 0;

            return stream;
        }
    }
}
