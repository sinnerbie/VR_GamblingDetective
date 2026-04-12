using UnityEngine;

public class LightsOutManager : MonoBehaviour
{
    [SerializeField] private LightTile[] allTiles;

    public void PressTile(LightTile pressedTile)
    {
        if (pressedTile == null) return;

        // Cambia los tiles definidos en la lista del botón pulsado
        foreach (var tile in pressedTile.AffectedTiles)
        {
            if (tile != null)
                tile.Toggle();
        }

        CheckWinCondition();
    }

    private void CheckWinCondition()
{
    foreach (var tile in allTiles)
    {
        if (tile != null && !tile.IsOn)
            return;
    }

    Debug.Log("¡Puzzle completado!");
}
}