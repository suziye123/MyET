using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ReferenceCollectorEx  {

    public static GameObject Get(this ReferenceCollector rc,string ResName)
    {
        return rc.Get<GameObject>($"{ResName}");
    }

    public static T GetComponent<T>(this ReferenceCollector rc,string ResName)
    {
        return rc.Get<GameObject>($"{ResName}").GetComponent<T>();
    }
}
