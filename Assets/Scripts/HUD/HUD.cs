using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class HUD : MonoBehaviour
{
    private UIDocument _attachedDocument = null;
    private VisualElement _root = null;

    private ProgressBar _healthbar = null;
    private Label _soulCoinsText = null;
    private Label _speedText = null;
    // Start is called before the first frame update
    void Start()
    {
        //UI
        _attachedDocument = GetComponent<UIDocument>();
        if (_attachedDocument)
        {
            _root = _attachedDocument.rootVisualElement;
        }

        if (_root != null)
        {
            _healthbar = _root.Q<ProgressBar>("PlayerHealthBar"); //this will find the first progressbar in the hud, for now as there is only one, that is fine, if we need to be more specific we could pass along a string parameter to define the name of the element.
            _soulCoinsText = _root.Q<Label>("CoinTrackerText");
            _speedText = _root.Q<Label>("Speed");

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

                SoulCoinTracker soulCoinTracker = player.GetComponent<SoulCoinTracker>();
                if (soulCoinTracker)
                {
                    UpdateSoulCoins(StaticVariablesManager._soulCoins);

                    soulCoinTracker.OnSoulCoinChanged += UpdateSoulCoins;
                }

                MovementBehaviour movementBehaviour = player.GetComponent<MovementBehaviour>();
                if (movementBehaviour)
                {
                    UpdateSpeed(movementBehaviour.Speed);

                    movementBehaviour.OnSpeedChange += UpdateSpeed;
                }
            }
        }
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


