using System.Collections;
using ExcelParser;

/// <summary>
/// 自动生成类。不要修改
/// 数据表的第一列为key
/// </summary>
public class CardData : IDataBean {

    private int cardID;
    /// <summary>
    /// CardID
    /// </summary>
    public int CardID {
        get {
             return cardID;
        }
        set {
             cardID = value;
        }
    }

    private string cardName;
    /// <summary>
    /// CardName
    /// </summary>
    public string CardName {
        get {
             return cardName;
        }
        set {
             cardName = value;
        }
    }

    private int posType;
    /// <summary>
    /// PosType
    /// </summary>
    public int PosType {
        get {
             return posType;
        }
        set {
             posType = value;
        }
    }

    private int cardType;
    /// <summary>
    /// CardType
    /// </summary>
    public int CardType {
        get {
             return cardType;
        }
        set {
             cardType = value;
        }
    }

    private int race;
    /// <summary>
    /// 所属派系
    /// </summary>
    public int Race {
        get {
             return race;
        }
        set {
             race = value;
        }
    }

    private int costRes;
    /// <summary>
    /// 消耗金钱
    /// </summary>
    public int CostRes {
        get {
             return costRes;
        }
        set {
             costRes = value;
        }
    }

    private int costPower;
    /// <summary>
    /// 消耗战力
    /// </summary>
    public int CostPower {
        get {
             return costPower;
        }
        set {
             costPower = value;
        }
    }

    private int score;
    /// <summary>
    /// 积分
    /// </summary>
    public int Score {
        get {
             return score;
        }
        set {
             score = value;
        }
    }

    private int exp;
    /// <summary>
    /// 扩展
    /// </summary>
    public int Exp {
        get {
             return exp;
        }
        set {
             exp = value;
        }
    }

    private string bornSkill;
    /// <summary>
    /// 出场技能
    /// </summary>
    public string BornSkill {
        get {
             return bornSkill;
        }
        set {
             bornSkill = value;
        }
    }

    private string dieSkill;
    /// <summary>
    /// 亡语
    /// </summary>
    public string DieSkill {
        get {
             return dieSkill;
        }
        set {
             dieSkill = value;
        }
    }

    private string des;
    /// <summary>
    /// Des
    /// </summary>
    public string Des {
        get {
             return des;
        }
        set {
             des = value;
        }
    }

    private string nameDes;
    /// <summary>
    /// NameDes
    /// </summary>
    public string NameDes {
        get {
             return nameDes;
        }
        set {
             nameDes = value;
        }
    }
}
