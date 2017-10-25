using System.Collections;
using ExcelParser;

/// <summary>
/// 自动生成类。不要修改
/// 数据表的第一列为key
/// </summary>
public class SkillBean : IDataBean {

    private int skillID;
    /// <summary>
    /// SkillID
    /// </summary>
    public int SkillID {
        get {
             return skillID;
        }
        set {
             skillID = value;
        }
    }

    private string skillName;
    /// <summary>
    /// 名称
    /// </summary>
    public string SkillName {
        get {
             return skillName;
        }
        set {
             skillName = value;
        }
    }

    private int isMustCast;
    /// <summary>
    /// 是否必须释放
    /// </summary>
    public int IsMustCast {
        get {
             return isMustCast;
        }
        set {
             isMustCast = value;
        }
    }

    private int isBuff;
    /// <summary>
    /// IsBuff
    /// </summary>
    public int IsBuff {
        get {
             return isBuff;
        }
        set {
             isBuff = value;
        }
    }

    private int castCondition;
    /// <summary>
    /// CastCondition
    /// </summary>
    public int CastCondition {
        get {
             return castCondition;
        }
        set {
             castCondition = value;
        }
    }

    private int activeCondition;
    /// <summary>
    /// 激活条件
    /// </summary>
    public int ActiveCondition {
        get {
             return activeCondition;
        }
        set {
             activeCondition = value;
        }
    }

    private int effectType;
    /// <summary>
    /// EffectType
    /// </summary>
    public int EffectType {
        get {
             return effectType;
        }
        set {
             effectType = value;
        }
    }

    private int effectValue;
    /// <summary>
    /// EffectValue
    /// </summary>
    public int EffectValue {
        get {
             return effectValue;
        }
        set {
             effectValue = value;
        }
    }

    private int effectValue2;
    /// <summary>
    /// EffectValue2
    /// </summary>
    public int EffectValue2 {
        get {
             return effectValue2;
        }
        set {
             effectValue2 = value;
        }
    }

    private int specifyTarget;
    /// <summary>
    /// SpecifyTarget
    /// </summary>
    public int SpecifyTarget {
        get {
             return specifyTarget;
        }
        set {
             specifyTarget = value;
        }
    }

    private int specifyTarget2;
    /// <summary>
    /// SpecifyTarget2
    /// </summary>
    public int SpecifyTarget2 {
        get {
             return specifyTarget2;
        }
        set {
             specifyTarget2 = value;
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

    private string desMore;
    /// <summary>
    /// D更多介绍
    /// </summary>
    public string DesMore {
        get {
             return desMore;
        }
        set {
             desMore = value;
        }
    }
}
