using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;


public class Pen : MonoBehaviour
{
    [Header("Pen Properties")]
    public Transform tip;
    public Material drawingMaterial;
    public Material tipMaterial;
    [Range(0.01f, 0.1f)]
    public float penWidth = 0.01f;
    public Color[] penColors;

    [Header("Hands & Grabbable")]
    public XRDirectInteractor rightHand;
    public XRDirectInteractor leftHand;
    public XRGrabInteractable grabbable;

    private LineRenderer currentDrawing;
    private int index;
    private int currentColorIndex;

    [SerializeField] private InputActionReference rTrigRef;
    [SerializeField] private InputActionReference lTrigRef;
    [SerializeField] private InputActionReference rSecondary;


    private void Start()
    {
        currentColorIndex = 0;
        tipMaterial.color = penColors[currentColorIndex];
    }

    private void Update()
    {
        bool isGrabbed = grabbable.isSelected;
        bool isRightHandDrawing = isGrabbed && grabbable.firstInteractorSelecting == rightHand && rTrigRef.action.IsPressed()==true;
        bool isLeftHandDrawing = isGrabbed && grabbable.firstInteractorSelecting == leftHand && lTrigRef.action.IsPressed() == true;
        if (isRightHandDrawing || isLeftHandDrawing)
        {
            Draw();
        }
        else if (currentDrawing != null)
        {
            currentDrawing = null;
        }
        else if (rSecondary.action.IsPressed() == true)
        {
            SwitchColor();
        }
    }

    private void Draw()
    {
        if (currentDrawing == null)
        {
            index = 0;
            currentDrawing = new GameObject().AddComponent<LineRenderer>();
            currentDrawing.material = drawingMaterial;
            currentDrawing.startColor = currentDrawing.endColor = penColors[currentColorIndex];
            currentDrawing.startWidth = currentDrawing.endWidth = penWidth;
            currentDrawing.positionCount = 1;
            currentDrawing.SetPosition(0, tip.position);
        }
        else
        {
            var currentPos = currentDrawing.GetPosition(index);
            if (Vector3.Distance(currentPos, tip.position) > 0.01f)
            {
                index++;
                currentDrawing.positionCount = index + 1;
                currentDrawing.SetPosition(index, tip.position);
            }
        }
    }

    private void SwitchColor()
    {
        if (currentColorIndex == penColors.Length - 1)
        {
            currentColorIndex = 0;
        }
        else
        {
            currentColorIndex++;
        }
        tipMaterial.color = penColors[currentColorIndex];
    }
}