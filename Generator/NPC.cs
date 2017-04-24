using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
    /// <summary>
    /// Holds data for NPCs
    /// </summary>
    ///
    public class NPC
    {

        private int npc_id;
        private int strength;
        private int dexterity;
        private int constitution;
        private int intelligence;
        private int wisdom;
        private int charisma;
        private string name;
        private string npcType;
        private string npcRace;
        private string alignment;

        //properties
        public int NPC_ID { get { return npc_id; } set { npc_id = value; } }
        public int Strength { get { return strength; } set { strength = value; } }
        public int Dexterity { get { return dexterity; } set { dexterity = value; } }
        public int Constitution { get { return constitution; } set { constitution = value; } }
        public int Intelligence { get { return intelligence; } set { intelligence = value; } }
        public int Wisdom { get { return wisdom; } set { wisdom = value; } }
        public int Charisma { get { return charisma; } set { charisma = value; } }
        public string Name { get { return name; } set { name = value; } }
        public string Type { get { return npcType; } set { npcType = value; } }
        public string Race { get { return npcRace; } set { npcRace = value; } }
        public string Alignment { get { return alignment; } set { alignment = value; } }


        //constructor
        public NPC() { }
        /// <summary>
        /// Constructor for the 6 basic stats and Name
        /// </summary>
        /// <param name="_str"></param>
        /// <param name="_dex"></param>
        /// <param name="_const"></param>
        /// <param name="_intell"></param>
        /// <param name="_wis"></param>
        /// <param name="_charisma"></param>
        /// <param name="_name"></param>
        public NPC(int _str, int _dex, int _const, int _intell, int _wis, int _charisma, string _name)
        {
            strength = _str;
            dexterity = _dex;
            intelligence = _intell;
            wisdom = _wis;
            constitution = _const;
            charisma = _charisma;
            name = _name;
        }
        //methods
        public override string ToString()
        {
            return name;
        }
        /// <summary>
        /// Turns all NPC stats into text to be saved to a file
        /// </summary>
        /// <returns></returns>
        public string npcToText()
        {
            return npc_id.ToString() + "," + strength.ToString() + "," + dexterity.ToString() + "," +
                constitution.ToString() + "," + intelligence.ToString() + "," +
                wisdom.ToString() + "," + charisma.ToString() + "," + npcType + "," +
                npcRace + "," + alignment + "," + name;
        }
        public double statMod(string stat)
        {
            double modValue;
            switch(stat)
            {
                case "strength":
                case "Strength":
                    modValue = ((double)strength - 10.0) / 2.0;
                    modValue = Math.Floor(modValue);
                    break;
                case "dexterity":
                case "Dexterity":
                    modValue = ((double)dexterity - 10.0) / 2.0;
                    modValue = Math.Floor(modValue);
                    break;
                case "constitution":
                case "Constitution":
                    modValue = ((double)constitution - 10.0) / 2.0;
                    modValue = Math.Floor(modValue);
                    break;
                case "intelligence":
                case "Intelligence":
                    modValue = ((double)intelligence - 10.0) / 2.0;
                    modValue = Math.Floor(modValue);
                    break;
                case "wisdom":
                case "Wisdom":
                    modValue = ((double)wisdom - 10.0) / 2.0;
                    modValue = Math.Floor(modValue);
                    break;
                case "charisma":
                case "Charisma":
                    modValue = ((double)charisma - 10.0) / 2.0;
                    modValue = Math.Floor(modValue);
                    break;
                default:
                    modValue = 0;
                    break;
            }
            return modValue;
        }
    }


}