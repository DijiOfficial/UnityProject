using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class ShopUIScript : MonoBehaviour
{
    [SerializeField] private UnityEvent _onBuy;
    [SerializeField] private TempPlayerInfo _tempplayerInfo;

    private Button _closeButton;
    private TMP_Text _soulCoinsText = null;
    private GameObject _upgradePopUp;
    private TMP_Text _titleLabel;
    private TMP_Text _bonusLabel;
    private TMP_Text _descriptionLabel;
    private List<GameObject> _cards = new List<GameObject>();
    private List<GameObject> _acquiredTicks = new List<GameObject>();

    [System.Serializable]
    private class Upgrade
    {
        public string Title;
        public string Bonus;
        public string Description;
        public bool Acquired;
    }
    private List<Upgrade> _upgrades;

    private List<Upgrade> ParseUpgrades(string[] lines)
    {
        string json = string.Join("\n", lines);
        var upgradesWrapper = JsonUtility.FromJson<UpgradeListWrapper>(json);
        return upgradesWrapper?.Upgrades ?? new List<Upgrade>();
    }

    private void OnEnable()
    {
        BindCloseButton();
        OpenShop();
    }

    private void BindCloseButton()
    {
        _closeButton = GameObject.Find("CloseButton").GetComponent<Button>();
        if (_closeButton != null)
            _closeButton.onClick.AddListener(() => FindObjectOfType<Shop>().CloseShop());
    }

    private void Awake()
    {
        string filePath = Path.Combine(Application.dataPath, "PermanentUpgrades.txt");
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            _upgrades = ParseUpgrades(lines);

            // Debugging: Log the number of upgrades found
        }
        else
        {
            Debug.LogError("PermanentUpgrades.txt file not found at path: " + filePath);
        }
        _soulCoinsText = GameObject.Find("SoulsText").GetComponent<TMP_Text>();
        _upgradePopUp = GameObject.Find("QuestPopUp");

        _titleLabel = _upgradePopUp.transform.Find("Title").GetComponent<TMP_Text>();
        _bonusLabel = _upgradePopUp.transform.Find("Bonus").GetComponent<TMP_Text>();
        _descriptionLabel = _upgradePopUp.transform.Find("Description").GetComponent<TMP_Text>();

        GameObject upgradesContainer = GameObject.Find("Upgrades");
        if (upgradesContainer == null) return;
        
        // Query all elements with the name "Card" and add them to the list
        foreach (Transform card in upgradesContainer.transform)
        {
            if (card.name == "Card")
            {
                _cards.Add(card.gameObject);
            }
        }

        for (int i = 0; i < _cards.Count; i++)
        {
            var tick = _cards[i].transform.Find("Tick");
            if (tick != null)
            {
                _acquiredTicks.Add(tick.gameObject);
                if (i < _upgrades.Count)
                {
                    tick.gameObject.SetActive(_upgrades[i].Acquired);
                }
            }
        }
    }

    private void OpenShop()
    {
        UpdateSoulCoins(StaticVariablesManager.Instance.GetCoinAmount);
        _upgradePopUp.SetActive(false);
    }

    private void Update()
    {
        if (_cards == null || _upgradePopUp == null) return;

        // Get the mouse position in screen space
        Vector2 mousePosition = Input.mousePosition;

        // Iterate through the list of cards
        bool isMouseOverCard = false;
        int cardIndex = 0;
        foreach (var card in _cards)
        {
            // Convert the mouse position to the local position of the card element
            RectTransform rectTransform = card.GetComponent<RectTransform>();
            Vector2 localMousePosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, mousePosition, null, out localMousePosition);

            // Check if the mouse is over the card element
            if (rectTransform.rect.Contains(localMousePosition))
            {
                isMouseOverCard = true;
                break;
            }
            cardIndex++;
        }

        // Update the display style of _upgradePopUp based on whether the mouse is over any card
        _upgradePopUp.SetActive(isMouseOverCard);

        // Update the text labels with the corresponding upgrade details
        if (isMouseOverCard && cardIndex < _upgrades.Count)
        {
            var upgrade = _upgrades[cardIndex];
            _titleLabel.text = upgrade.Title;
            _bonusLabel.text = "COSTS: \n" + upgrade.Bonus;
            _descriptionLabel.text = upgrade.Description;

            // Check for mouse click to buy the upgrade
            if (Input.GetMouseButtonDown(0) && !upgrade.Acquired)
                BuyUpgrade(cardIndex);
        }
    }

    public void UpdateSoulCoins(int soulCoins)
    {
        if (_soulCoinsText == null) return;

        _soulCoinsText.text = soulCoins.ToString();
    }

    private void BuyUpgrade(int idx)
    {
        var upgrade = _upgrades[idx];
        int bonusCost = int.Parse(upgrade.Bonus.Trim());

        if (StaticVariablesManager.Instance.GetCoinAmount >= bonusCost)
        {
            // Deduct the cost from the soul coins
            StaticVariablesManager.Instance.RemoveCoin(bonusCost);

            // Mark the upgrade as acquired
            upgrade.Acquired = true;

            // Update the tick display
            _acquiredTicks[idx].SetActive(true);

            // Update the soul coins display
            UpdateSoulCoins(StaticVariablesManager.Instance.GetCoinAmount);

            // Write the updated upgrades to the file
            WriteUpgradesToFile();

            // Reload the TempPlayerInfo persistent file
            _tempplayerInfo.LoadPermanentUpgrades();

            _onBuy?.Invoke();
        }
    }

    [System.Serializable]
    private class UpgradeListWrapper
    {
        public List<Upgrade> Upgrades;
    }

    private void WriteUpgradesToFile()
    {
        string filePath = Path.Combine(Application.dataPath, "PermanentUpgrades.txt");
        var upgradesWrapper = new UpgradeListWrapper
        {
            Upgrades = _upgrades
        };
        string json = JsonUtility.ToJson(upgradesWrapper, true);
        File.WriteAllText(filePath, json);
    }
}
