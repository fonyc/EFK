using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EFK.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [SerializeField] CharacterType characterType;
        public CharactersStats characters;
    }
}
