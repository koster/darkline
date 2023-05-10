using System.Collections.Generic;
using UnityEngine;

namespace Source.Util
{
    public static class CMSExtensions
    {
        public static Sprite LoadSprite(this string path)
        {
            return Resources.Load<Sprite>(path);
        }
        
        public static T Load<T>(this string path) where T : Object
        {
            return Resources.Load<T>(path);
        }
        
        public static T New<T>(this List<T> path) where T : new()
        {
            var item = new T();
            path.Add(item);
            return item;
        }
    }
}