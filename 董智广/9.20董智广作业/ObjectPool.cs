using System.Collections.Generic;
using System;

public class ObjectPool<T>
{
    // ���ö����б���ų��пɹ����õĶ���
    private readonly List<T> availableObjects = new();

    // ������б���ŵ�ǰ����ʹ�õĶ���
    private readonly List<T> activeObjects = new();

    // ���������ڱ�֤�̰߳�ȫ
    private readonly object lockObject = new();

    // ��ȡ����ķ���
    public T GetObject()
    {
        // ������ǰ����ȷ���̰߳�ȫ
        lock (lockObject)
        {
            // ���û�п��õĶ����׳��쳣
            if (availableObjects.Count == 0)
                throw new InvalidOperationException("No objects available");

            // �ӿ��ö����б��л�ȡ��һ������
            var obj = availableObjects[0];
            // �ӿ��ö����б����Ƴ��ö���
            availableObjects.RemoveAt(0);
            // ���ö�����ӵ�������б���
            activeObjects.Add(obj);
            // ���ػ�ȡ���Ķ���
            return obj;
        }
    }

    // �黹����ķ���
    public void ReturnObject(T obj)
    {
        // ������ǰ����ȷ���̰߳�ȫ
        lock (lockObject)
        {
            // ���Դӻ�����б����Ƴ��ö������ʧ�����׳��쳣
            if (!activeObjects.Remove(obj))
                throw new InvalidOperationException("Object not from this pool");

            // ��������ӻؿ��ö����б�
            availableObjects.Add(obj);
        }
    }
}
