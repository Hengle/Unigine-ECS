using Unigine;
using World = Flecs.World;
using Flecs;
using System;
using static Flecs.Macros;

namespace UnigineECS
{
    class TestSystem : ComponentSystem<Message>
    {
        public TestSystem(World world) : base(world, SystemKind.OnUpdate)
        {
            var entity = ecs.new_entity<Message>(world);
            ecs.set(world, entity, new Message { Value = Caches.AddUnmanagedString("Hello Flecs#!") });
        }

        protected override void Tick(ref Rows rows, Span<Message> messages, float deltaTime)
        {
            for (int i = 0; i < rows.count; i++)
            {
                Log.Message("{0}, Delta time: {1}", messages[i].Value, deltaTime);
                ecs.delete(world, rows[i]);
            }         
        }
    }
}