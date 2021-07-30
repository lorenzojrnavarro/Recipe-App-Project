using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FilterDropDown : MonoBehaviour
{
    public GameObject dropdownBox;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        bool isOpen = dropdownBox.activeSelf;
        float alphaTarget = isOpen ? 0 : 1;

        dropdownBox.SetActive(true);
        dropdownBox.GetComponent<CanvasGroup>().DOFade(alphaTarget, 0.25f).OnComplete(() =>
        {
            dropdownBox.SetActive(!isOpen);
        });

    }

}
