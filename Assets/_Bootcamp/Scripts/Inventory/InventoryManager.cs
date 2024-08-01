using System;
using System.Collections;
using System.Collections.Generic;
using _Bootcamp.Scripts.Interactable;
using _Bootcamp.Scripts.Player;
using _Bootcamp.Scripts.UI;
using DG.Tweening;
using ModestTree;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Sequence = DG.Tweening.Sequence;

public class InventoryManager : MonoBehaviour
{
    public Transform ItemContent;
    public GameObject InventoryItem;

    [SerializeField] private int _itemCapacity;
    [SerializeField] private GameObject _emptyUi;
    [SerializeField] private Transform _border;
    [SerializeField] private GameObject _penguinPrefab;
    //[SerializeField] private PlayerInputController _playerInput;
    [SerializeField] private TextMeshProUGUI _infoText;
    [SerializeField] private float stoppingDistance = 5f;
    private bool canSummonPenguin = true;
    [SerializeField]private List<GameObject> _ghostSpawn;
    private CollectableController[] _items;
    private List<GameObject> _uiList = new ();
    private int _currentItemIndex;
    private bool _isGlassesUsed;

    private void Start()
    {
        // başta belirlenen kapasitede bir array oluşturuyor
        _items = new CollectableController[_itemCapacity];
    }

    private void Update()
    {
       // if (_playerInput.Scroll() > 0)
        //    NextItem();
        //else if (_playerInput.Scroll() < 0)
        //    PreItem();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _isGlassesUsed = !_isGlassesUsed;

            if (_isGlassesUsed)
            {
                SpawnGhosts();
            }
            else if (!_isGlassesUsed)
            {
                RemoveGhosts();
            }

        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && canSummonPenguin)
        {
            StartCoroutine(CallPenguin());
            UseItem(1);
        }

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

    public void UseItem(int index)
    {
        if (index >= 0 && index < _items.Length)
        {
            _items[index].Use();
        }
    }
    private IEnumerator CallPenguin()
    {
        canSummonPenguin = false;
        GameObject penguin = Instantiate(_penguinPrefab);

        penguin.transform.localScale = Vector3.zero; 
        Sequence spawnSequence = DOTween.Sequence();
        spawnSequence.Append(penguin.transform.DOScale(Vector3.one*2, 3f).SetEase(Ease.OutBounce)); 
        spawnSequence.Join(penguin.transform.DORotate(new Vector3(0, 360, 0), 1f, RotateMode.FastBeyond360)); 

        yield return spawnSequence.WaitForCompletion(); 

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float enhancedStoppingDistance = stoppingDistance + 2f;
            while (penguin != null && Vector3.Distance(penguin.transform.localPosition, player.transform.localPosition) > stoppingDistance)
            {
                penguin.transform.localPosition = Vector3.MoveTowards(penguin.transform.localPosition, player.transform.localPosition, 5f * Time.deltaTime);
                penguin.transform.LookAt(player.transform);
                yield return null;
            }
        }
        yield return new WaitForSeconds(40);
        Destroy(penguin);
        canSummonPenguin = true;
    }

    private void SpawnGhosts()
    {
        foreach (var item in _ghostSpawn)
        {
            item.SetActive(true);
        }
    }
    private void RemoveGhosts()
    {
        foreach (var item in _ghostSpawn)
        {
            item.SetActive(false);
        }
    }
}
