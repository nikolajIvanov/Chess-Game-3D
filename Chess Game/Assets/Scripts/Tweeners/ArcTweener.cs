using DG.Tweening; // Hierbei handelt es sich um ein extra Package namens DOTween HOTween V2
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcTweener : MonoBehaviour, IObjectTweener
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpHeight;

    public void MoveTo(Transform transform, Vector3 targetPosition)
    {
        float distance = Vector3.Distance(targetPosition, transform.position);
        transform.DOJump(targetPosition, jumpHeight, 1, distance / movementSpeed);
    }
}