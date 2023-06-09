using System.Collections.Generic;
using UnityEngine;

namespace Source.Util
{
    public static class MathExtensions
    {
        public static bool Roll(this float v)
        {
            if (v == 0) return false;
            return Random.Range(0f, 1f) < v;
        }
        
        public static float C01(this float v)
        {
            return Mathf.Clamp01(v);
        }
        
        public static float Abs(this float v)
        {
            return Mathf.Abs(v);
        }
        
        public static int Abs(this int v)
        {
            return Mathf.Abs(v);
        }
        
        public static float C(this float v, float a, float b)
        {
            return Mathf.Clamp(v, a, b);
        }
        
        public static T GetRandom<T>(this List<T> v)
        {
            return v[Random.Range(0, v.Count)];
        }
        
        public static T PopRandom<T>(this List<T> v)
        {
            var idx = Random.Range(0, v.Count);
            var popRandom = v[idx];
            v.RemoveAt(idx);
            return popRandom;
        }
        
        public static T GetSafely<T>(this List<T> v, int n)
        {
            if (v.Count == 0)
                return default(T);
            return v[Mathf.Clamp(n, 0, v.Count - 1)];
        }
    }
}