using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CipherDecryption
{
    public class FrequencyAnalysis
    {
        public List<CharacterFrequency> PlainCharacterFrequencies = new List<CharacterFrequency>();

        public FrequencyAnalysis(string text)
        {
            string[] splitText = text.Split("\r\n");

            foreach (var frequency in splitText)
            {
                string[] test = frequency.Split(",");

                PlainCharacterFrequencies.Add(new CharacterFrequency(test[0][0], float.Parse(test[1])));
            }
        }

        public List<CharacterFrequency> GetCharacterFrequencies(CipherText text)
        {
            Dictionary<char, CharacterFrequency> characterFrequencies = new Dictionary<char, CharacterFrequency>();
            //Make ciphertext implement IEnumberable
            //Checks how many occurrences of a character there are in a cipherText
            foreach (var CipherCharacter in text.Text)
            {
                if (!characterFrequencies.ContainsKey(CipherCharacter.cipherCharacter))
                {
                    characterFrequencies.Add(CipherCharacter.cipherCharacter, new CharacterFrequency(CipherCharacter.cipherCharacter));
                }

                characterFrequencies[CipherCharacter.cipherCharacter].Frequency++;
            }

            foreach (var characterFrequency in characterFrequencies.Values)
            {
                characterFrequency.Frequency /= text.Text.Count;
            }

            return characterFrequencies.Values.ToList();
        }

        public void PerformFrequencyAnalysis(List<CharacterFrequency> cipherCharacterFrequencies, CipherText text)
        {
            PlainCharacterFrequencies = PlainCharacterFrequencies.OrderByDescending(x => x.Frequency).ToList();
            cipherCharacterFrequencies = cipherCharacterFrequencies.OrderByDescending(x => x.Frequency).ToList();

            text.Text.Find(x => x.cipherCharacter == cipherCharacterFrequencies[0].Character).cipherCharacter =
                PlainCharacterFrequencies[0].Character;

            Console.WriteLine(PlainCharacterFrequencies[0].Character);
            Console.WriteLine(cipherCharacterFrequencies[0].Character);

        }

        public class CharacterFrequency
        {
            public char Character;
            public float Frequency;

            public CharacterFrequency(char character)
            {
                Character = character;
                Frequency = 0;
            }

            public CharacterFrequency(char character, float frequency)
            {
                Character = character;
                Frequency = frequency;
            }
        }
    }
}
