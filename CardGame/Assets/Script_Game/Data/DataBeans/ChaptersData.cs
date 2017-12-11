using System.Collections;
using ExcelParser;

/// <summary>
/// 自动生成类。不要修改
/// 数据表的第一列为key
/// </summary>
public class ChaptersData : IDataBean {

    private int lvID;
    /// <summary>
    /// LvID
    /// </summary>
    public int LvID {
        get {
             return lvID;
        }
        set {
             lvID = value;
        }
    }

    private int lvHard;
    /// <summary>
    /// 难度
    /// </summary>
    public int LvHard {
        get {
             return lvHard;
        }
        set {
             lvHard = value;
        }
    }

    private string lvName;
    /// <summary>
    /// 关卡名称
    /// </summary>
    public string LvName {
        get {
             return lvName;
        }
        set {
             lvName = value;
        }
    }

    private int maxScore;
    /// <summary>
    /// 共有得分
    /// </summary>
    public int MaxScore {
        get {
             return maxScore;
        }
        set {
             maxScore = value;
        }
    }

    private string cardGroup;
    /// <summary>
    /// 卡组配置
    /// </summary>
    public string CardGroup {
        get {
             return cardGroup;
        }
        set {
             cardGroup = value;
        }
    }
}
