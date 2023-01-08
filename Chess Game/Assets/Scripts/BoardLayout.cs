using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Gibt die Möglichkeit Scriptable Objects zu erstellen.
 * Dafür geht man in Unity bei den Assets Rechtsklick-> Create -> Scriptable Objects -> Board -> Layout
 * Wir nutzen das, damit zu Beginn die Figuren richtig aufs Board gesetzt werden 
*/
[CreateAssetMenu(menuName = "Scriptable Objects/Board/Layout")]
/*
 * Diese Klasse wird zu Beginn eines Schachspiels von der Klasse "ChessGameController" ausgeführt,
 * damit das Schachbrett mit allen nötigen Figuren geladen wird.
 */
public class BoardLayout : ScriptableObject
{
    /*
     * Warum wird diese Klasse in einer Klasse geschrieben?
     */
    [Serializable]
    private class BoardSquareSetup
    {
        public Vector2Int position;
        public PieceType pieceType;
        public TeamColor teamColor;
    }

    [SerializeField] private BoardSquareSetup[] boardSquares;

    public int GetPiecesCount()
    {
        return boardSquares.Length;
    }

    public Vector2Int GetSquareCoordsAtIndex(int index)
    {
        if (boardSquares.Length <= index)
        {
            Debug.LogError("Index of piece is out of range");
            return new Vector2Int(-1, -1);
        }

        return new Vector2Int(boardSquares[index].position.x - 1, boardSquares[index].position.y -1);
    }

    public string GetSquarePieceNameAtIndex(int index)
    {
        if (boardSquares.Length <= index)
        {
            Debug.LogError("Index of piece is out of range");
            return "";
        }

        return boardSquares[index].pieceType.ToString();
    }

    public TeamColor GetSquareTeamColorAtIndex(int index)
    {
        if (boardSquares.Length <= index)
        {
            Debug.LogError("Index of piece is out of range");
            return TeamColor.Black;
        }

        return boardSquares[index].teamColor;
    }
}
