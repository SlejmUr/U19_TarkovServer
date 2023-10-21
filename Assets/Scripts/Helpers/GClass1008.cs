using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarkovServerU19.Helpers
{
    public class GClass1008<T> : IEnumerable, IEnumerable<T>
    {
        // Token: 0x060057D3 RID: 22483 RVA: 0x001B5495 File Offset: 0x001B3695
        public GClass1008(int capacity) : this(capacity, new T[0])
        {
        }

        // Token: 0x060057D4 RID: 22484 RVA: 0x001B54A4 File Offset: 0x001B36A4
        public GClass1008(int capacity, T[] items)
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

        // Token: 0x17000B57 RID: 2903
        // (get) Token: 0x060057D5 RID: 22485 RVA: 0x001B5532 File Offset: 0x001B3732
        public int Capacity
        {
            get
            {
                return this.gparam_0.Length;
            }
        }

        // Token: 0x17000B58 RID: 2904
        // (get) Token: 0x060057D6 RID: 22486 RVA: 0x001B553C File Offset: 0x001B373C
        public bool IsFull
        {
            get
            {
                return this.Size == this.Capacity;
            }
        }

        // Token: 0x17000B59 RID: 2905
        // (get) Token: 0x060057D7 RID: 22487 RVA: 0x001B554C File Offset: 0x001B374C
        public bool IsEmpty
        {
            get
            {
                return this.Size == 0;
            }
        }

        // Token: 0x17000B5A RID: 2906
        // (get) Token: 0x060057D8 RID: 22488 RVA: 0x001B5557 File Offset: 0x001B3757
        public int Size
        {
            get
            {
                return this.int_2;
            }
        }

        // Token: 0x060057D9 RID: 22489 RVA: 0x001B555F File Offset: 0x001B375F
        public T Front()
        {
            this.method_0("Cannot access an empty buffer.");
            return this.gparam_0[this.int_0];
        }

        // Token: 0x060057DA RID: 22490 RVA: 0x001B557D File Offset: 0x001B377D
        public T Back()
        {
            this.method_0("Cannot access an empty buffer.");
            return this.gparam_0[((this.int_1 != 0) ? this.int_1 : this.Capacity) - 1];
        }

        // Token: 0x17000B5B RID: 2907
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

        // Token: 0x060057DD RID: 22493 RVA: 0x001B5688 File Offset: 0x001B3888
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

        // Token: 0x060057DE RID: 22494 RVA: 0x001B56F4 File Offset: 0x001B38F4
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

        // Token: 0x060057DF RID: 22495 RVA: 0x001B5760 File Offset: 0x001B3960
        public void PopBack()
        {
            this.method_0("Cannot take elements from an empty buffer.");
            this.method_2(ref this.int_1);
            this.gparam_0[this.int_1] = default(T);
            this.int_2--;
        }

        // Token: 0x060057E0 RID: 22496 RVA: 0x001B57AC File Offset: 0x001B39AC
        public void PopFront()
        {
            this.method_0("Cannot take elements from an empty buffer.");
            this.gparam_0[this.int_0] = default(T);
            this.method_1(ref this.int_0);
            this.int_2--;
        }

        // Token: 0x060057E1 RID: 22497 RVA: 0x001B57F8 File Offset: 0x001B39F8
        public void Clear()
        {
            this.int_0 = 0;
            this.int_1 = 0;
            this.int_2 = 0;
            Array.Clear(this.gparam_0, 0, this.gparam_0.Length);
        }

        // Token: 0x060057E2 RID: 22498 RVA: 0x001B5824 File Offset: 0x001B3A24
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

        // Token: 0x060057E3 RID: 22499 RVA: 0x001B58A0 File Offset: 0x001B3AA0
        public IList<ArraySegment<T>> ToArraySegments()
        {
            return new ArraySegment<T>[]
            {
            this.method_4(),
            this.method_5()
            };
        }

        // Token: 0x060057E4 RID: 22500 RVA: 0x001B58C2 File Offset: 0x001B3AC2
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

        // Token: 0x060057E5 RID: 22501 RVA: 0x001B58D1 File Offset: 0x001B3AD1
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        // Token: 0x060057E6 RID: 22502 RVA: 0x001B58D9 File Offset: 0x001B3AD9
        private void method_0(string message = "Cannot access an empty buffer.")
        {
            if (this.IsEmpty)
            {
                throw new InvalidOperationException(message);
            }
        }

        // Token: 0x060057E7 RID: 22503 RVA: 0x001B58EC File Offset: 0x001B3AEC
        private void method_1(ref int index)
        {
            int num = index + 1;
            index = num;
            if (num == this.Capacity)
            {
                index = 0;
            }
        }

        // Token: 0x060057E8 RID: 22504 RVA: 0x001B590D File Offset: 0x001B3B0D
        private void method_2(ref int index)
        {
            if (index == 0)
            {
                index = this.Capacity;
            }
            index--;
        }

        // Token: 0x060057E9 RID: 22505 RVA: 0x001B5921 File Offset: 0x001B3B21
        private int method_3(int index)
        {
            return this.int_0 + ((index < this.Capacity - this.int_0) ? index : (index - this.Capacity));
        }

        // Token: 0x060057EA RID: 22506 RVA: 0x001B5948 File Offset: 0x001B3B48
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

        // Token: 0x060057EB RID: 22507 RVA: 0x001B59B8 File Offset: 0x001B3BB8
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

        // Token: 0x040056FC RID: 22268
        private readonly T[] gparam_0;

        // Token: 0x040056FD RID: 22269
        private int int_0;

        // Token: 0x040056FE RID: 22270
        private int int_1;

        // Token: 0x040056FF RID: 22271
        private int int_2;
    }
}
