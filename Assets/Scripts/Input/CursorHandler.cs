using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorHandler : MonoBehaviour
{
    [Header("Screen cursor")]
    [SerializeField] private bool isSelecting;
    [SerializeField] private bool hasSelected;
    [SerializeField] private Vector2 currentPosScreen;
    [SerializeField] private Vector2 initialPosScreen;
    [SerializeField] private float dragDirection;
    [SerializeField] private Vector2Int edgeCheck;

    [Header("Scene cursor")]
    [SerializeField] private bool hasHitSomething;
    //[SerializeField] private Collider colliderFound;
    [SerializeField] private Collider2D colliderFound;
    [SerializeField] private Vector3 currentPosScene;
    [SerializeField] private Vector3 initialPosScene;

    //public bool IsSelecting() { return isSelecting; }
    public bool HasSelected() { return hasSelected; }
    public Vector2 GetCurrentPosScreen() { return currentPosScreen; }
    public Vector2 GetInitialPosScreen() { return initialPosScreen; }
    public Vector2 GetCurrentPosScene() { return currentPosScene; }
    public Vector2 GetInitialPosScene() { return initialPosScene; }

    public void ReadCursor(UIPanel panelUnderCursor, bool selection, bool selectionDown, bool selectionUp)
    {
        if (!panelUnderCursor && selectionDown)
        {
            isSelecting = true;
        }
        if (isSelecting && selectionUp)
        {
            hasSelected = true;
        }
        if (!selection && !selectionDown && !selectionUp){
            isSelecting = false;
            hasSelected = false;
        }

        currentPosScreen = Input.mousePosition;
        if (!isSelecting)
        {
            initialPosScreen = currentPosScreen;
            dragDirection = float.NaN;
        }
        else
        {
            dragDirection = Mathf.Atan2(currentPosScreen.y - initialPosScreen.y, currentPosScreen.x - initialPosScreen.x) * 180 / Mathf.PI;
        }

        edgeCheck = new Vector2Int();
        if (currentPosScreen.x <= 0) edgeCheck.x--;
        if (currentPosScreen.x >= Screen.width - 1) edgeCheck.x++;
        if (currentPosScreen.y <= 0) edgeCheck.y--;
        if (currentPosScreen.y >= Screen.height - 1) edgeCheck.y++;

        Camera cam = Camera.main;
        Ray ray = cam.ScreenPointToRay(currentPosScreen);
        //hasHitSomething = Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity, selectionLayerMask);
        RaycastHit2D raycastHit = Physics2D.GetRayIntersection(ray);

        hasHitSomething = raycastHit.collider != null;
        if (hasHitSomething)
        {
            colliderFound = raycastHit.collider;
            currentPosScene = raycastHit.point;
            if (!isSelecting) initialPosScene = currentPosScene;
            //colliderFound = Physics2D.OverlapPoint(currentPosScene);
        }
        else
        {
            colliderFound = null;
        }
    }
}
