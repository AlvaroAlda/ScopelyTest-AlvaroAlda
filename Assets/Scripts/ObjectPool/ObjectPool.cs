using System;
using System.Collections.Generic;

namespace ObjectPool
{
    public class ObjectPool<T>
    {
        private readonly Func<T> _factoryMethod;
        private readonly Stack<T> _objectPool;
        
        public ObjectPool(Func<T> factoryMethod)
        {
            _factoryMethod = factoryMethod;
            _objectPool = new Stack<T>();
        }
        
        public T Get() => _objectPool.Count != 0 ? _objectPool.Pop() : _factoryMethod();
        
        public void Push(T obj)
        { 
            _objectPool.Push(obj);
        }
    }
}