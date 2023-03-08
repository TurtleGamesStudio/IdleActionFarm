using UnityEngine;

public static class InterfaceImplementaion
{
    public static void Implement<T>(ref MonoBehaviour monoBehaviour)
    {
        if (monoBehaviour == null)
            return;

        if (monoBehaviour is T)
            return;

        Debug.LogWarning(monoBehaviour.name + " needs to implement " + typeof(T));
        monoBehaviour = null;
    }
}
