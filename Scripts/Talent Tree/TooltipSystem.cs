using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    // This script sits on the tooltip Canvas
    private static TooltipSystem current;

    public tooltipScript tooltip;

    public void Awake()
    {
        current = this;
    }

    //when called from the trigger, set the text and activate the tooltip
    public static void Show(int costValue, string cost, string content, string header = "")
    {
        current.tooltip.SetText(costValue, cost, content, header);
        current.tooltip.gameObject.SetActive(true);
    }

    //when called from the trigger, deactivate the tooltip
    public static void Hide()
    {
        current.tooltip.gameObject.SetActive(false);
    }

}
