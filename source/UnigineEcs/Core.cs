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
            Log.Message("{0}", $"\nUnigineECS {stringVersion} has been initialized.\n");

            mainWorld = World.Create(true);

            ECS_COMPONENT<Message>(mainWorld);
            new TestSystem(mainWorld);

            var entity = ecs.new_entity<Message>(mainWorld);

            ecs.set(mainWorld, entity, new Message { Value = Caches.AddUnmanagedString("Hello Flecs#!") });
            ecs.set_target_fps(mainWorld, 0);
        }

        public static void Update()
        {
            ecs.progress(mainWorld, 0);
        }

        public static void Shutdown()
        {
            mainWorld.Fini();
        }

        struct Message
        {
            public CharPtr Value;
        }

        class TestSystem : ASystem<Message>
        {
            public TestSystem(World world) : base(world)
            {
            }

            protected override void Tick(ref Rows rows, Span<Message> messages)
            {
                for (int i = 0; i < rows.count; i++)
                {
                    Log.Message("{0}", messages[i].Value);
                }
            }
        }
    }
}