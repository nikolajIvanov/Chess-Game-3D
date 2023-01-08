using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PiecesCreator))]
/*
 * Bezieht Daten vom BoardLayout.cs und basierend darauf, wird das Board erstellt.
 */
public class ChessGameController : MonoBehaviour
{

    [SerializeField] private BoardLayout startingBoardLayout;

    private PiecesCreator pieceCreator; // Referenz zur Klasse PiecesCreator

    private void Awake()
    {
        SetDependencies();
    }

    private void SetDependencies()
    {
        /* Weil der PieceCreator eine Komponente des ChessGameControllers wird, wird dieser in der Awake Methode
         * gechached.
         */
        pieceCreator = GetComponent<PiecesCreator>();
    }

    // Jedes Spiel wird aus dieser Klasse gestartet
    void Start()
    {
        StartNewGame();
    }

    private void StartNewGame()
    {
        CreatePiecesFromLayout(startingBoardLayout);
    }

    // Hier wird das Boardlayout erstellt
    // Paramter: BoardLayout
    private void CreatePiecesFromLayout(BoardLayout layout)
    {
        // Jeder Block ist eine separate Variable
        for (int i = 0; i < layout.GetPiecesCount(); i++)
        {
            Vector2Int squareCoords = layout.GetSquareCoordsAtIndex(i);
            TeamColor team = layout.GetSquareTeamColorAtIndex(i);
            string typeName = layout.GetSquarePieceNameAtIndex(i);

            Type type = Type.GetType(typeName);
            CreatePieceAndInitialize(squareCoords, team, type);
        }
    }
    // Erstellt eine einzige Figur und initalisiert diese mit den oben genannten Daten
    private void CreatePieceAndInitialize(Vector2Int squareCoords, TeamColor team, Type type)
    {
        // Im Übergabeparamter (type) wird festgestellt, welches Prefab ausgewählt werden soll
        Piece newPiece = pieceCreator.CreatePiece(type).GetComponent<Piece>();
    }
}
