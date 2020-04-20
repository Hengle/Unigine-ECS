using Flecs;
using System;
using static Flecs.Macros;
using static UnigineECS.Macros;

namespace UnigineECS
{
    public abstract class ComponentSystem<T> : ISystem where T : unmanaged
    {
        protected World world;
        string ISystem.Signature => typeof(T).Name;

        public ComponentSystem(World world, SystemKind systemKind)
        {
            this.world = world;
            ECS_COMPONENT<T>(world);
            ECS_SYSTEM<T>(world, Tick, systemKind);
        }

        unsafe void ISystem.Tick(ref Rows rows)
        {
            var set1 = (T*)_ecs.column(ref rows, Heap.SizeOf<T>(), 1);
            Tick(ref rows, new Span<T>(set1, (int)rows.count), ecs.get_delta_time(world));
        }

        protected abstract void Tick(ref Rows rows, Span<T> comp1, float deltaTime);
    }

    public abstract class ComponentSystem<T1, T2> : ISystem where T1 : unmanaged where T2 : unmanaged
    {
        protected World world;
        string ISystem.Signature => typeof(T1).Name + ", " + typeof(T2).Name;

        public ComponentSystem(World world, SystemKind systemKind)
        {
            this.world = world;
            ECS_COMPONENT<T1>(world);
            ECS_COMPONENT<T2>(world);
            ECS_SYSTEM<T1, T2>(world, Tick, systemKind);
        }

        unsafe void ISystem.Tick(ref Rows rows)
        {
            var set1 = (T1*)_ecs.column(ref rows, Heap.SizeOf<T1>(), 1);
            var set2 = (T2*)_ecs.column(ref rows, Heap.SizeOf<T2>(), 2);
            Tick(ref rows, new Span<T1>(set1, (int)rows.count), new Span<T2>(set2, (int)rows.count), ecs.get_delta_time(world));
        }

        protected abstract void Tick(ref Rows rows, Span<T1> comp1, Span<T2> comp2, float deltaTime);
    }

    public abstract class ComponentSystem<T1, T2, T3> : ISystem where T1 : unmanaged where T2 : unmanaged where T3 : unmanaged
    {
        protected World world;
        string ISystem.Signature => typeof(T1).Name + ", " + typeof(T2).Name + ", " + typeof(T3).Name;

        public ComponentSystem(World world, SystemKind systemKind)
        {
            this.world = world;
            ECS_COMPONENT<T1>(world);
            ECS_COMPONENT<T2>(world);
            ECS_COMPONENT<T3>(world);
            ECS_SYSTEM<T1, T2, T3>(world, Tick, systemKind);
        }

        unsafe void ISystem.Tick(ref Rows rows)
        {
            var set1 = (T1*)_ecs.column(ref rows, Heap.SizeOf<T1>(), 1);
            var set2 = (T2*)_ecs.column(ref rows, Heap.SizeOf<T2>(), 2);
            var set3 = (T3*)_ecs.column(ref rows, Heap.SizeOf<T3>(), 3);
            Tick(ref rows, new Span<T1>(set1, (int)rows.count), new Span<T2>(set2, (int)rows.count), new Span<T3>(set3, (int)rows.count), ecs.get_delta_time(world));
        }

        protected abstract void Tick(ref Rows rows, Span<T1> comp1, Span<T2> comp2, Span<T3> comp3, float deltaTime);
    }


}
