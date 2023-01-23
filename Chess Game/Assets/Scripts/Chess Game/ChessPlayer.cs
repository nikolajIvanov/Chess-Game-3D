using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChessPlayer
{
    public TeamColor team { get; set; }
    public Board board { get; set; }
    public List<Piece> activePieces { get; private set; }
    // Konstruktor; ähnlich wie in Python def __init__(self):
    public ChessPlayer(TeamColor team, Board board)
    {
        this.board = board;
        this.team = team;
        activePieces = new List<Piece>();
    }

    public void AddPiece(Piece piece)
    {
        /*
         * Prüft ob die Figur, welche als Paramter übergeben wird, sich in der Liste mit den aktiven Figuren befindet.
         * Falls nicht, wird es der Liste hinzugefügt.
         */
        if (!activePieces.Contains(piece))
        {
            activePieces.Add(piece);
        }
    }
    
    public void RemovePiece(Piece piece)
    {
        /*
         * Selbe wie AddPiece, nur das er prüft, ob die Figur bereits in der Liste ist und aus der Liste entfernt wird
         */
        if (activePieces.Contains(piece))
        {
            activePieces.Remove(piece);
        }
    }

    public void GenerateAllPossibleMoves()
    {
        /*
         * Es wird über alle Figuren aus der Liste activePieces iteriert und es wird geprüft, ob sich die Figur auf dem
         * Feld befindet.
         * Wenn das der Fall ist, wird die Methode "SelectAvailableSquares" aufgerufen.
         *
         * Die Methode wird in der Klasse ChessGameController über die Methode GenerateAllPossibleMoves() aufgerufen.
         * GenerateAllPossibleMoves() wird von der Methode StartNewGame() aufgerufen. Hier wird der if-Befehl positiv
         * verlaufen, weil in der Methode StartNewGame() wird vorher die Methode CreatePiecesFromLayout() augerufen,
         * in der alle Figuren auf das Feld übertragen werden.
         */
        foreach (var piece in activePieces)
        {
            if (board.HasPiece(piece))
            {
                piece.SelectAvailableSquares();
            }
        }
    }
    
    public Piece[] GetPieceAtackingOppositePiceOfType<T>() where T : Piece
    {
        return activePieces.Where(p => p.IsAttackingPieceOfType<T>()).ToArray();
    }

    public Piece[] GetPiecesOfType<T>() where T : Piece
    {
        return activePieces.Where(p => p is T).ToArray();
    }

    public void RemoveMovesEnablingAttakOnPieceOfType<T>(ChessPlayer opponent, Piece selectedPiece) where T : Piece
    {
        List<Vector2Int> coordsToRemove = new List<Vector2Int>();

        coordsToRemove.Clear();
        foreach (var coords in selectedPiece.availableMoves)
        {
            Piece pieceOnCoords = board.GetPieceOnSquare(coords);
            board.UpdateBoardOnPieceMove(coords, selectedPiece.occupiedSquare, selectedPiece, null);
            opponent.GenerateAllPossibleMoves();
            if (opponent.CheckIfIsAttacigPiece<T>())
                coordsToRemove.Add(coords);
            board.UpdateBoardOnPieceMove(selectedPiece.occupiedSquare, coords, selectedPiece, pieceOnCoords);
        }
        foreach (var coords in coordsToRemove)
        {
            selectedPiece.availableMoves.Remove(coords);
        }

    }

    internal bool CheckIfIsAttacigPiece<T>() where T : Piece
    {
        foreach (var piece in activePieces)
        {
            if (board.HasPiece(piece) && piece.IsAttackingPieceOfType<T>())
                return true;
        }
        return false;
    }

    public bool CanHidePieceFromAttack<T>(ChessPlayer opponent) where T : Piece
    {
        foreach (var piece in activePieces)
        {
            foreach (var coords in piece.availableMoves)
            {
                Piece pieceOnCoords = board.GetPieceOnSquare(coords);
                board.UpdateBoardOnPieceMove(coords, piece.occupiedSquare, piece, null);
                opponent.GenerateAllPossibleMoves();
                if (!opponent.CheckIfIsAttacigPiece<T>())
                {
                    board.UpdateBoardOnPieceMove(piece.occupiedSquare, coords, piece, pieceOnCoords);
                    return true;
                }
                board.UpdateBoardOnPieceMove(piece.occupiedSquare, coords, piece, pieceOnCoords);
            }
        }
        return false;
    }

    internal void OnGameRestarted()
    {
        activePieces.Clear();
    }
}
