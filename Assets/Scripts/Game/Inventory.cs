// using System;
// using System.Collections;
// using System.Collections.Generic;
// using TMPro;
// using UnityEngine;
// using UnityEngine.UI;
//
// public class Inventory : MonoBehaviour
// {
//    [SerializeField] private KeyCardHolder _keyCardHolder;
//    [SerializeField] private Transform _container;
//    [SerializeField] private Transform _template;
//
//    private void Awake()
//    {
//       _template.gameObject.SetActive(false);
//    }
//
//    private void Start()
//    {
//       _keyCardHolder.OnKeyCardsChanged += Inventory_OnKeysChanged;
//    }
//
//    private void Inventory_OnKeysChanged(object sender, System.EventArgs e)
//    {
//       UpdateInventory();
//    }
//
//    private void UpdateInventory()
//    {
//       //Cleans old keys
//       CleanUp();
//       //Instantiates current key list
//       List<KeyCard.KeyType> keyList = _keyCardHolder.GetKeyList();
//       if (keyList.Count == 0)
//       {
//          _template.gameObject.SetActive(false);
//       }
//       for (int i = 0; i < keyList.Count; i++)
//       {
//          KeyCard.KeyType keyType = keyList[i];
//          Transform keyTransform = Instantiate(_template, _container);
//          _template.gameObject.SetActive(true);
//          keyTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(50 * i, 0);
//          Image keyCardImage = keyTransform.Find("KeyCardImage").GetComponent<Image>();
//          Debug.Log("KEY TYPE: " + keyType);
//          switch (keyType)
//          {
//             case KeyCard.KeyType.Blue: keyCardImage.color = Color.blue;   break;
//             case KeyCard.KeyType.Green: keyCardImage.color = Color.green; break;
//             
//          }
//       }
//    }
//
//    private void CleanUp()
//    {
//       foreach (Transform child in _container)
//       {
//          if (child == _template) continue;
//          Destroy(child.gameObject);
//       }
//    }
// }
