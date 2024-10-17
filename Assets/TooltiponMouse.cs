using UnityEngine;

public class ObjectWithTooltip : MonoBehaviour
{
    public string tooltipMessage = "This is a tooltip";   // Message for the tooltip
    private TooltipManager tooltipManager;

    private void Start()
    {
        tooltipManager = FindObjectOfType<TooltipManager>();  // Find the TooltipManager in the scene
    }

    private void OnMouseOver()
    {
        Debug.Log("showing TT");
        // Show the tooltip when the mouse is over the GameObject
        tooltipManager.ShowTooltip(tooltipMessage, Input.mousePosition);
    }

    private void OnMouseExit()
    {
        // Hide the tooltip when the mouse is no longer over the GameObject
        tooltipManager.HideTooltip();
    }
}
