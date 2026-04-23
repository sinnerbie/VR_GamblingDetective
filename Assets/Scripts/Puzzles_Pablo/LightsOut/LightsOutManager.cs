using UnityEngine;

public class LightsOutManager : MonoBehaviour
{
    [SerializeField] private LightTile[] allTiles;

    public void PressTile(LightTile pressedTile)
    {
        if (pressedTile == null) return;

        foreach (var tile in pressedTile.AffectedTiles)
        {
            if (tile != null)
                tile.Toggle();
        }

        CheckWinCondition();
    }

    private void CheckWinCondition()
    {
        bool allOn = true;

        foreach (var tile in allTiles)
        {
            if (tile != null && !tile.IsOn)
            {
                allOn = false;
                break;
            }
        }

        foreach (var tile in allTiles)
        {
            if (tile != null)
                tile.SetCompletedVisual(allOn);
        }

        if (allOn)
            Debug.Log("¡Puzzle completado!");
    }
}