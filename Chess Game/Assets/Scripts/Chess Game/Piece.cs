using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(IObjectTweener))]
[RequireComponent(typeof(MaterialSetter))]
public abstract class Piece : MonoBehaviour
{
    [SerializeField] private MaterialSetter materialSetter; // Referenz zur Klasse MaterialSetter
    public Board board { protected get; set; }
    public Vector2Int occupiedSquare { get; set; }
    public TeamColor team { get; set; }
    public bool hasMoved { get; private set; }
    
    public List<Vector2Int> availableMoves; // Liste mit allen Bewegungsmöglichkeiten der einzelnen Figuren
    
    private IObjectTweener tweener;
    public abstract List<Vector2Int> SelectAvailableSquares();

    private void Awake()
    {
        availableMoves = new List<Vector2Int>();
        tweener = GetComponent<IObjectTweener>();
        materialSetter = GetComponent<MaterialSetter>();
        hasMoved = false;
    }

    public void SetMaterial(Material selectedMaterial)
    {
        materialSetter.SetSingleMaterial(selectedMaterial);
    }

    public bool IsFromSameTeam(Piece piece)
    {
        return team == piece.team;
    }

    public bool CanMoveTo(Vector2Int coords)
    {
        /*
         * Überprüft die Werte die als Paramter übergeben werden, ob es sich beim nächsten Zug um einen gültigen Zug
         * handelt (jeder Typ hat andere Bewegungsmöglichkeiten)
         */
        return availableMoves.Contains(coords);
    }

    public virtual void MovePiece(Vector2Int coords)
    {
        Vector3 targetPosition = board.CalculatePositionFromCoords(coords);
        occupiedSquare = coords;
        hasMoved = true;
        tweener.MoveTo(transform, targetPosition);
    }

    protected void TryToAddMove(Vector2Int coords)
    {
        availableMoves.Add(coords);
    }

    public void SetData(Vector2Int coords, TeamColor team, Board board)
    {
        this.team = team;
        occupiedSquare = coords;
        this.board = board;
        /* Setzt die transform position basierend auf übergebenen Koordinaten. Die Position wird vom Board Objekt
         * errechnet, da es das einzige Objekt ist, was die eigene Gräße kennt.
         */
        transform.position = board.CalculatePositionFromCoords(coords);
    }
}
