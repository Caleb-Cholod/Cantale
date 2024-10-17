using UnityEngine;
using UnityEngine.UI;

public class TooltipManager : MonoBehaviour
{
    public GameObject tooltipPrefab;   // Tooltip prefab with a Text component
    private GameObject tooltipInstance;
    private Text tooltipText;
    private RectTransform tooltipRect;

    private void Start()
    {
        // Instantiate the tooltip, but don't show it immediately
        tooltipInstance = Instantiate(tooltipPrefab, transform);
        tooltipText = tooltipInstance.GetComponentInChildren<Text>();
        tooltipRect = tooltipInstance.GetComponent<RectTransform>();
        tooltipInstance.SetActive(false);
    }

    // Method to show the tooltip at the mouse position
    public void ShowTooltip(string message, Vector3 position)
    {
        tooltipInstance.SetActive(true);
        tooltipText.text = message;

        // Move tooltip to the mouse position
        Vector3 screenPosition = Input.mousePosition;
        tooltipRect.position = screenPosition;
    }

    // Method to hide the tooltip
    public void HideTooltip()
    {
        tooltipInstance.SetActive(false);
    }
}
