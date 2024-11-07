using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class HUD : MonoBehaviour
{
    private UIDocument _attachedDocument = null;
    private VisualElement _root = null;

    private ProgressBar _healthbar = null;
    private VisualElement __healthbarContainer = null;

    private Label _soulCoinsText = null;
    private Label _speedText = null;

    private Label _comboText = null;
    private ProgressBar _comboBar = null;

    private VisualElement _FIcon = null;
    private Label _ShopText = null;

    private Shop _shop = null;
    private bool _isDisplayOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        //UI
        _attachedDocument = GetComponent<UIDocument>();
        if (_attachedDocument)
        {
            _root = _attachedDocument.rootVisualElement;
        }

        if (_root == null) return;
        _healthbar = _root.Q<ProgressBar>("PlayerHealthBar"); //this will find the first progressbar in the hud, for now as there is only one, that is fine, if we need to be more specific we could pass along a string parameter to define the name of the element.
        var healthbarContainer = _healthbar.Q(className: "unity-progress-bar__background");
        healthbarContainer.style.backgroundColor = Color.black;
        __healthbarContainer = _healthbar.Q(className: "unity-progress-bar__progress");
        __healthbarContainer.style.backgroundColor = Color.green;

        _soulCoinsText = _root.Q<Label>("CoinTrackerText");
        _speedText = _root.Q<Label>("Speed");
        _comboText = _root.Q<Label>("Combo");
        _comboBar = _root.Q<ProgressBar>("ComboBar");

        _FIcon = _root.Q<VisualElement>("FKey");
        _ShopText = _root.Q<Label>("OpenShop");
        _FIcon.style.display = DisplayStyle.None;
        _ShopText.style.display = DisplayStyle.None;
        _shop = FindObjectOfType<Shop>();

        PlayerCharacter player = FindObjectOfType<PlayerCharacter>();
        if (player != null)
        {

            Health playerHealth = player.GetComponent<Health>();
            if (playerHealth)
            {
                // initialize
                UpdateHealth(playerHealth.StartHealth, playerHealth.CurrentHealth);
                // hook to monitor changes
                playerHealth.OnHealthChanged += UpdateHealth;
            }

            UpdateSoulCoins(StaticVariablesManager.Instance.GetCoinAmount);
            StaticVariablesManager.Instance.OnSoulCoinChanged += UpdateSoulCoins;

            MovementBehaviour movementBehaviour = player.GetComponent<MovementBehaviour>();
            if (movementBehaviour)
            {
                UpdateSpeed(movementBehaviour.Speed);

                movementBehaviour.OnSpeedChange += UpdateSpeed;
            }

            ComboScript comboScript = player.GetComponent<ComboScript>();
            if (comboScript)
            {
                UpdateCombo(0,0,0);

                comboScript.OnComboChange += UpdateCombo;
            }
        }
    }

    private void Update()
    {
        if (!_shop) return;

        if (_shop.IsInShopeRange && !_isDisplayOpen)
        {
            _isDisplayOpen = true;
            _FIcon.style.display = DisplayStyle.Flex;
            _ShopText.style.display = DisplayStyle.Flex;
        }
        else if (!_shop.IsInShopeRange && _isDisplayOpen)
        {
            _isDisplayOpen = false;
            _FIcon.style.display = DisplayStyle.None;
            _ShopText.style.display = DisplayStyle.None;
        }
    }

    public void UpdateCombo(int combo, float start, float current)
    {
        if (_comboText == null || _comboBar == null) return;

        _comboText.text = combo == 0 ? " " : "Combo " + combo + "x";

        // Clamp current to a minimum of 0
        current = Mathf.Max(current, 0);
        _comboBar.value = (current / start) * 100.0f;

        // Check if current is less than or equal to 0 and update the display style of _comboBar
        _comboBar.style.display = current <= 0 ? DisplayStyle.None : DisplayStyle.Flex;
    }

    public void UpdateSpeed(float speed)
    {
        if (_speedText == null) return;

        _speedText.text = "Speed: " + speed.ToString();
    }

    public void UpdateHealth(float startHealth, float currentHealth)
    {
        if (_healthbar == null) return;

        // Clamp currentHealth to a minimum of 0
        currentHealth = Mathf.Max(currentHealth, 0);

        // Update the health bar value and title
        _healthbar.value = (currentHealth / startHealth) * 100.0f;
        _healthbar.title = string.Format("{0}/{1}", currentHealth, startHealth);
    }
    
    public void UpdateSoulCoins(int soulCoins)
    {
        if (_soulCoinsText == null) return;

        _soulCoinsText.text = soulCoins.ToString();
    }
}


