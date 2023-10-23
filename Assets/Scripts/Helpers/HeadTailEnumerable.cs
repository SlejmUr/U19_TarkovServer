using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace TarkovServerU19.BSGClasses
{
    public class HeadTailEnumerable<T> : IEnumerable, ICollection, IEnumerable<T>, IReadOnlyCollection<T>, IProducerConsumerCollection<T>
    {
        public class Head
        {
            internal volatile T[] tparam;

            internal volatile Struct709[] struct709_0;

            private volatile Head prot_Head;

            public long m_index;

            private volatile int int_0;

            private volatile int int_1;

            private volatile HeadTailEnumerable<T> headTailEnumerable;

            internal Head head => prot_Head;

            internal bool Boolean_0 => Int32_0 > Int32_1;

            internal int Int32_0 => Math.Min(int_0, 32);

            internal int Int32_1 => Math.Min(int_1, 31);

            internal Head(long index, HeadTailEnumerable<T> source)
            {
                tparam = new T[32];
                struct709_0 = new Struct709[32];
                int_1 = -1;
                m_index = index;
                headTailEnumerable = source;
            }

            public void Setup(long index, HeadTailEnumerable<T> source)
            {
                for (int i = 0; i < 32; i++)
                {
                    tparam[i] = default(T);
                    struct709_0[i].m_value = false;
                }
                prot_Head = null;
                m_index = 0L;
                int_0 = 0;
                int_1 = -1;
                headTailEnumerable = source;
            }

            internal void method_0(T value)
            {
                int_1++;
                tparam[int_1] = value;
                struct709_0[int_1].m_value = true;
            }

            internal Head method_1()
            {
                Head head = headTailEnumerable.class1914_0.Get();
                head.Setup(m_index + 1L, headTailEnumerable);
                prot_Head = head;
                return head;
            }

            internal void method_2()
            {
                Head head = headTailEnumerable.class1914_0.Get();
                head.Setup(m_index + 1L, headTailEnumerable);
                prot_Head = head;
                headTailEnumerable.m_tail = prot_Head;
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
                        tparam[num] = value;
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
                result = tparam[int32_];
                if (headTailEnumerable.int_1 <= 0)
                {
                    tparam[int32_] = default(T);
                }
                if (int32_ + 1 >= 32)
                {
                    SpinWait spinWait3 = default(SpinWait);
                    while (prot_Head == null)
                    {
                        spinWait3.SpinOnce();
                    }
                    headTailEnumerable.class1914_0.Return(headTailEnumerable.m_head);
                    headTailEnumerable.m_head = prot_Head;
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
                result = tparam[int32_];
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
                    list.Add(tparam[i]);
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
            private readonly ConcurrentBag<U> concurrentBag;

            private readonly Func<U> func;

            public Class1914(Func<U> objectGenerator)
            {
                func = objectGenerator ?? throw new ArgumentNullException("objectGenerator");
                concurrentBag = new ConcurrentBag<U>();
            }

            public U Get()
            {
                if (!concurrentBag.TryTake(out var result))
                {
                    return func();
                }
                return result;
            }

            public void Return(U item)
            {
                concurrentBag.Add(item);
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
                if (head.head == null)
                {
                    return true;
                }
                SpinWait spinWait = default(SpinWait);
                while (true)
                {
                    if (head.Boolean_0)
                    {
                        if (head.head == null)
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
                Spliiter(out var head, out var tail, out var headLow, out var tailHigh);
                if (head != tail)
                {
                    return 32 - headLow + 32 * (int)(tail.m_index - head.m_index - 1L) + (tailHigh + 1);
                }
                return tailHigh - headLow + 1;
            }
        }

        public HeadTailEnumerable()
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

        public HeadTailEnumerable(IEnumerable<T> collection)
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
            ((ICollection)MakeAList()).CopyTo(array, index);
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
            return MakeAList().ToArray();
        }

        private List<T> MakeAList()
        {
            Interlocked.Increment(ref int_1);
            List<T> list = new List<T>();
            try
            {
                Spliiter(out var head, out var tail, out var headLow, out var tailHigh);
                if (head == tail)
                {
                    head.method_6(list, headLow, tailHigh);
                    return list;
                }
                head.method_6(list, headLow, 31);
                for (Head gClass2301_ = head.head; gClass2301_ != tail; gClass2301_ = gClass2301_.head)
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

        private void Spliiter(out Head head, out Head tail, out int headLow, out int tailHigh)
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
            MakeAList().CopyTo(array, index);
        }

        public IEnumerator<T> GetEnumerator()
        {
            Interlocked.Increment(ref int_1);
            Spliiter(out var head, out var tail, out var headLow, out var tailHigh);
            return Enumerator(head, tail, headLow, tailHigh);
        }

        private IEnumerator<T> Enumerator(Head head, Head tail, int headLow, int tailHigh)
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
                        yield return head.tparam[num];
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
                    yield return head.tparam[num];
                    int num2 = num + 1;
                    num = num2;
                }
                for (Head gClass2301_ = head.head; gClass2301_ != tail; gClass2301_ = gClass2301_.head)
                {
                    num = 0;
                    while (num < 32)
                    {
                        spinWait.Reset();
                        while (!gClass2301_.struct709_0[num].m_value)
                        {
                            spinWait.SpinOnce();
                        }
                        yield return gClass2301_.tparam[num];
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
                    yield return tail.tparam[num];
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