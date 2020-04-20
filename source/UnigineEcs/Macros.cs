using Flecs;
using System;
using System.Runtime.InteropServices;

namespace UnigineECS
{
    public delegate void SystemAction<T>(ref Rows ids, Span<T> comp, float deltaTime) where T : unmanaged;
    public delegate void SystemAction<T1, T2>(ref Rows ids, Span<T1> comp1, Span<T2> comp2, float deltaTime) where T1 : unmanaged where T2 : unmanaged;
    public delegate void SystemAction<T1, T2, T3>(ref Rows ids, Span<T1> comp1, Span<T2> comp2, Span<T3> comp3, float deltaTime)
        where T1 : unmanaged where T2 : unmanaged where T3 : unmanaged;

    /// <summary>
    /// all SCREAMING_SNAKE_CASE declarative macros from Flecs are implemented here. The idea is to add "using Flecs.Macros" to
    /// your file so that the declarative API will match what Flecs does directly. The differences between macros explained above
    /// also applies here.
    /// </summary>
    public unsafe static class Macros
    {
        public static EntityId ECS_SYSTEM<T1>(World world, SystemAction<T1> systemImpl, SystemKind kind) where T1 : unmanaged
        {
            SystemActionDelegate del = delegate(ref Rows rows)
            {
                var set1 = (T1*)_ecs.column(ref rows, Heap.SizeOf<T1>(), 1);
                systemImpl(ref rows, new Span<T1>(set1, (int)rows.count), ecs.get_delta_time(world));
            };

            // ensure our system doesnt get GCd and that our Component is registered
            Caches.AddSystemAction(world, del);
            Caches.GetComponentTypeId<T1>(world);

            var systemNamePtr = Caches.AddUnmanagedString(systemImpl.Target.GetType().FullName);
            return ecs.new_system(world, systemNamePtr, kind, typeof(T1).Name, del);
        }

        public static EntityId ECS_SYSTEM<T1, T2>(World world, SystemAction<T1, T2> systemImpl, SystemKind kind) where T1 : unmanaged where T2 : unmanaged
        {
            SystemActionDelegate del = delegate(ref Rows rows)
            {
                var set1 = (T1*)_ecs.column(ref rows, Heap.SizeOf<T1>(), 1);
                var set2 = (T2*)_ecs.column(ref rows, Heap.SizeOf<T2>(), 2);
                systemImpl(ref rows, new Span<T1>(set1, (int)rows.count), new Span<T2>(set2, (int)rows.count), ecs.get_delta_time(world));
            };

            // ensure our system doesnt get GCd and that our Component is registered
            Caches.AddSystemAction(world, del);
            Caches.GetComponentTypeId<T1>(world);
            Caches.GetComponentTypeId<T2>(world);

            var systemNamePtr = Caches.AddUnmanagedString(systemImpl.Target.GetType().FullName);
            return ecs.new_system(world, systemNamePtr, kind, $"{typeof(T1).Name}, {typeof(T2).Name}", del);
        }

        public static EntityId ECS_SYSTEM<T1, T2, T3>(World world, SystemAction<T1, T2, T3> systemImpl, SystemKind kind)
            where T1 : unmanaged where T2 : unmanaged where T3 : unmanaged
        {
            SystemActionDelegate del = delegate(ref Rows rows)
            {
                var set1 = (T1*)_ecs.column(ref rows, Heap.SizeOf<T1>(), 1);
                var set2 = (T2*)_ecs.column(ref rows, Heap.SizeOf<T2>(), 2);
                var set3 = (T3*)_ecs.column(ref rows, Heap.SizeOf<T3>(), 3);
                systemImpl(ref rows, new Span<T1>(set1, (int)rows.count), new Span<T2>(set2, (int)rows.count), new Span<T3>(set3, (int)rows.count), ecs.get_delta_time(world));
            };

            // ensure our system doesnt get GCd and that our Component is registered
            Caches.AddSystemAction(world, del);
            Caches.GetComponentTypeId<T1>(world);
            Caches.GetComponentTypeId<T2>(world);
            Caches.GetComponentTypeId<T3>(world);

            var systemNamePtr = Caches.AddUnmanagedString(systemImpl.Target.GetType().FullName);
            return ecs.new_system(world, systemNamePtr, kind, $"{typeof(T1).Name}, {typeof(T2).Name}, {typeof(T3).Name}", del);
        }
    }
}