using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class KeyBinding : MonoBehaviour
{
    [SerializeField] public Button saveButton;
    [SerializeField] public Button backButton;
    
    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
    private GameObject _currentKey;

    public Text forward, back, left, right, jump;

    private Color32 _normalColor = new Color32(255, 196, 196, 255);
    private Color32 _selectedColor = new Color32(227, 31, 31, 255);
    private Color32 _hoverColor = new Color(200, 200, 200, 255);


    private void Start()
    {
        LoadKeys();

        forward.text = keys["MoveForwardBtn"].ToString();
        back.text = keys["MoveBackBtn"].ToString();
        left.text = keys["MoveLeftBtn"].ToString();
        right.text = keys["MoveRightBtn"].ToString();
        jump.text = keys["JumpBtn"].ToString();

        AddEventTrigger(saveButton.gameObject, EventTriggerType.PointerEnter, (e) => OnPointerEnterButton(saveButton, _hoverColor));
        AddEventTrigger(saveButton.gameObject, EventTriggerType.PointerExit, (e) => OnPointerExitButton(saveButton, _normalColor));

        AddEventTrigger(backButton.gameObject, EventTriggerType.PointerEnter, (e) => OnPointerEnterButton(backButton, _hoverColor));
        AddEventTrigger(backButton.gameObject, EventTriggerType.PointerExit, (e) => OnPointerExitButton(backButton, _normalColor));
    }

    private void Update()
    {
        if (Input.GetKeyDown(keys["MoveForwardBtn"]))
        {
            //Do a move action
            Debug.Log($"Forward: {forward.text}");
        }

        if (Input.GetKeyDown(keys["MoveBackBtn"]))
        {
            //Do a move action
            Debug.Log($"Back: {back.text}");
        }

        if (Input.GetKeyDown(keys["MoveLeftBtn"]))
        {
            //Do a move action
            Debug.Log($"Left: {left.text}");
        }

        if (Input.GetKeyDown(keys["MoveRightBtn"]))
        {
            //Do a move action
            Debug.Log($"Right: {right.text}");
        }

        if (Input.GetKeyDown(keys["JumpBtn"]))
        {
            //Do a move action
            Debug.Log($"Jump: {jump.text}");
        }
    }

    private void OnGUI()
    {
        if (_currentKey != null)
        {
            Event e = Event.current;

            if (e.isKey)
            {
                keys[_currentKey.name] = e.keyCode;
                _currentKey.transform.GetChild(0).GetComponent<Text>().text = e.keyCode.ToString();
                _currentKey.GetComponent<Image>().color = _normalColor;
                _currentKey = null;
            }
        }
    }

    public void ChangeKey(GameObject clicked)
    {
        if (_currentKey != null)
        {
            _currentKey.GetComponent<Image>().color = _normalColor;
        }

        _currentKey = clicked;
        _currentKey.GetComponent<Image>().color = _selectedColor;
    }

    public void SaveKeys()
    {
        foreach (var key in keys)
        {
            PlayerPrefs.SetString(key.Key, key.Value.ToString());
        }

        PlayerPrefs.Save();
    }

    private void LoadKeys()
    {
        keys.Add("MoveForwardBtn", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("MoveForwardBtn", "W")));
        keys.Add("MoveBackBtn", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("MoveBackBtn", "S")));
        keys.Add("MoveLeftBtn", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("MoveLeftBtn", "A")));
        keys.Add("MoveRightBtn", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("MoveRightBtn", "D")));
        keys.Add("JumpBtn", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("JumpBtn", "Space")));
    }

    private void AddEventTrigger(GameObject obj, EventTriggerType type, Action<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = obj.AddComponent<EventTrigger>();
        }

        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = type };
        entry.callback.AddListener(new UnityEngine.Events.UnityAction<BaseEventData>(action));
        trigger.triggers.Add(entry);
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