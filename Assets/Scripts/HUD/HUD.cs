using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class HUD : MonoBehaviour
{
    private UIDocument _attachedDocument = null;
    private VisualElement _root = null;

    private ProgressBar _healthbar = null;
    private VisualElement __healthbarContainer = null;

    private TextMeshProUGUI _soulCoinsText = null;
    private TextMeshProUGUI _coinText = null;
    private Label _speedText = null;
    private Label _rangeAttackCounter = null;

    private Label _wave = null;

    private Label _comboText = null;
    private ProgressBar _comboBar = null;
    private ProgressBar _dashBar = null;
    private ProgressBar _shield = null;

    private VisualElement _FIcon = null;
    private Label _ShopText = null;

    private Shop _shop = null;
    private bool _isDisplayOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        // UI
        _attachedDocument = GetComponent<UIDocument>();
        if (_attachedDocument)
        {
            _root = _attachedDocument.rootVisualElement;
        }

        if (_root == null) return;
        _healthbar = _root.Q<ProgressBar>("PlayerHealthBar"); // this will find the first progressbar in the hud, for now as there is only one, that is fine, if we need to be more specific we could pass along a string parameter to define the name of the element.
        var healthbarContainer = _healthbar.Q(className: "unity-progress-bar__background");
        healthbarContainer.style.backgroundColor = Color.black;
        __healthbarContainer = _healthbar.Q(className: "unity-progress-bar__progress");
        __healthbarContainer.style.backgroundColor = Color.green;

        _speedText = _root.Q<Label>("Speed");
        _comboText = _root.Q<Label>("Combo");
        _comboBar = _root.Q<ProgressBar>("ComboBar");
        _rangeAttackCounter = _root.Q<Label>("RangeAttackCounter");
        _dashBar = _root.Q<ProgressBar>("DashBar");
        _shield = _root.Q<ProgressBar>("ShieldBar");
        _wave = _root.Q<Label>("Wave");

        _FIcon = _root.Q<VisualElement>("FKey");
        _ShopText = _root.Q<Label>("OpenShop");
        _FIcon.style.display = DisplayStyle.None;
        _ShopText.style.display = DisplayStyle.None;
        _shop = FindObjectOfType<Shop>();

        // Find the Canvas elements
        _soulCoinsText = GameObject.Find("SoulText").GetComponent<TextMeshProUGUI>();
        _coinText = GameObject.Find("CoinText").GetComponent<TextMeshProUGUI>();

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

            SpecialPowerScript shield = player.GetComponent<SpecialPowerScript>();
            if (shield)
            {
                // initialize
                UpdateShield(shield.Duration, shield.Duration, 0, shield.Cooldown);
                // hook to monitor changes
                shield.OnShieldChange += UpdateShield;
            }

            UpdateSoulCoins(StaticVariablesManager.Instance.GetCoinAmount);
            StaticVariablesManager.Instance.OnSoulCoinChanged += UpdateSoulCoins;

            UpdateWave(StaticVariablesManager.Instance.CurrentLevel);
            StaticVariablesManager.Instance.OnLevelChange += UpdateWave;

            UpdateEnemies(StaticVariablesManager.Instance.EnemyCount);
            StaticVariablesManager.Instance.OnEnemyChange += UpdateEnemies;

            ComboScript comboScript = player.GetComponent<ComboScript>();
            if (comboScript)
            {
                UpdateCombo(0, 0, 0);
                comboScript.OnComboChange += UpdateCombo;
            }

            UpdateRangeAttackCounter(player.TotalRangedAttack);
            player.OnRangedAttackChange += UpdateRangeAttackCounter;

            DashScript dashScript = player.GetComponent<DashScript>();
            if (dashScript)
            {
                UpdateDash(0, 0);
                dashScript.OnDashChange += UpdateDash;
            }

            UpdateGold(0);
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

    public void UpdateRangeAttackCounter(int current)
    {
        if (_rangeAttackCounter == null) return;

        if (current == 0)
            _rangeAttackCounter.style.display = DisplayStyle.None;
        else
        {
            _rangeAttackCounter.style.display = DisplayStyle.Flex;
            _rangeAttackCounter.text = current.ToString();
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

    public void UpdateDash(float start, float current)
    {
        if (_dashBar == null) return;

        // Clamp current to a minimum of 0
        current = Mathf.Max(current, 0);
        _dashBar.value = (current / start) * 100.0f;

        // Check if current is less than or equal to 0 and update the display style of _dashBar
        _dashBar.style.display = current <= 0 ? DisplayStyle.None : DisplayStyle.Flex;
    }

    public void UpdateEnemies(int enemies)
    {
        if (_speedText == null) return;

        _speedText.text = enemies > 0 ? "Enemies: " + enemies.ToString() : string.Empty;
        _speedText.style.display = enemies > 0 ? DisplayStyle.Flex : DisplayStyle.None;
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

    public void UpdateShield(float start, float current, float timer, float cooldown)
    {
        if (_shield == null) return;

        // Clamp current to a minimum of 0
        current = Mathf.Max(current, 0);

        // Update the health bar value and title
        _shield.value = (current / start) * 100.0f;
        _shield.title = string.Format("{0}/{1}", current, start);

        // Handle the timer logic for shield recharge
        if (current == 0)
        {
            float restoredShield = (1 - (timer / cooldown)) * start;
            restoredShield = Mathf.Min(restoredShield, start); // Clamp restored shield to a maximum of start
            _shield.value = (restoredShield / start) * 100.0f;
            _shield.title = string.Format("{0}/{1}", restoredShield, start);
        }
    }

    public void UpdateSoulCoins(int soulCoins)
    {
        if (_soulCoinsText == null) return;

        _soulCoinsText.text = soulCoins.ToString();
    }

    public void UpdateGold(int gold)
    {
        if (_coinText == null) return;

        _coinText.text = gold.ToString();
    }

    public void UpdateWave(int wave)
    {
        if (_wave == null) return;

        _wave.text = "Room " + wave;
    }
}
