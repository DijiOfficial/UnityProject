using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class Shop : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] protected float _interactionRadius = 25.0f;

    [SerializeField] protected InputActionAsset _inputAsset;
    protected InputAction _FKeyPress;
    protected InputAction _EscKeyPress;
    private GameObject _UI;
    private Transform _playerTransform;

    protected bool _isInShop = false;
    private bool _isInRange = false;

    public bool IsInShopeRange { get { return _isInRange; } }
    protected virtual void Awake()
    {
        if (_inputAsset == null) return;

        _FKeyPress = _inputAsset.FindActionMap("KeyBinds").FindAction("F");
        _EscKeyPress = _inputAsset.FindActionMap("KeyBinds").FindAction("ESC");
        _UI = GameObject.Find("ShopUI");
        _UI.SetActive(false);
    }

    void Start()
    {
        _playerTransform = FindObjectOfType<PlayerCharacter>().gameObject.transform;
    }

    private void Update()
    {
        if (_playerTransform == null) return;

        // Check if the player is within the interaction radius
        float distanceToPlayerSqr = (transform.position - _playerTransform.position).sqrMagnitude;
        if (distanceToPlayerSqr <= _interactionRadius)
        {
            _isInRange = true;
            HandleShopInput();
        }
        else
        {
            _isInRange = false;
            if (_isInShop) CloseShop();
        }
    }
    protected virtual void HandleShopInput()
    {
        if(_FKeyPress.IsPressed() && !_isInShop)
        {
            _isInShop = true;

            _UI.SetActive(true);
            EnableCursor();
        }
        else if(_EscKeyPress.IsPressed() && _isInShop)
        {
            CloseShop();
        }
    }

    public virtual void CloseShop()
    {
        _isInShop = false;

        _UI.SetActive(false);
        DisableCursor();
    }

    protected void EnableCursor()
    {
        UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.lockState = CursorLockMode.Confined;

    }

    protected void DisableCursor()
    {
        UnityEngine.Cursor.visible = false;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, Mathf.Sqrt(_interactionRadius));
    }
}
