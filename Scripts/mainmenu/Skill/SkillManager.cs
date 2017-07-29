using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour {

    public TextAsset SkillText;
    public ArrayList SkillList = new ArrayList();
    public static SkillManager _instance;


    private void Awake()
    {
        ReadSkillList();
        _instance = this;
    }
    private void ReadSkillList()
    {
        string Str = SkillText.ToString();
        string[] rowArray = Str.Split('\n');
        string[] colArray;
        foreach (string row in rowArray)
        {
            colArray = row.Split(',');
            SkillInfo item = new SkillInfo();

            item.Id = int.Parse(colArray[0]);
            item.Name = colArray[1];
            item.Icon = colArray[2];
            switch (colArray[3])
            {
                case "Warrior":
                    item.PlayerType = PlayerType.Warrior;
                    break;
                case "FemaleAssassin":
                    item.PlayerType = PlayerType.FemaleAssassin;
                    break;
            }
            switch (colArray[4])
            {
                case "Basic":
                    item.SkillType = SkillType.Basic;
                    break;
                case "Skill":
                    item.SkillType = SkillType.Skill;
                    break;
            }
            switch (colArray[5])
            {
                case "Basic":
                    item.PosType = PosType.Basic;
                    break;
                case "One":
                    item.PosType = PosType.One;
                    break;
                case "Two":
                    item.PosType = PosType.Two;
                    break; 
                case "Three":
                    item.PosType = PosType.Three;
                    break;
            }
            item.ColdTime = int.Parse(colArray[6]);
            item.BaseDamage = int.Parse(colArray[7]);
            SkillList.Add(item);
        }
    }

    public SkillInfo GetSkillByPosType(PosType pos)
    {
        foreach(SkillInfo skill in SkillList)
        {
            if(skill.PlayerType == PlayerInfo._instance.PlayerType && skill.PosType == pos)
            {
                return skill;
            }
        }
        return null;
    }

}
