using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class tooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //This script is put on objects that havea tooltip
    // Variables to input text
    public string content;
    public string header;
    public string cost;
    public int costValue;

    //On Mouse Over Start access Toltip System and show tooltip with the text
    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipSystem.Show(costValue,cost,content,header);
    }
    //On Mouse over hide the tooltip
    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Hide();
    }
    //On Disabling hide the tooltip
    public void OnDisable()
    {
        TooltipSystem.Hide();
    }
}
