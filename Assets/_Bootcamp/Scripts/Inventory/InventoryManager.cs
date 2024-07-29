using System;
using System.Collections.Generic;
using _Bootcamp.Scripts.Interactable;
using _Bootcamp.Scripts.Player;
using _Bootcamp.Scripts.UI;
using DG.Tweening;
using ModestTree;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Transform ItemContent;
    public GameObject InventoryItem;

    [SerializeField] private int _itemCapacity;
    [SerializeField] private GameObject _emptyUi;
    [SerializeField] private Transform _border;
    [SerializeField] private PlayerInputController _playerInput;
    [SerializeField] private TextMeshProUGUI _infoText;
    
    private CollectableController[] _items;
    private List<GameObject> _uiList = new ();
    private int _currentItemIndex;

    private void Start()
    {
        // başta belirlenen kapasitede bir array oluşturuyor
        _items = new CollectableController[_itemCapacity];
    }

    private void Update()
    {
        if (_playerInput.Scroll() > 0)
            NextItem();
        else if (_playerInput.Scroll() < 0)
            PreItem();
    }

    public void Add(CollectableController item)
    {
        // items içerisinde yer varsa ekliyor 

        for (var i = 0; i < _items.Length; i++)
        {
            if (_items[i] != null) continue;
            
            _items[i] = item;

            var newUi = Instantiate(_emptyUi, transform);
            _uiList.Add(newUi);
            newUi.transform.localScale = Vector3.zero;

            var hoverElement = newUi.GetComponent<HoverElement>();
            hoverElement.text = _infoText;
            hoverElement.explain = item.data.info;
            var icon = newUi.transform.Find("Icon").GetComponent<Image>();
            icon.sprite = item.data.icon;

            newUi.transform.DOScale(Vector3.one, .2f);

            break;
        }
    }

    public void Remove(CollectableController item)
    {
        var index = _items.IndexOf(item);
        _items[index] =  null;
        _uiList[index].transform.DOScale(Vector3.zero, .2f);
        _uiList.RemoveAt(index);
    }

    public void ListItems()
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in _items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<Text>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

            itemName.text = item.data.itemName;
            itemIcon.sprite = item.data.icon;
        }
    }

    public void NextItem()
    {
        _currentItemIndex++;
        if (_currentItemIndex >= _uiList.Count)
            _currentItemIndex = 0;

        _border.SetParent(_uiList[_currentItemIndex].transform);
        _border.localPosition = Vector3.zero;
        _border.gameObject.SetActive(true);
    }

    public void PreItem()
    {
        _currentItemIndex--;
        if (_currentItemIndex < 0)
            _currentItemIndex = _uiList.Count - 1;

        _border.SetParent(_uiList[_currentItemIndex].transform);
        _border.localPosition = Vector3.zero;
        _border.gameObject.SetActive(true);
    }
    
    public void SelectItem()
    {
        
    }
}
