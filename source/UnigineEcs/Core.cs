using Unigine;
using World = Flecs.World;

namespace UnigineECS
{
    internal static class Core
    {
        const uint version = (0 << 16) | (0 << 8) | (1);
        static string stringVersion => (version >> 16 & 0xFF) + "." + (version >> 8 & 0xFF) + "." + (version & 0xFF);

        public static void Init()
        {
            Log.Message("{0}", $"\nUnigineECS {stringVersion} has been initialized.\n");
        }

        public static void Update()
        {
        }
    }
}