using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EFK.Stats
{
    [CreateAssetMenu(fileName = "Characters", menuName = "Characters")]
    public class CharactersStats : ScriptableObject
    {
        [SerializeField] Character[] characters = null;

        public Dictionary<Stat, float> statLookupDict = null;

        public Dictionary<int, int> levelLookUpDict = null;

        public float GetStat(CharacterType characterType, Stat stat)
        {
            //Ensures that the first time someone needs a stat it creates the lookup dictionary
            BuildStatLookupDict(characterType);

            return statLookupDict[stat];
        }

        public int GetLevelList(CharacterType characterType, int choiceNumber)
        {
            //Ensures that the first time someone needs the list, its created
            BuildLevelList(characterType);

            return levelLookUpDict[choiceNumber];
        }

        private void BuildLevelList(CharacterType _characterType)
        {
            if (levelLookUpDict != null) return;

            foreach (Character character in characters)
            {
                if (_characterType != character.characterType) continue;
                levelLookUpDict = new Dictionary<int, int>();
                
                for(int x = 0; x < character.levelList.Length; x++)
                {
                    levelLookUpDict[x] = character.levelList[x];
                }
            }
        }

        //Creates a fast character reference only with one character information in a Dictionary so searching is cheaper
        private void BuildStatLookupDict(CharacterType _characterType)
        {
            if (statLookupDict != null) return;

            statLookupDict = new Dictionary<Stat, float>();

            foreach (Character character in characters)
            {
                if (_characterType != character.characterType) continue;

                foreach(Stats stat in character.stats)
                {
                    statLookupDict[stat.stat] = stat.value;
                }
            }
        }

    }

    [System.Serializable]
    public class Character
    {
        public CharacterType characterType;
        public Stats[] stats;
        public int[] levelList;
    }

    [System.Serializable]
    public class Stats
    {
        public Stat stat;
        public int value;
    }
}
