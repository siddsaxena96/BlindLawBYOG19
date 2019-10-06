using System;
using System.Collections;

public interface IPoolable<T>
{
    IComponentPool<T> Pool { get; set; }
    void Return();
}