using Flecs;
using System;
using System.Linq;
using Unigine;
using World = Flecs.World;
using static Flecs.Macros;
using System.Reflection;

namespace UnigineECS
{
    internal static class Core
    {
        private const uint version = (0 << 16) | (0 << 8) | (1);
        static string stringVersion => (version >> 16 & 0xFF) + "." + (version >> 8 & 0xFF) + "." + (version & 0xFF);

        private static World mainWorld = default;

        public static void Init()
        {
            App.SetBackgroundUpdate(true);
            CreateMainWorld();
            RegisterIComponents();

            Log.Message("{0}", $"\nUnigineECS {stringVersion} has been initialized.\n");
            new TestSystem(mainWorld);
        }

        private static void RegisterIComponents()
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type componentType in assembly.GetTypes())
                {
                    if (typeof(IComponent).IsAssignableFrom(componentType) && componentType.IsValueType)
                    {
                        ECS_COMPONENT(mainWorld, componentType);
                    }
                }
            }
        }

        private static void CreateMainWorld()
        {
            mainWorld = World.Create(9090);
            ecs.set_threads(mainWorld, (uint)Environment.ProcessorCount);
        }

        public static void Update()
        {
            ecs.progress(mainWorld, Game.IFps);
        }

        public static void Shutdown()
        {
            mainWorld.Quit();
        }
    }
}