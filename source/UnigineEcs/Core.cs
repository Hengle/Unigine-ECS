using Unigine;
using World = Flecs.World;
using static Flecs.Macros;
using Flecs;
using System;

namespace UnigineECS
{
    internal static class Core
    {
        const uint version = (0 << 16) | (0 << 8) | (1);
        static string stringVersion => (version >> 16 & 0xFF) + "." + (version >> 8 & 0xFF) + "." + (version & 0xFF);

        static World mainWorld = default;

        public static void Init()
        {
            App.SetBackgroundUpdate(true);
            Log.Message("{0}", $"\nUnigineECS {stringVersion} has been initialized.\n");

            mainWorld = World.Create(9090);
            ecs.set_threads(mainWorld, (uint)Environment.ProcessorCount);

            new TestSystem(mainWorld);
        }

        public static void Update()
        {
            ecs.progress(mainWorld, Game.IFps);
        }

        public static void Shutdown()
        {
            mainWorld.Fini();
        }
    }
}