using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputHandler
{
    // Übernimmt um welche Position es sich handelt, welche Figur ausgewählt wurde und was damit getan werden soll
    void ProcessInput(Vector3 inputPosition, GameObject selectedObject, Action onClick);
}
