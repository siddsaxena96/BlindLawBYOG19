using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A generic abstract Component pooler class to pool Component types.
/// </summary>
/// <typeparam name="T">The type of Component to pool.</typeparam>
public abstract class ComponentPool<T> : MonoBehaviour, IComponentPool<T> where T : Component
{
    [Tooltip("Should the component pool auto-initialize on start?")]
    [SerializeField] protected bool autoInitialize = true;
    [Tooltip("The initial count of pooled objects to pool on auto-initialize.")]
    [SerializeField] protected int initialPooledCount = DefaultInitialPooledCount;
    [Tooltip("The list of pooled components.")]
    [SerializeField] protected Queue<T> componentPool = new Queue<T>();

    private const int DefaultInitialPooledCount = 5;

    protected virtual void Reset()
    {
        autoInitialize = true;
        initialPooledCount = DefaultInitialPooledCount;
        componentPool = new Queue<T>();
    }

    protected virtual void Start()
    {
        if (autoInitialize)
        {
            InitializePool();
        }
    }

    /// <summary>
    /// Initializes the component pool.
    /// </summary>
    public void InitializePool()
    {
        if (initialPooledCount > 0)
        {
            for (int i = 0; i < initialPooledCount; i++)
            {
                CreateNewComponent();
            }
        }
    }

    /// <summary>
    /// Returns the component to the pool. The component is re-initialized.
    /// </summary>
    /// <param name="component">The Component to return to the pool.</param>
    public void ReturnToPool(T component)
    {
        InitializeComponent(component);
        componentPool.Enqueue(component);
    }

    public T Get()
    {
        if (componentPool.Count == 0)
        {
            CreateNewComponent();
        }

        return componentPool.Dequeue();
    }

    private T CreateNewComponent()
    {
        if (componentPool == null)
        {
            componentPool = new Queue<T>();
        }

        T component = CreateComponent();
        InitializeComponent(component);
        component.transform.SetParent(transform);
        component.name = typeof(T).Name;
        componentPool.Enqueue(component);
        if (component.GetComponent<IPoolable<T>>() != null)
            component.GetComponent<IPoolable<T>>().Pool = this;
        return component;
    }

    protected virtual T CreateComponent()
    {
        return new GameObject().AddComponent<T>();
    }

    protected virtual void InitializeComponent(T component)
    {
        component.gameObject.SetActive(false);
    }
}
