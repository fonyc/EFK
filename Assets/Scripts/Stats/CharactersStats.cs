using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EFK.Stats
{
    [CreateAssetMenu(fileName = "Characters", menuName = "Characters")]
    public class CharactersStats : ScriptableObject
    {
        [SerializeField] Character[] characters = null;

        public Dictionary<Stat, float> lookupDict = null;

        public float GetStat(CharacterType characterType, Stat stat)
        {
            //Ensures that the first time someone needs a stat it creates the lookup dictionary
            BuildLookup(characterType);

            return lookupDict[stat];
        }

        //Creates a fast character reference only with one character information in a Dictionary so searching is cheaper
        private void BuildLookup(CharacterType _characterType)
        {
            if (lookupDict != null) return;

            lookupDict = new Dictionary<Stat, float>();

            foreach (Character character in characters)
            {
                if (_characterType != character.characterType) continue;

                foreach(Stats stat in character.stats)
                {
                    lookupDict[stat.stat] = stat.value;
                }
            }
        }
    }

    [System.Serializable]
    public class Character
    {
        public CharacterType characterType;
        public Stats[] stats;
    }

    [System.Serializable]
    public class Stats
    {
        public Stat stat;
        public float value;
    }
}
