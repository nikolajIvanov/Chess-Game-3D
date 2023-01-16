using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Durch RequireComponent wird immer, wenn das Skript ChessGameController einem Objekt in Unity zugewiesen wird,
 * automatisch das Skript PiecesCreator auch als Komponente hinzugefügt.
 */
[RequireComponent(typeof(PiecesCreator))]
/*
 * Bezieht Daten vom BoardLayout.cs und basierend darauf, wird das Board erstellt.
 */
public class ChessGameController : MonoBehaviour
{

    [SerializeField] private BoardLayout startingBoardLayout;
    [SerializeField] private Board board; // Referenz zur Klasse Board
    
    private PiecesCreator pieceCreator; // Referenz zur Klasse PiecesCreator
    private ChessPlayer whitePlayer;
    private ChessPlayer blackPlayer;
    private ChessPlayer activePlayer;


    private void Awake()
    {
        SetDependencies();
        CreatePlayers();
    }

    private void CreatePlayers()
    {
        whitePlayer = new ChessPlayer(TeamColor.White, board);
        blackPlayer = new ChessPlayer(TeamColor.Black, board);
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
        activePlayer = whitePlayer; // Im Schach fängt der Spieler mit den weißen Figuren an
        GenerateAllPossibleMoves(activePlayer); // Wird benötigt, damit der Spieler direkt zu Beginn alle Züge nutzen kann
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
        newPiece.SetData(squareCoords, team, board);

        Material teamMaterial = pieceCreator.GetTeamMaterial(team);
        newPiece.SetMaterial(teamMaterial);
    }
    
    private void GenerateAllPossibleMoves(ChessPlayer player)
    {
        // Erklärung siehe Klasse ChessPlayer Methode GenerateAllPossibleMoves()
        player.GenerateAllPossibleMoves();
    }
}
