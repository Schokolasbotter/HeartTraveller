using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class tooltipScript : MonoBehaviour
{
    //this script sits on the tooltip object

    //text objects
    public Text headerField;
    public Text contentField;
    public Text costField;
    public Text buyableField;

    public PlayerController playercontroller;
    public LayoutElement layoutElement;
    public int characterWrapLimit;

    // set the text from the tooltipSystem script
    public void SetText(int costValue, string cost, string content, string header = "")
    {
        //if Header is empty deactivate the header
        if(string.IsNullOrEmpty(header))
        {
            headerField.gameObject.SetActive(false);
        }
        else // Otherwise activate header and set the text
        {
            headerField.gameObject.SetActive(true);
            headerField.text = header;
        }

        //Set the text of content and cost
        contentField.text = content;
        costField.text = cost;

        //Show buyable text if not enough Oxygen
        if(playercontroller.oxygenCounter.Oxygen < costValue)
        {
            buyableField.gameObject.SetActive(true);
        }
        else
        {
            buyableField.gameObject.SetActive(false);
        }

        //Get the length of the texts
        int headerLength = headerField.text.Length;
        int contentLength = contentField.text.Length;

        //If the text length of either header or content exceeds the wrap limit activate layoutElement, which sets a maximum with of the tooltip
        layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit) ? true : false;
    }

    private void Update()
    {
        //Get Mouse Position
        Vector2 position = Input.mousePosition;
        //Set Tooltip Position to Mouse Position (Pivot Point)
        transform.position = position;
    }

}
