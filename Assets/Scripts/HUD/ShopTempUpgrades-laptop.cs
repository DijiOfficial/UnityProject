using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTempUpgrades : Shop
{
    private GameObject _upgradeShop;
    protected override void Awake()
    {
        if (_inputAsset == null) return;

        _FKeyPress = _inputAsset.FindActionMap("KeyBinds").FindAction("F");
        _EscKeyPress = _inputAsset.FindActionMap("KeyBinds").FindAction("ESC");

        _upgradeShop = GameObject.Find("ShopUICanvas");
        //_isInShop = true;
        CloseShop();
    }

    public override void CloseShop()
    {
        _isInShop = false;
        _upgradeShop.SetActive(false);
        DisableCursor();
    }

    protected override void HandleShopInput()
    {
        if (_FKeyPress.IsPressed() && !_isInShop)
        {
            _isInShop = true;
            _upgradeShop.SetActive(true);

            EnableCursor();
        }
        else if (_EscKeyPress.IsPressed() && _isInShop)
        {
            CloseShop();
        }
    }
}
