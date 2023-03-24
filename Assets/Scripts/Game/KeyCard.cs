using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCard : MonoBehaviour
{
   [SerializeField] private KeyType _keyType;
   public enum KeyType
   {
      Green,
      Blue
   }

   public KeyType GetKeyType()
   {
      return _keyType;
   }
}
