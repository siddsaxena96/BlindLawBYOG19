using System;
using System.Collections;

public interface IComponentPool<T>
{
    void ReturnToPool(T poolable);
}