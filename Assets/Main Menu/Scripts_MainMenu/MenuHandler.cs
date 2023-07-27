using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    public GameObject[] items;     
    private int currentItemIndex;  

    private void Start()
    {
        currentItemIndex = 0;
        SetItemActive(currentItemIndex);
    }

    public void OnNextButtonClick()
    {
        SetItemActive(currentItemIndex, false);
        currentItemIndex = (currentItemIndex + 1) % items.Length;
        SetItemActive(currentItemIndex, true);
    }

    public void OnPrevButtonClick()
    {
        SetItemActive(currentItemIndex, false);
        currentItemIndex = (currentItemIndex - 1 + items.Length) % items.Length;
        SetItemActive(currentItemIndex, true);
    }

    private void SetItemActive(int index, bool isActive = true)
    {
        if (index >= 0 && index < items.Length)
            items[index].SetActive(isActive);
    }
}

