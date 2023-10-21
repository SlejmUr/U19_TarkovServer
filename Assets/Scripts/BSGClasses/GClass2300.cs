using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace TarkovServerU19.BSGClasses
{
    public class GClass2300<T> : IEnumerable, ICollection, IEnumerable<T>, IReadOnlyCollection<T>, IProducerConsumerCollection<T>
    {
        public class Head
        {
            internal volatile T[] gparam_0;

            internal volatile Struct709[] struct709_0;

            private volatile Head gclass2301_0;

            public long m_index;

            private volatile int int_0;

            private volatile int int_1;

            private volatile GClass2300<T> gclass2300_0;

            internal Head GClass2301_0 => gclass2301_0;

            internal bool Boolean_0 => Int32_0 > Int32_1;

            internal int Int32_0 => Math.Min(int_0, 32);

            internal int Int32_1 => Math.Min(int_1, 31);

            internal Head(long index, GClass2300<T> source)
            {
                gparam_0 = new T[32];
                struct709_0 = new Struct709[32];
                int_1 = -1;
                m_index = index;
                gclass2300_0 = source;
            }

            public void Setup(long index, GClass2300<T> source)
            {
                for (int i = 0; i < 32; i++)
                {
                    gparam_0[i] = default(T);
                    struct709_0[i].m_value = false;
                }
                gclass2301_0 = null;
                m_index = 0L;
                int_0 = 0;
                int_1 = -1;
                gclass2300_0 = source;
            }

            internal void method_0(T value)
            {
                int_1++;
                gparam_0[int_1] = value;
                struct709_0[int_1].m_value = true;
            }

            internal Head method_1()
            {
                Head head = gclass2300_0.class1914_0.Get();
                head.Setup(m_index + 1L, gclass2300_0);
                gclass2301_0 = head;
                return head;
            }

            internal void method_2()
            {
                Head head = gclass2300_0.class1914_0.Get();
                head.Setup(m_index + 1L, gclass2300_0);
                gclass2301_0 = head;
                gclass2300_0.m_tail = gclass2301_0;
            }

            internal bool method_3(T value)
            {
                if (int_1 >= 31)
                {
                    return false;
                }
                int num = 32;
                try
                {
                }
                finally
                {
                    num = Interlocked.Increment(ref int_1);
                    if (num <= 31)
                    {
                        gparam_0[num] = value;
                        struct709_0[num].m_value = true;
                    }
                    if (num == 31)
                    {
                        method_2();
                    }
                }
                return num <= 31;
            }

            internal bool method_4(out T result)
            {
                SpinWait spinWait = default(SpinWait);
                int int32_ = Int32_0;
                int int32_2 = Int32_1;
                while (true)
                {
                    if (int32_ <= int32_2)
                    {
                        if (Interlocked.CompareExchange(ref int_0, int32_ + 1, int32_) == int32_)
                        {
                            break;
                        }
                        spinWait.SpinOnce();
                        int32_ = Int32_0;
                        int32_2 = Int32_1;
                        continue;
                    }
                    result = default(T);
                    return false;
                }
                SpinWait spinWait2 = default(SpinWait);
                while (!struct709_0[int32_].m_value)
                {
                    spinWait2.SpinOnce();
                }
                result = gparam_0[int32_];
                if (gclass2300_0.int_1 <= 0)
                {
                    gparam_0[int32_] = default(T);
                }
                if (int32_ + 1 >= 32)
                {
                    SpinWait spinWait3 = default(SpinWait);
                    while (gclass2301_0 == null)
                    {
                        spinWait3.SpinOnce();
                    }
                    gclass2300_0.class1914_0.Return(gclass2300_0.m_head);
                    gclass2300_0.m_head = gclass2301_0;
                }
                return true;
            }

            internal bool method_5(out T result)
            {
                result = default(T);
                int int32_ = Int32_0;
                if (int32_ > Int32_1)
                {
                    return false;
                }
                SpinWait spinWait = default(SpinWait);
                while (!struct709_0[int32_].m_value)
                {
                    spinWait.SpinOnce();
                }
                result = gparam_0[int32_];
                return true;
            }

            internal void method_6(List<T> list, int start, int end)
            {
                for (int i = start; i <= end; i++)
                {
                    SpinWait spinWait = default(SpinWait);
                    while (!struct709_0[i].m_value)
                    {
                        spinWait.SpinOnce();
                    }
                    list.Add(gparam_0[i]);
                }
            }
        }

        public struct Struct709
        {
            public volatile bool m_value;

            public Struct709(bool value)
            {
                m_value = value;
            }
        }

        internal class Class1914<U>
        {
            private readonly ConcurrentBag<U> concurrentBag_0;

            private readonly Func<U> func_0;

            public Class1914(Func<U> objectGenerator)
            {
                func_0 = objectGenerator ?? throw new ArgumentNullException("objectGenerator");
                concurrentBag_0 = new ConcurrentBag<U>();
            }

            public U Get()
            {
                if (!concurrentBag_0.TryTake(out var result))
                {
                    return func_0();
                }
                return result;
            }

            public void Return(U item)
            {
                concurrentBag_0.Add(item);
            }
        }

        [Serializable]
        private sealed class Class1915
        {
            public static readonly Class1915 class1915_0 = new Class1915();

            public static Func<Head> func_0;

            public static Func<Head> func_1;

            internal Head method_0()
            {
                return new Head(0L, null);
            }

            internal Head method_1()
            {
                return new Head(0L, null);
            }
        }

        [NonSerialized]
        public volatile Head m_head;

        [NonSerialized]
        public volatile Head m_tail;

        private T[] gparam_0;

        private const int int_0 = 32;

        [NonSerialized]
        internal volatile int int_1;

        internal Class1914<Head> class1914_0 = new Class1914<Head>(() => new Head(0L, null));

        bool ICollection.IsSynchronized => false;

        object ICollection.SyncRoot
        {
            get
            {
                throw new NotSupportedException("ConcurrentCollection_SyncRoot_NotSupported");
            }
        }

        public bool IsEmpty
        {
            get
            {
                Head head = m_head;
                if (!head.Boolean_0)
                {
                    return false;
                }
                if (head.GClass2301_0 == null)
                {
                    return true;
                }
                SpinWait spinWait = default(SpinWait);
                while (true)
                {
                    if (head.Boolean_0)
                    {
                        if (head.GClass2301_0 == null)
                        {
                            break;
                        }
                        spinWait.SpinOnce();
                        head = m_head;
                        continue;
                    }
                    return false;
                }
                return true;
            }
        }

        public int Count
        {
            get
            {
                method_4(out var head, out var tail, out var headLow, out var tailHigh);
                if (head != tail)
                {
                    return 32 - headLow + 32 * (int)(tail.m_index - head.m_index - 1L) + (tailHigh + 1);
                }
                return tailHigh - headLow + 1;
            }
        }

        public GClass2300()
        {
            Head head = class1914_0.Get();
            head.Setup(0L, this);
            m_head = (m_tail = head);
        }

        private void method_0(IEnumerable<T> collection)
        {
            Head head = class1914_0.Get();
            head.Setup(0L, this);
            m_head = head;
            int num = 0;
            foreach (T item in collection)
            {
                head.method_0(item);
                num++;
                if (num >= 32)
                {
                    head = head.method_1();
                    num = 0;
                }
            }
            m_tail = head;
        }

        public GClass2300(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }
            method_0(collection);
        }

        void ICollection.CopyTo(Array array, int index)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }
            ((ICollection)method_3()).CopyTo(array, index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        bool IProducerConsumerCollection<T>.TryAdd(T item)
        {
            Enqueue(item);
            return true;
        }

        bool IProducerConsumerCollection<T>.TryTake(out T item)
        {
            return TryDequeue(out item);
        }

        public T[] ToArray()
        {
            return method_3().ToArray();
        }

        private List<T> method_3()
        {
            Interlocked.Increment(ref int_1);
            List<T> list = new List<T>();
            try
            {
                method_4(out var head, out var tail, out var headLow, out var tailHigh);
                if (head == tail)
                {
                    head.method_6(list, headLow, tailHigh);
                    return list;
                }
                head.method_6(list, headLow, 31);
                for (Head gClass2301_ = head.GClass2301_0; gClass2301_ != tail; gClass2301_ = gClass2301_.GClass2301_0)
                {
                    gClass2301_.method_6(list, 0, 31);
                }
                tail.method_6(list, 0, tailHigh);
                return list;
            }
            finally
            {
                Interlocked.Decrement(ref int_1);
            }
        }

        private void method_4(out Head head, out Head tail, out int headLow, out int tailHigh)
        {
            head = m_head;
            tail = m_tail;
            headLow = head.Int32_0;
            tailHigh = tail.Int32_1;
            SpinWait spinWait = default(SpinWait);
            while (head != m_head || tail != m_tail || headLow != head.Int32_0 || tailHigh != tail.Int32_1 || head.m_index > tail.m_index)
            {
                spinWait.SpinOnce();
                head = m_head;
                tail = m_tail;
                headLow = head.Int32_0;
                tailHigh = tail.Int32_1;
            }
        }

        public void CopyTo(T[] array, int index)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }
            method_3().CopyTo(array, index);
        }

        public IEnumerator<T> GetEnumerator()
        {
            Interlocked.Increment(ref int_1);
            method_4(out var head, out var tail, out var headLow, out var tailHigh);
            return method_5(head, tail, headLow, tailHigh);
        }

        private IEnumerator<T> method_5(Head head, Head tail, int headLow, int tailHigh)
        {
            try
            {
                SpinWait spinWait = default(SpinWait);
                int num;
                if (head == tail)
                {
                    num = headLow;
                    while (num <= tailHigh)
                    {
                        spinWait.Reset();
                        while (!head.struct709_0[num].m_value)
                        {
                            spinWait.SpinOnce();
                        }
                        yield return head.gparam_0[num];
                        int num2 = num + 1;
                        num = num2;
                    }
                    yield break;
                }
                num = headLow;
                while (num < 32)
                {
                    spinWait.Reset();
                    while (!head.struct709_0[num].m_value)
                    {
                        spinWait.SpinOnce();
                    }
                    yield return head.gparam_0[num];
                    int num2 = num + 1;
                    num = num2;
                }
                for (Head gClass2301_ = head.GClass2301_0; gClass2301_ != tail; gClass2301_ = gClass2301_.GClass2301_0)
                {
                    num = 0;
                    while (num < 32)
                    {
                        spinWait.Reset();
                        while (!gClass2301_.struct709_0[num].m_value)
                        {
                            spinWait.SpinOnce();
                        }
                        yield return gClass2301_.gparam_0[num];
                        int num2 = num + 1;
                        num = num2;
                    }
                }
                num = 0;
                while (num <= tailHigh)
                {
                    spinWait.Reset();
                    while (!tail.struct709_0[num].m_value)
                    {
                        spinWait.SpinOnce();
                    }
                    yield return tail.gparam_0[num];
                    int num2 = num + 1;
                    num = num2;
                }
            }
            finally
            {
                Interlocked.Decrement(ref int_1);
            }
        }

        public void Enqueue(T item)
        {
            SpinWait spinWait = default(SpinWait);
            while (!m_tail.method_3(item))
            {
                spinWait.SpinOnce();
            }
        }

        public bool TryDequeue(out T result)
        {
            do
            {
                if (IsEmpty)
                {
                    result = default(T);
                    return false;
                }
            }
            while (!m_head.method_4(out result));
            return true;
        }

        public bool TryPeek(out T result)
        {
            Interlocked.Increment(ref int_1);
            do
            {
                if (IsEmpty)
                {
                    result = default(T);
                    Interlocked.Decrement(ref int_1);
                    return false;
                }
            }
            while (!m_head.method_5(out result));
            Interlocked.Decrement(ref int_1);
            return true;
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }
    }
}