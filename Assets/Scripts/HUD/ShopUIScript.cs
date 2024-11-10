using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class ShopUIScript : MonoBehaviour
{
    private Button _closeButton;
    private Label _soulCoinsText = null;
    private VisualElement _upgradePopUp;
    private Label _titleLabel;
    private Label _bonusLabel;
    private Label _descriptionLabel;
    private List<VisualElement> _cards = new List<VisualElement>();
    private List<VisualElement> _acquiredTicks = new List<VisualElement>();
    
    [System.Serializable]
    private class Upgrade
    {
        public string Title;
        public string Bonus;
        public string Description;
        public bool Acquired;
    }
    private List<Upgrade> _upgrades;

    //private List<Upgrade> ParseUpgrades(string[] lines)
    //{
    //    List<Upgrade> upgrades = new List<Upgrade>();
    //    Upgrade currentUpgrade = null;
    //    bool insideUpgradesArray = false;

    //    foreach (string line in lines)
    //    {
    //        string trimmedLine = line.Trim();

    //        if (trimmedLine.StartsWith("\"Upgrades\":"))
    //        {
    //            insideUpgradesArray = true;
    //            continue;
    //        }

    //        if (insideUpgradesArray)
    //        {
    //            if (trimmedLine.StartsWith("[") || trimmedLine.StartsWith("]"))
    //            {
    //                continue;
    //            }

    //            if (trimmedLine.StartsWith("{"))
    //            {
    //                currentUpgrade = new Upgrade();
    //            }
    //            else if (trimmedLine.StartsWith("}"))
    //            {
    //                if (currentUpgrade != null)
    //                {
    //                    upgrades.Add(currentUpgrade);
    //                    currentUpgrade = null;
    //                }
    //            }
    //            else if (currentUpgrade != null)
    //            {
    //                string[] parts = trimmedLine.Split(new[] { ':' }, 2);
    //                if (parts.Length == 2)
    //                {
    //                    string key = parts[0].Trim().Trim('"');
    //                    string value = parts[1].Trim().Trim('"', ',');

    //                    switch (key)
    //                    {
    //                        case "Title":
    //                            currentUpgrade.Title = value;
    //                            break;
    //                        case "Bonus":
    //                            currentUpgrade.Bonus = value;
    //                            break;
    //                        case "Description":
    //                            currentUpgrade.Description = value;
    //                            break;
    //                        case "Acquired":
    //                            currentUpgrade.Acquired = bool.Parse(value);
    //                            break;
    //                    }
    //                }
    //            }
    //        }
    //    }

    //    return upgrades;
    //}
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

    private void Awake()
    {
        string filePath = Path.Combine(Application.dataPath, "PermanentUpgrades.txt");
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            _upgrades = ParseUpgrades(lines);

            // Debugging: Log the number of upgrades found
            Debug.Log("Number of upgrades found: " + _upgrades.Count);
        }
        else
        {
            Debug.LogError("PermanentUpgrades.txt file not found at path: " + filePath);
        }

    }

    private void OpenShop()
    {
        //UI
        var attachedDocument = GetComponent<UIDocument>();
        if (!attachedDocument) return;

        var root = attachedDocument.rootVisualElement;

        if (root == null) return;
        _soulCoinsText = root.Q<Label>("SoulsText");

        UpdateSoulCoins(StaticVariablesManager.Instance.GetCoinAmount);

        _upgradePopUp = root.Q<VisualElement>("QuestPopUp");
        _upgradePopUp.style.display = DisplayStyle.None;

        // Initialize text labels
        _titleLabel = _upgradePopUp.Q<Label>("Title");
        _bonusLabel = _upgradePopUp.Q<Label>("Bonus");
        _descriptionLabel = _upgradePopUp.Q<Label>("Description");

        // Query all elements with the name "Card" and add them to the list
        _cards = root.Query<VisualElement>("Card").ToList();

        // Initialize the _acquiredTicks list and update their display status
        _acquiredTicks.Clear();
        for (int i = 0; i < _cards.Count; i++)
        {
            var tick = _cards[i].Q<VisualElement>("Tick");
            _acquiredTicks.Add(tick);
            if (i < _upgrades.Count)
            {
                tick.style.display = _upgrades[i].Acquired ? DisplayStyle.Flex : DisplayStyle.None;
            }
        }
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
            Vector2 localMousePosition = card.WorldToLocal(new Vector2(mousePosition.x, Screen.height - mousePosition.y));

            // Check if the mouse is over the card element
            if (card.ContainsPoint(localMousePosition))
            {
                isMouseOverCard = true;
                break;
            }
            cardIndex++;
        }

        // Update the display style of _upgradePopUp based on whether the mouse is over any card
        _upgradePopUp.style.display = isMouseOverCard ? DisplayStyle.Flex : DisplayStyle.None;

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
            _acquiredTicks[idx].style.display = DisplayStyle.Flex;

            // Update the soul coins display
            UpdateSoulCoins(StaticVariablesManager.Instance.GetCoinAmount);

            // Write the updated upgrades to the file
            WriteUpgradesToFile();
        }
    }
    [System.Serializable] private class UpgradeListWrapper
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
