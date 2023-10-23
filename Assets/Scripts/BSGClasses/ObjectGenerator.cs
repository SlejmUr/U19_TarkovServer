using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System;

namespace TarkovServerU19.BSGClasses
{
    public class ObjectGenerator<T>
    {
        private readonly Stack<T> stack_0 = new Stack<T>();

        private readonly Func<T> func_0;

        public int Count => stack_0.Count;

        public ObjectGenerator(Func<T> objectGenerator, int initialCapacity)
        {
            func_0 = objectGenerator;
            for (int i = 0; i < initialCapacity; i++)
            {
                stack_0.Push(objectGenerator());
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Get()
        {
            if (stack_0.Count <= 0)
            {
                return func_0();
            }
            return stack_0.Pop();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Return(T item)
        {
            stack_0.Push(item);
        }
    }
}