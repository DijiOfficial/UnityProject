using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class Shop : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private float _interactionRadius = 25.0f;

    [SerializeField] private InputActionAsset _inputAsset;
    private InputAction _FKeyPress;
    private InputAction _EscKeyPress;
    private GameObject _UI;
    private Transform _playerTransform;

    private bool _isInShop = false;
    private bool _isInRange = false;

    public bool IsInShopeRange { get { return _isInRange; } }
    private void Awake()
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
            _isInRange = false;
    }
    private void HandleShopInput()
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

    public void CloseShop()
    {
        _isInShop = false;

        _UI.SetActive(false);
        DisableCursor();
    }

    private void EnableCursor()
    {
        UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.lockState = CursorLockMode.Confined;

    }

    private void DisableCursor()
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
