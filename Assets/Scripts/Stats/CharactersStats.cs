using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EFK.Stats
{
    [CreateAssetMenu(fileName = "Characters", menuName = "Characters")]
    public class CharactersStats : ScriptableObject
    {
        [SerializeField] Character[] characters;

        public Dictionary<CharacterType, Stats> lookupDict = null;

        [System.Serializable]
        public class Character
        {
            public CharacterType character;
            public Stats stats;
        }

        [System.Serializable]
        public class Stats
        {
            public float speed;
            public MeterStats meterStats;
            public float[] levelList;
        }

        [System.Serializable]
        public class MeterStats
        {
            public float curseMeterRate;
            public float monsterMeterRate;
            public float curseMeterCaught;
        }

        public void GetStat()
        {
            //Ensures that the first time someone needs a stat it creates the lookup dictionary
            BuildLookup();
        }

        //Creates a fast character reference in a Dictionary so searching is cheaper
        private void BuildLookup()
        {
            if (lookupDict != null) return;

            lookupDict = new Dictionary<CharacterType, Stats>();

            foreach (Character character in characters)
            {
                lookupDict.Add(character.character, character.stats);
            }
        }
    }

}
