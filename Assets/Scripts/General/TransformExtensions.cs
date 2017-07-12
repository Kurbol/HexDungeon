using UnityEngine;

public static class TransformExtensions
{
    public static Transform GetChild(this Transform transform, string childName)
    {
        var child = transform.Find(childName);
        if (child == null)
        {
            child = new GameObject(childName).transform;
            child.parent = transform;
        }

        return child;
    }
}