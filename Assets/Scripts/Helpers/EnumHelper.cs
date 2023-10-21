using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace TarkovServerU19.Assets.Scripts.Helpers
{
    public static class EnumHelper<T> where T : struct, Enum
    {
        // Token: 0x06003108 RID: 12552 RVA: 0x000DA960 File Offset: 0x000D8B60
        static EnumHelper()
        {
            switch (EnumHelper<T>.TypeCode)
            {
                case TypeCode.Byte:
                    EnumHelper<T>.func_0 = EnumHelper<T>.smethod_5<byte>();
                    return;
                case TypeCode.Int16:
                    EnumHelper<T>.func_1 = EnumHelper<T>.smethod_5<short>();
                    return;
                case TypeCode.Int32:
                    EnumHelper<T>.func_2 = EnumHelper<T>.smethod_5<int>();
                    return;
            }
            throw EnumHelper<T>.smethod_4();
        }

        // Token: 0x1700075D RID: 1885
        // (get) Token: 0x06003109 RID: 12553 RVA: 0x000DAA44 File Offset: 0x000D8C44
        private static string[] String_0
        {
            get
            {
                string[] result;
                if ((result = EnumHelper<T>.string_1) == null)
                {
                    result = (EnumHelper<T>.string_1 = EnumHelper<T>.Names.Select(new Func<string, string>(EnumHelper<T>.Class385.class385_0.method_0)).ToArray<string>());
                }
                return result;
            }
        }

        // Token: 0x1700075E RID: 1886
        // (get) Token: 0x0600310A RID: 12554 RVA: 0x000DAA83 File Offset: 0x000D8C83
        private static IReadOnlyDictionary<string, T> IReadOnlyDictionary_0
        {
            get
            {
                if (EnumHelper<T>.dictionary_0 == null)
                {
                    EnumHelper<T>.smethod_1();
                }
                return EnumHelper<T>.dictionary_0;
            }
        }

        // Token: 0x1700075F RID: 1887
        // (get) Token: 0x0600310B RID: 12555 RVA: 0x000DAA96 File Offset: 0x000D8C96
        private static IReadOnlyDictionary<T, string> IReadOnlyDictionary_1
        {
            get
            {
                if (EnumHelper<T>.dictionary_1 == null)
                {
                    EnumHelper<T>.smethod_2();
                }
                return EnumHelper<T>.dictionary_1;
            }
        }

        // Token: 0x0600310C RID: 12556 RVA: 0x000DAAAC File Offset: 0x000D8CAC
        public static int IndexOf(T value)
        {
            for (int i = 0; i < EnumHelper<T>.Count; i++)
            {
                if (EnumHelper<T>.EqualityComparer.Equals(value, EnumHelper<T>.Values[i]))
                {
                    return i;
                }
            }
            return -1;
        }

        // Token: 0x0600310D RID: 12557 RVA: 0x000DAAE4 File Offset: 0x000D8CE4
        public static byte GetByteFromValue(T value)
        {
            switch (EnumHelper<T>.TypeCode)
            {
                case TypeCode.Byte:
                    return EnumHelper<T>.func_0(value);
                case TypeCode.Int16:
                    return (byte)EnumHelper<T>.func_1(value);
                case TypeCode.Int32:
                    return (byte)EnumHelper<T>.func_2(value);
            }
            throw EnumHelper<T>.smethod_4();
        }

        // Token: 0x0600310E RID: 12558 RVA: 0x000DAB3C File Offset: 0x000D8D3C
        public static short GetShortFromValue(T value)
        {
            switch (EnumHelper<T>.TypeCode)
            {
                case TypeCode.Byte:
                    return (short)EnumHelper<T>.func_0(value);
                case TypeCode.Int16:
                    return EnumHelper<T>.func_1(value);
                case TypeCode.Int32:
                    return (short)EnumHelper<T>.func_2(value);
            }
            throw EnumHelper<T>.smethod_4();
        }

        // Token: 0x0600310F RID: 12559 RVA: 0x000DAB94 File Offset: 0x000D8D94
        public static int GetIntFromValue(T value)
        {
            switch (EnumHelper<T>.TypeCode)
            {
                case TypeCode.Byte:
                    return (int)EnumHelper<T>.func_0(value);
                case TypeCode.Int16:
                    return (int)EnumHelper<T>.func_1(value);
                case TypeCode.Int32:
                    return EnumHelper<T>.func_2(value);
            }
            throw EnumHelper<T>.smethod_4();
        }

        // Token: 0x06003110 RID: 12560 RVA: 0x000DABEA File Offset: 0x000D8DEA
        public static T GetValue(int integer)
        {
            return (T)((object)integer);
        }

        // Token: 0x06003111 RID: 12561 RVA: 0x000DABF7 File Offset: 0x000D8DF7
        public static Dictionary<T, TValue> GetDictWith<TValue>()
        {
            return new Dictionary<T, TValue>(EnumHelper<T>.Count, EnumHelper<T>.EqualityComparer);
        }

        // Token: 0x06003112 RID: 12562 RVA: 0x000DAC08 File Offset: 0x000D8E08
        public static bool TryDeserializeValue(string name, out T value)
        {
            if (EnumHelper<T>.IReadOnlyDictionary_0.TryGetValue(name, out value))
            {
                return true;
            }
            for (int i = 0; i < EnumHelper<T>.Count; i++)
            {
                if (EnumHelper<T>.Names[i] == name)
                {
                    value = EnumHelper<T>.Values[i];
                    return true;
                }
            }
            Debug.LogError(string.Concat(new string[]
            {
            "Enum \"",
            typeof(T).Name,
            "\" does not contain any value named \"",
            name,
            "\"."
            }));
            value = default(T);
            return false;
        }

        // Token: 0x06003113 RID: 12563 RVA: 0x000DACA4 File Offset: 0x000D8EA4
        public static string GetName(T value)
        {
            int num = EnumHelper<T>.IndexOf(value);
            if (num >= 0)
            {
                return EnumHelper<T>.Names[num];
            }
            string text = value.ToString();
            Debug.LogError("Attempt to get name of undefined " + EnumHelper<T>.Type.Name + " " + text);
            return text;
        }

        // Token: 0x06003114 RID: 12564 RVA: 0x000DACF8 File Offset: 0x000D8EF8
        public static string GetLocalizationKey(T value)
        {
            EnumHelper<T>.smethod_3();
            int num = EnumHelper<T>.IndexOf(value);
            if (num >= 0)
            {
                return EnumHelper<T>.string_0[num];
            }
            string text = value.ToString();
            Debug.LogError("Attempt to localize undefined " + EnumHelper<T>.Type.Name + " " + text);
            return text;
        }

        // Token: 0x06003115 RID: 12565 RVA: 0x000DAD4C File Offset: 0x000D8F4C
        public static IReadOnlyList<string> GetLocalizedNames()
        {
            EnumHelper<T>.smethod_3();
            string[] array = new string[EnumHelper<T>.Count];
            for (int i = 0; i < EnumHelper<T>.string_0.Length; i++)
            {
                array[i] = EnumHelper<T>.string_0[i]; //.Localized(null);
            }
            return array;
        }

        // Token: 0x06003116 RID: 12566 RVA: 0x000DAD8C File Offset: 0x000D8F8C
        public static string SerializeValue(T value)
        {
            return EnumHelper<T>.IReadOnlyDictionary_1[value];
        }

        // Token: 0x06003117 RID: 12567 RVA: 0x000DAD9C File Offset: 0x000D8F9C
        public static bool HasFlag(T value, T flag)
        {
            int intFromValue = EnumHelper<T>.GetIntFromValue(value);
            int intFromValue2 = EnumHelper<T>.GetIntFromValue(flag);
            return (intFromValue & intFromValue2) == intFromValue2;
        }

        // Token: 0x06003118 RID: 12568 RVA: 0x000DADBC File Offset: 0x000D8FBC
        public static bool IsDefined(T value)
        {
            foreach (T y in EnumHelper<T>.Values)
            {
                if (EnumHelper<T>.EqualityComparer.Equals(value, y))
                {
                    return true;
                }
            }
            return false;
        }

        // Token: 0x06003119 RID: 12569 RVA: 0x000DAE18 File Offset: 0x000D9018
        [CanBeNull]
        private static U smethod_0<U>([NotNull] string name)
        {
            return (U)((object)EnumHelper<T>.Type.GetMember(name).First<MemberInfo>().GetCustomAttributes(typeof(U), false).FirstOrDefault<object>());
        }

        // Token: 0x0600311A RID: 12570 RVA: 0x000DAE44 File Offset: 0x000D9044
        private static void smethod_1()
        {
            int i = 0;
            EnumHelper<T>.dictionary_0 = new Dictionary<string, T>(EnumHelper<T>.Count);
            try
            {
                for (i = 0; i < EnumHelper<T>.Count; i++)
                {
                    EnumHelper<T>.dictionary_0.Add(EnumHelper<T>.String_0[i], EnumHelper<T>.Values[i]);
                }
            }
            catch
            {
                throw new Exception(string.Concat(new string[]
                {
                "Enum \"",
                typeof(T).Name,
                "\"(",
                EnumHelper<T>.String_0[i],
                ") has equal serialization names."
                }));
            }
        }

        // Token: 0x0600311B RID: 12571 RVA: 0x000DAEE8 File Offset: 0x000D90E8
        private static void smethod_2()
        {
            int i = 0;
            EnumHelper<T>.dictionary_1 = EnumHelper<T>.GetDictWith<string>();
            try
            {
                for (i = 0; i < EnumHelper<T>.Count; i++)
                {
                    EnumHelper<T>.dictionary_1.Add(EnumHelper<T>.Values[i], EnumHelper<T>.String_0[i]);
                }
            }
            catch
            {
                throw new Exception(string.Concat(new string[]
                {
                "Enum \"",
                typeof(T).Name,
                "\"(",
                EnumHelper<T>.Names[i],
                ") has values with equal hashes."
                }));
            }
        }

        // Token: 0x0600311C RID: 12572 RVA: 0x000DAF8C File Offset: 0x000D918C
        private static void smethod_3()
        {
            if (EnumHelper<T>.string_0 == null)
            {
                EnumHelper<T>.string_0 = EnumHelper<T>.Names.Select(new Func<string, string>(EnumHelper<T>.Class385.class385_0.method_1)).ToArray<string>();
            }
        }

        // Token: 0x0600311D RID: 12573 RVA: 0x000DAFC8 File Offset: 0x000D91C8
        private static ArgumentException smethod_4()
        {
            return new ArgumentException("EnumHelper can work correct only with Byte/Int16/Int32-based enums, " + string.Format("{0} is based on {1}", EnumHelper<T>.Type.Name, EnumHelper<T>.TypeCode));
        }

        // Token: 0x0600311E RID: 12574 RVA: 0x000DAFF8 File Offset: 0x000D91F8
        [CompilerGenerated]
        internal static Func<T, U> smethod_5<U>()
        {
            ParameterExpression parameterExpression = Expression.Parameter(typeof(T), null);
            return Expression.Lambda<Func<T, U>>(Expression.ConvertChecked(parameterExpression, typeof(U)), new ParameterExpression[]
            {
            parameterExpression
            }).Compile();
        }

        // Token: 0x04002AA4 RID: 10916
        public static readonly Type Type = typeof(T);

        // Token: 0x04002AA5 RID: 10917
        public static readonly ReadOnlyCollection<T> Values = Array.AsReadOnly<T>((T[])Enum.GetValues(typeof(T)));

        // Token: 0x04002AA6 RID: 10918
        public static readonly ReadOnlyCollection<string> Names = Array.AsReadOnly<string>(Enum.GetNames(typeof(T)));

        // Token: 0x04002AA7 RID: 10919
        public static readonly int Count = EnumHelper<T>.Values.Count;

        // Token: 0x04002AA8 RID: 10920
        public static readonly TypeCode TypeCode = Convert.GetTypeCode(EnumHelper<T>.Values.First<T>());

        // Token: 0x04002AA9 RID: 10921
        public static readonly Type UnderlyingType = Enum.GetUnderlyingType(EnumHelper<T>.Type);

        // Token: 0x04002AAA RID: 10922
        public static readonly IComparer<T> Comparer = new EnumHelper<T>.Class383();

        // Token: 0x04002AAB RID: 10923
        public static readonly IEqualityComparer<T> EqualityComparer = new EnumHelper<T>.Class384();

        // Token: 0x04002AAC RID: 10924
        private static string[] string_0;

        // Token: 0x04002AAD RID: 10925
        private static string[] string_1;

        // Token: 0x04002AAE RID: 10926
        private static Dictionary<string, T> dictionary_0;

        // Token: 0x04002AAF RID: 10927
        private static Dictionary<T, string> dictionary_1;

        // Token: 0x04002AB0 RID: 10928
        private static readonly Func<T, byte> func_0;

        // Token: 0x04002AB1 RID: 10929
        private static readonly Func<T, short> func_1;

        // Token: 0x04002AB2 RID: 10930
        private static readonly Func<T, int> func_2;

        // Token: 0x02000740 RID: 1856
        private sealed class Class383 : IComparer<T>
        {
            // Token: 0x0600311F RID: 12575 RVA: 0x000DB03C File Offset: 0x000D923C
            public int Compare(T x, T y)
            {
                return EnumHelper<T>.GetIntFromValue(x).CompareTo(EnumHelper<T>.GetIntFromValue(y));
            }
        }

        // Token: 0x02000741 RID: 1857
        private sealed class Class384 : IEqualityComparer<T>
        {
            // Token: 0x06003121 RID: 12577 RVA: 0x000DB05D File Offset: 0x000D925D
            public bool Equals(T x, T y)
            {
                return EnumHelper<T>.GetIntFromValue(x) == EnumHelper<T>.GetIntFromValue(y);
            }

            // Token: 0x06003122 RID: 12578 RVA: 0x000DB06D File Offset: 0x000D926D
            public int GetHashCode(T obj)
            {
                return EnumHelper<T>.GetIntFromValue(obj);
            }
        }

        // Token: 0x02000742 RID: 1858
        [CompilerGenerated]
        [Serializable]
        private sealed class Class385
        {
            // Token: 0x06003126 RID: 12582 RVA: 0x000DB081 File Offset: 0x000D9281
            internal string method_0(string originalName)
            {
                OriginalNameAttribute gattribute = EnumHelper<T>.smethod_0<OriginalNameAttribute>(originalName);
                string result;
                if (gattribute != null)
                {
                    if ((result = gattribute.Name) != null)
                    {
                        return result;
                    }
                }
                result = originalName;
                return result;
            }

            // Token: 0x06003127 RID: 12583 RVA: 0x000DB09A File Offset: 0x000D929A
            internal string method_1(string name)
            {
                return EnumHelper<T>.Type.Name + "/" + name;
            }

            // Token: 0x04002AB3 RID: 10931
            public static readonly EnumHelper<T>.Class385 class385_0 = new EnumHelper<T>.Class385();

            // Token: 0x04002AB4 RID: 10932
            public static Func<string, string> func_0;

            // Token: 0x04002AB5 RID: 10933
            public static Func<string, string> func_1;
        } 
    }
}
