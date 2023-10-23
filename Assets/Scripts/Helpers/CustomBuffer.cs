using System;
using System.Collections;
using System.Collections.Generic;

namespace TarkovServerU19.Helpers
{
    public class CustomBuffer<T> : IEnumerable, IEnumerable<T>
    {
        public CustomBuffer(int capacity) : this(capacity, new T[0])
        {
        }
        public CustomBuffer(int capacity, T[] items)
        {
            if (capacity < 1)
            {
                throw new ArgumentException("Circular buffer cannot have negative or zero capacity.", "capacity");
            }
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }
            if (items.Length > capacity)
            {
                throw new ArgumentException("Too many items to fit circular buffer", "items");
            }
            this.gparam_0 = new T[capacity];
            Array.Copy(items, this.gparam_0, items.Length);
            this.int_2 = items.Length;
            this.int_0 = 0;
            this.int_1 = ((this.int_2 == capacity) ? 0 : this.int_2);
        }

        public int Capacity
        {
            get
            {
                return this.gparam_0.Length;
            }
        }

        public bool IsFull
        {
            get
            {
                return this.Size == this.Capacity;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return this.Size == 0;
            }
        }

        public int Size
        {
            get
            {
                return this.int_2;
            }
        }

        public T Front()
        {
            this.ThrowError("Cannot access an empty buffer.");
            return this.gparam_0[this.int_0];
        }

        public T Back()
        {
            this.ThrowError("Cannot access an empty buffer.");
            return this.gparam_0[((this.int_1 != 0) ? this.int_1 : this.Capacity) - 1];
        }

        public T this[int index]
        {
            get
            {
                if (this.IsEmpty)
                {
                    throw new IndexOutOfRangeException(string.Format("Cannot access index {0}. Buffer is empty", index));
                }
                if (index >= this.int_2)
                {
                    throw new IndexOutOfRangeException(string.Format("Cannot access index {0}. Buffer size is {1}", index, this.int_2));
                }
                int num = this.method_3(index);
                return this.gparam_0[num];
            }
            set
            {
                if (this.IsEmpty)
                {
                    throw new IndexOutOfRangeException(string.Format("Cannot access index {0}. Buffer is empty", index));
                }
                if (index >= this.int_2)
                {
                    throw new IndexOutOfRangeException(string.Format("Cannot access index {0}. Buffer size is {1}", index, this.int_2));
                }
                int num = this.method_3(index);
                this.gparam_0[num] = value;
            }
        }

        public void PushBack(T item)
        {
            if (this.IsFull)
            {
                this.gparam_0[this.int_1] = item;
                this.method_1(ref this.int_1);
                this.int_0 = this.int_1;
                return;
            }
            this.gparam_0[this.int_1] = item;
            this.method_1(ref this.int_1);
            this.int_2++;
        }

        public void PushFront(T item)
        {
            if (this.IsFull)
            {
                this.method_2(ref this.int_0);
                this.int_1 = this.int_0;
                this.gparam_0[this.int_0] = item;
                return;
            }
            this.method_2(ref this.int_0);
            this.gparam_0[this.int_0] = item;
            this.int_2++;
        }

        public void PopBack()
        {
            this.ThrowError("Cannot take elements from an empty buffer.");
            this.method_2(ref this.int_1);
            this.gparam_0[this.int_1] = default(T);
            this.int_2--;
        }

        public void PopFront()
        {
            this.ThrowError("Cannot take elements from an empty buffer.");
            this.gparam_0[this.int_0] = default(T);
            this.method_1(ref this.int_0);
            this.int_2--;
        }

        public void Clear()
        {
            this.int_0 = 0;
            this.int_1 = 0;
            this.int_2 = 0;
            Array.Clear(this.gparam_0, 0, this.gparam_0.Length);
        }

        public T[] ToArray()
        {
            T[] array = new T[this.Size];
            int num = 0;
            foreach (ArraySegment<T> arraySegment in this.ToArraySegments())
            {
                Array.Copy(arraySegment.Array, arraySegment.Offset, array, num, arraySegment.Count);
                num += arraySegment.Count;
            }
            return array;
        }

        public IList<ArraySegment<T>> ToArraySegments()
        {
            return new ArraySegment<T>[]
            {
            this.method_4(),
            this.method_5()
            };
        }

        public IEnumerator<T> GetEnumerator()
        {
            IList<ArraySegment<T>> list = ToArraySegments();
            foreach (ArraySegment<T> item in list)
            {
                for (int i = 0; i < item.Count; i++)
                {
                    yield return item.Array[item.Offset + i];
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void ThrowError(string message = "Cannot access an empty buffer.")
        {
            if (this.IsEmpty)
            {
                throw new InvalidOperationException(message);
            }
        }

        private void method_1(ref int index)
        {
            int num = index + 1;
            index = num;
            if (num == this.Capacity)
            {
                index = 0;
            }
        }

        private void method_2(ref int index)
        {
            if (index == 0)
            {
                index = this.Capacity;
            }
            index--;
        }

        private int method_3(int index)
        {
            return this.int_0 + ((index < this.Capacity - this.int_0) ? index : (index - this.Capacity));
        }

        private ArraySegment<T> method_4()
        {
            if (this.IsEmpty)
            {
                return new ArraySegment<T>(new T[0]);
            }
            if (this.int_0 < this.int_1)
            {
                return new ArraySegment<T>(this.gparam_0, this.int_0, this.int_1 - this.int_0);
            }
            return new ArraySegment<T>(this.gparam_0, this.int_0, this.gparam_0.Length - this.int_0);
        }

        private ArraySegment<T> method_5()
        {
            if (this.IsEmpty)
            {
                return new ArraySegment<T>(new T[0]);
            }
            if (this.int_0 < this.int_1)
            {
                return new ArraySegment<T>(this.gparam_0, this.int_1, 0);
            }
            return new ArraySegment<T>(this.gparam_0, 0, this.int_1);
        }

        private readonly T[] gparam_0;
        private int int_0;
        private int int_1;
        private int int_2;
    }
}
