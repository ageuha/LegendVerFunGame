using System;
using System.Collections.Generic;

namespace Member.YDW.Script.PathFinder
{
    public class PriorityQueue<T> where T : IComparable<T>
    { 

        public List<T> heap = new();
    
        public int Count => heap.Count;

        public void Clear() => heap?.Clear();

        public T Contains(T target)
        {
            int idx = heap.IndexOf(target);
            if (idx < 0) return default;
            return heap[idx];
        }

        public void Push(T data)
        {
            heap.Add(data);
            int now = heap.Count - 1; // ���� �߰��� "��"�� ��ġ
            while (now > 0)
            {
                int next = (now - 1) / 2;
                if (heap[now].CompareTo(heap[next]) < 0)
                {
                    break;
                }

                (heap[now], heap[next]) = (heap[next], heap[now]);
                now = next;
            }
        }

        public T Pop()
        {
            T ret = heap[0];
            int lastIndex = heap.Count - 1;
            heap[0] = heap[lastIndex];
            heap.RemoveAt(lastIndex);
            lastIndex--;

            int now = 0;
            while (true)
            {
                int left = 2 * now + 1;
                int right = 2 * now + 2;
                int next = now;

                if (left <= lastIndex && heap[next].CompareTo(heap[left]) < 0)
                    next = left;

                if(right <= lastIndex && heap[next].CompareTo(heap[right]) < 0)
                    next = right;
                if(next == now)
                    break;

                (heap[now], heap[next]) = (heap[next], heap[now]);
                now = next;

            
            }
            return ret;
        }

        public T Peak()
        {
            return heap.Count == 0? default : heap[0];
        }

    }
}