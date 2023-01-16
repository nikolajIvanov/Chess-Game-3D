using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Vermutung: In dieser Klasse wird geprüft welches Feld auf dem Brett gedrückt wurde 
 */
public class ColliderInputRecieved : InputReciever
{
    private Vector3 clickPosition;

    private void Update()
    {
        // Es wird überprüft ob der linke Mausbutton geklickt wurde
        if (Input.GetMouseButtonDown(0))
        {
            // es wird ein Ray von der Kamera auf die besagte Position gebracht
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                clickPosition = hit.point;
                OnInputRecieved();
            }
        }
    }

    /*
     * Wird von der Abstrakten Klasse InputReciever vererbt. Da die Methode selbst Abstrakt war, muss diese hier
     * überschrieben werden
     */
    public override void OnInputRecieved()
    {
        foreach (var handler in inputHandlers)
        {
            handler.ProcessInput(clickPosition, null, null);
        }
    }
}
