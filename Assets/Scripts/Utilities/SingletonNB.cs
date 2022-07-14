using Unity.Netcode;
using UnityEngine;


/// <summary>
/// Singleton class
/// </summary>
/// <typeparam name="T">Type of the singleton</typeparam>
public abstract class SingletonNB<T> : NetworkBehaviour where T : SingletonNB<T>
{
    /// <summary>
    /// The static reference to the instance
    /// </summary>
    public static T Instance { get; protected set; }

    /// <summary>
    /// Gets whether an instance of this singleton exists
    /// </summary>
    public static bool InstanceExists => Instance != null;

    /// <summary>
    /// Awake method to associate singleton with instance
    /// </summary>
    protected virtual void Awake()
    {
        if (InstanceExists)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = (T) this;
        }
    }

    /// <summary>
    /// OnDestroy method to clear singleton association
    /// </summary>
    public override void OnDestroy()
    {
        base.OnDestroy();
        if (Instance == this)
        {
            Instance = null;
        }
    }
}