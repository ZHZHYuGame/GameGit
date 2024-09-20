using System.Collections.Generic;
using System;

public class ObjectPool<T>
{
    // 可用对象列表，存放池中可供借用的对象
    private readonly List<T> availableObjects = new();

    // 活动对象列表，存放当前正在使用的对象
    private readonly List<T> activeObjects = new();

    // 锁对象，用于保证线程安全
    private readonly object lockObject = new();

    // 获取对象的方法
    public T GetObject()
    {
        // 锁定当前对象，确保线程安全
        lock (lockObject)
        {
            // 如果没有可用的对象，抛出异常
            if (availableObjects.Count == 0)
                throw new InvalidOperationException("No objects available");

            // 从可用对象列表中获取第一个对象
            var obj = availableObjects[0];
            // 从可用对象列表中移除该对象
            availableObjects.RemoveAt(0);
            // 将该对象添加到活动对象列表中
            activeObjects.Add(obj);
            // 返回获取到的对象
            return obj;
        }
    }

    // 归还对象的方法
    public void ReturnObject(T obj)
    {
        // 锁定当前对象，确保线程安全
        lock (lockObject)
        {
            // 尝试从活动对象列表中移除该对象，如果失败则抛出异常
            if (!activeObjects.Remove(obj))
                throw new InvalidOperationException("Object not from this pool");

            // 将对象添加回可用对象列表
            availableObjects.Add(obj);
        }
    }
}
