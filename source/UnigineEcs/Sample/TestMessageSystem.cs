using Flecs;
using System;
using Unigine;
using World = Flecs.World;

namespace UnigineECS
{
    public class TestSystem : ComponentSystem<Message>
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
                Log.Message($"{messages[i].Value}, Delta time: {deltaTime} \n");
                ecs.delete(world, rows[i]);
            }
        }
    }
}