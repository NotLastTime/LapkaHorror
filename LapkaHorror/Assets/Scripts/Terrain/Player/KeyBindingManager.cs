using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class KeyBindingManager : MonoBehaviour
{
    [SerializeField] public Button saveButton;
    [SerializeField] public Button backButton;
    
    public static KeyBindingManager Instance {  get; private set; }

    private Dictionary<string, KeyCode> _movementKeys = new Dictionary<string, KeyCode>();
    private Dictionary<string, KeyCode> _abilityKeys = new Dictionary<string, KeyCode>();
    //private Dictionary<string, KeyCode> _interactionKeys = new Dictionary<string, KeyCode>();

    private GameObject _currentMovementKey;
    private GameObject _currentAbilityKey;

    private Color32 _normalColor = new Color32(255, 196, 196, 255);
    private Color32 _selectedColor = new Color32(227, 31, 31, 255);
    private Color32 _hoverColor = new Color(200, 200, 200, 255);

    public Text forward, back, left, right, jump;
    public Text ability1, ability2, ability3, ability4, ability5, ability6, ability7, ability8, ability9;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadKeys();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializeKeysUI();
        SetupButtonsEvents();
    }

    private void OnGUI()
    {
        if (_currentMovementKey != null)
        {
            Event e = Event.current;

            if (e.isKey)
            {
                _movementKeys[_currentMovementKey.name] = e.keyCode;
                _currentMovementKey.transform.GetChild(0).GetComponent<Text>().text = e.keyCode.ToString();
                _currentMovementKey.GetComponent<Image>().color = _normalColor;
                _currentMovementKey = null;
            }
        }
        if (_currentAbilityKey != null)
        {
            Event e = Event.current;

            if (e.isKey)
            {
                _abilityKeys[_currentAbilityKey.name] = e.keyCode;
                _currentAbilityKey.transform.GetChild(0).GetComponent<Text>().text = e.keyCode.ToString();
                _currentAbilityKey.GetComponent<Image>().color = _normalColor;
                _currentAbilityKey = null;
            }
        }
    }

    public void ChangeKey(GameObject clicked)
    {
        if (_currentMovementKey != null)
        {
            _currentMovementKey.GetComponent<Image>().color = _normalColor;
        }

        _currentMovementKey = clicked;
        _currentMovementKey.GetComponent<Image>().color = _selectedColor;

        if (_currentAbilityKey != null)
        {
            _currentAbilityKey.GetComponent<Image>().color = _normalColor;
        }

        _currentAbilityKey = clicked;
        _currentAbilityKey.GetComponent<Image>().color = _selectedColor;
    }

    public void InitializeKeysUI()
    {
        // Move
        forward.text = _movementKeys["MoveForwardBtn"].ToString();
        back.text = _movementKeys["MoveBackBtn"].ToString();
        left.text = _movementKeys["MoveLeftBtn"].ToString();
        right.text = _movementKeys["MoveRightBtn"].ToString();
        jump.text = _movementKeys["JumpBtn"].ToString();

        // Ability
        ability1.text = _abilityKeys["AbilityActionBtn1"].ToString();
        ability2.text = _abilityKeys["AbilityActionBtn2"].ToString();
        ability3.text = _abilityKeys["AbilityActionBtn3"].ToString();
        ability4.text = _abilityKeys["AbilityActionBtn4"].ToString();
        ability5.text = _abilityKeys["AbilityActionBtn5"].ToString();
        ability6.text = _abilityKeys["AbilityActionBtn6"].ToString();
        ability7.text = _abilityKeys["AbilityActionBtn7"].ToString();
        ability8.text = _abilityKeys["AbilityActionBtn8"].ToString();
        ability9.text = _abilityKeys["AbilityActionBtn9"].ToString();

        // Interation
    }

    public void SaveKeys()
    {
        foreach (var key in _movementKeys)
        {
            PlayerPrefs.SetString(key.Key, key.Value.ToString());
        }

        foreach (var key in _abilityKeys)
        {
            PlayerPrefs.SetString(key.Key, key.Value.ToString());
        }

        PlayerPrefs.Save();
    }

    private void LoadKeys()
    {
        // Move
        _movementKeys.Add("MoveForwardBtn", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("MoveForwardBtn", "W")));
        _movementKeys.Add("MoveBackBtn", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("MoveBackBtn", "S")));
        _movementKeys.Add("MoveLeftBtn", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("MoveLeftBtn", "A")));
        _movementKeys.Add("MoveRightBtn", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("MoveRightBtn", "D")));
        _movementKeys.Add("JumpBtn", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("JumpBtn", "Space")));

        // Ability
        _abilityKeys.Add("AbilityActionBtn1", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("AbilityActionBtn1", "Alpha1").Replace("Alpha", "")));
        _abilityKeys.Add("AbilityActionBtn2", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("AbilityActionBtn2", "Alpha2").Replace("Alpha", "")));
        _abilityKeys.Add("AbilityActionBtn3", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("AbilityActionBtn3", "Alpha3").Replace("Alpha", "")));
        _abilityKeys.Add("AbilityActionBtn4", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("AbilityActionBtn4", "Alpha4").Replace("Alpha", "")));
        _abilityKeys.Add("AbilityActionBtn5", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("AbilityActionBtn5", "Alpha5").Replace("Alpha", "")));
        _abilityKeys.Add("AbilityActionBtn6", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("AbilityActionBtn6", "Alpha6").Replace("Alpha", "")));
        _abilityKeys.Add("AbilityActionBtn7", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("AbilityActionBtn7", "Alpha7").Replace("Alpha", "")));
        _abilityKeys.Add("AbilityActionBtn8", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("AbilityActionBtn8", "Alpha8").Replace("Alpha", "")));
        _abilityKeys.Add("AbilityActionBtn9", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("AbilityActionBtn9", "Alpha9").Replace("Alpha", "")));
    }

    public Dictionary<string, KeyCode> GetMovementKeyBindings()
    {
        return _movementKeys;
    }

    public Dictionary<string, KeyCode> GetAbilityKeyBindings()
    {
        return _abilityKeys;
    }

    private void SetupButtonsEvents()
    {
        AddEventTrigger(saveButton.gameObject, EventTriggerType.PointerEnter, (e) => OnPointerEnterButton(saveButton, _hoverColor));
        AddEventTrigger(saveButton.gameObject, EventTriggerType.PointerExit, (e) => OnPointerExitButton(saveButton, _normalColor));

        AddEventTrigger(backButton.gameObject, EventTriggerType.PointerEnter, (e) => OnPointerEnterButton(backButton, _hoverColor));
        AddEventTrigger(backButton.gameObject, EventTriggerType.PointerExit, (e) => OnPointerExitButton(backButton, _normalColor));
    }

    private void AddEventTrigger(GameObject _obj, EventTriggerType _type, Action<BaseEventData> _action)
    {
        EventTrigger _trigger = _obj.GetComponent<EventTrigger>();
        if (_trigger == null)
        {
            _trigger = _obj.AddComponent<EventTrigger>();
        }

        EventTrigger.Entry _entry = new EventTrigger.Entry { eventID = _type };
        _entry.callback.AddListener(new UnityEngine.Events.UnityAction<BaseEventData>(_action));
        _trigger.triggers.Add(_entry);
    }

    private void OnPointerEnterButton(Button _button, Color _hoverColor)
    {
        _button.GetComponent<Image>().color = _hoverColor;
    }

    private void OnPointerExitButton(Button _button, Color _normalColor)
    {
        _button.GetComponent<Image>().color = _normalColor;
    }
}