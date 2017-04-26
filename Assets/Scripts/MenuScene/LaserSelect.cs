using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LaserSelect : MonoBehaviour
{
    public LayerMask selectableLayers;
    private GameObject pointerDest;
    private bool clicked = false;

    private void activateObject(GameObject ob)
    {
        var pe = new PointerEventData(EventSystem.current);
        ExecuteEvents.Execute(ob, pe, ExecuteEvents.pointerEnterHandler);
    }
    private void deactivateObject(GameObject ob)
    {
        var pe = new PointerEventData(EventSystem.current);
        ExecuteEvents.Execute(ob, pe, ExecuteEvents.pointerExitHandler);
    }
    private void clickObject(GameObject ob)
    {
        var pe = new PointerEventData(EventSystem.current);
        ExecuteEvents.Execute(ob, pe, ExecuteEvents.submitHandler);
    }

    private void FixedUpdate()
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.forward, 100.0f, selectableLayers);
        if (hits.Length > 0)
        {
            int closestHit = 0;
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].distance < hits[closestHit].distance) closestHit = i;
            }
    
            if (hits[closestHit].transform.gameObject != pointerDest)
            {
                // Inform objects that a new one is active
                if (pointerDest != null) deactivateObject(pointerDest);
                pointerDest = hits[closestHit].transform.gameObject;
                activateObject(pointerDest);
            }

			if (!clicked && Input.GetAxis("Break")==1)
            {
                clicked = true;
                clickObject(pointerDest);
            }
        } else {
            deactivateObject(pointerDest);
            pointerDest = null;
            clicked = false;
        }
		clicked = (Input.GetAxis("Break") == 1);
    }
}