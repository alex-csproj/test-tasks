﻿using System.Collections.Generic;

namespace TestTasks.T9
{
    internal static class IReadOnlyListExtensions
    {
        public static int IndexOf<T>(this IReadOnlyList<T> list, T item)
        {
            for (var i = 0; i < list.Count; i++)
                if (list[i].Equals(item))
                    return i;

            return -1;
        }
    }
}