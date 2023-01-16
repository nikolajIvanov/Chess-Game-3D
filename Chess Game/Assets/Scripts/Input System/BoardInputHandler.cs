using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Board))]
public class BoardInputHandler : MonoBehaviour, IInputHandler
{

    private Board board;

    private void Awake()
    {
        throw new NotImplementedException();
    }

    public void ProcessInput(Vector3 inputPosition, GameObject selectedObject, Action onClick)
    {
        throw new NotImplementedException();
    }
}
