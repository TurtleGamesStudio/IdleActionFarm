using System.Collections.Generic;
using UnityEngine;

public interface IBagView
{
    public IReadOnlyList<Transform> Places { get; }

    public void Init(int capacity);
}
