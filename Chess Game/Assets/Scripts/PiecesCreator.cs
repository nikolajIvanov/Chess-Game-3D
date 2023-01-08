using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecesCreator : MonoBehaviour
{
    [SerializeField] private GameObject[] piecesPrefabs;
    [SerializeField] private Material blackMaterial;
    [SerializeField] private Material whiteMaterial;
    
    // Wird zum mappgen von Prefab piece type name zu dem prefab. Hilft uns die richtige Figur zu finden
    private Dictionary<string, GameObject> nameToPieceDict = new Dictionary<string, GameObject>();

    private void Awake()
    {
        // Das Dict wird befüllt mit Piece type names als Schlüssel und Piece Prefabs (Figuren Prefabs) als Wert
        foreach (var piece in piecesPrefabs)
        {
            nameToPieceDict.Add(piece.GetComponent<Piece>().GetType().ToString(), piece);
        }
    }

    public GameObject CreatePiece(Type type)
    {
        // Zuerst finden wir das das richtige Figuren Prefab basierend auf dem Typ, welcher als Parameter übergeben wird
        GameObject prefab = nameToPieceDict[type.ToString()];
        //TODO Versteh ich nicht ganz
        // Wenn der richtige Prefab gefunden wird, wird dieser als Rückgabewert übergeben, andernfall null
        if (prefab)
        {
            GameObject newPiece = Instantiate(prefab);
            return newPiece;
        }

        return null;
    }

    public Material GetTeamMaterial(TeamColor team)
    {
        // Übergabe der Farbe; Abhängig vom Parameter team 
        return team == TeamColor.White ? whiteMaterial : blackMaterial;
    }
}
