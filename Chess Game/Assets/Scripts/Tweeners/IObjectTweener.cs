using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal interface IObjectTweener
{
    void MoveTo(Transform transform, Vector3 targetPosition);
}
