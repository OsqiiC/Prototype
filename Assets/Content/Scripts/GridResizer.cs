using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridResizer : MonoBehaviour
{
    [SerializeField] private float minWidth = 400;
    [SerializeField] private float targetWidth;
    [SerializeField] private GridLayoutGroup gridLayoutGroup;
    [SerializeField] private bool useParentDefinedResize = true;
    [SerializeField] private bool useUpdateResize = true;

    private RectTransform gridRect => gridLayoutGroup.transform as RectTransform;
    private RectTransform parentRectTransform => gridLayoutGroup.transform.parent.transform as RectTransform;

    private float MinWidth => minWidth < 300 ? 300 : minWidth;

    private float contentSpace;
    private float widthRatio;
    private int columnCount;
    private float cellWidth;

    private void OnRectTransformDimensionsChange()
    {
        if (!useUpdateResize) return;

        UpdateCells();
    }

    [ContextMenu("Update")]
    public void UpdateCells()
    {
        if (gridRect.childCount < 1) return;
        
        contentSpace = (gridRect.rect.width - gridLayoutGroup.padding.left -
                        gridLayoutGroup.padding.right);
        
        var usedWidth =contentSpace < targetWidth? MinWidth: targetWidth;
        
        widthRatio = (contentSpace + gridLayoutGroup.spacing.x) / (usedWidth + gridLayoutGroup.spacing.x);
        columnCount = Mathf.FloorToInt(widthRatio);
        cellWidth = CalculateCellWidth();

        gridLayoutGroup.cellSize = new Vector2(cellWidth, gridLayoutGroup.cellSize.y);
    }

    private float CalculateCellWidth()
    {
        if (useParentDefinedResize && columnCount == 1)
        {
            float parentGridOffset = 0;

            if (parentRectTransform.TryGetComponent(out LayoutGroup layoutGroup))
            {
                parentGridOffset = layoutGroup.padding.left + layoutGroup.padding.right;
            }

            return parentRectTransform.rect.width - gridLayoutGroup.padding.left -
                   gridLayoutGroup.padding.right - parentGridOffset;
        }

        return (contentSpace - (columnCount - 1) * gridLayoutGroup.spacing.x) / columnCount;
    }
}