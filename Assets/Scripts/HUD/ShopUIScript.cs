using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShopUIScript : MonoBehaviour
{
    private Button _closeButton;
    private Label _soulCoinsText = null;
    private void OnEnable()
    {
        BindCloseButton();
    }
    private IEnumerable<Object> BindCloseButton()
    {
        var _attachedDocument = GetComponent<UIDocument>();
        if (_attachedDocument)
        {
            var _root = _attachedDocument.rootVisualElement;
            if (_root != null)
            {
                _closeButton = _root.Q<Button>("CloseButton");
                if (_closeButton != null)
                {
                    _closeButton.clickable.clicked += FindObjectOfType<Shop>().gameObject.GetComponent<Shop>().CloseShop;
                }
            }
        };
        return null;
    }

    void Start()
    {
        //UI
        var attachedDocument = GetComponent<UIDocument>();
        if (!attachedDocument) return;
        
        var root = attachedDocument.rootVisualElement;

        if (root == null) return;
        _soulCoinsText = root.Q<Label>("SoulsText");

        UpdateSoulCoins(StaticVariablesManager.Instance.GetCoinAmount);
        StaticVariablesManager.Instance.OnSoulCoinChanged += UpdateSoulCoins;
    }

    public void UpdateSoulCoins(int soulCoins)
    {
        if (_soulCoinsText == null) return;

        _soulCoinsText.text = soulCoins.ToString();
    }
}
