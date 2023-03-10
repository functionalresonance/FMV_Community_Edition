public class Function
{
    private List<string> _displayText;
    private string _label;
    private double _x;
    private double _y;
    private string _fnStyle;
    private string _FunctionType;

    public Function(string IDNr, double x, double y, string fnStyle, string FunctionType, string label, int orphans, string isInput, string fnColorStyle, string fnColorValue )
    {
        this.IDNr = IDNr;
        _x = x;
        _y = y;
        _fnStyle = fnStyle;
        _FunctionType = FunctionType;
        _label = label;
        _displayText = returnTextLines(_label, 12);
        this.orphans = orphans;
        this.isInput = isInput;
        this.fnColorStyle = fnColorStyle;
        this.fnColorValue = fnColorValue;
        options = "fnStyle" + fnStyle + ":fnType" + FunctionType + ":";
    }
    public string options { get; set; }
    public string IDNr { get; set; }
    public int orphans { get; set; }
    public string isInput { get; set; }
    public string fnColorStyle { get; set; }
    public string fnColorValue { get; set; }
    public double x
    {
        get { return _x; }
        set
        {
            _x = value;
        }
    }
    public double y
    {
        get { return _y; }
        set
        {
            _y = value;
        }
    }
    public string fnStyle
    {
        get { return _fnStyle; }
        set { 
            _fnStyle = value;
            options = "fnStyle" + _fnStyle + ":fnType" + _FunctionType + ":";
        }
    }
    public string FunctionType
    {
        get { return _FunctionType; }
        set { 
            _FunctionType = value;
            options = "fnStyle" + _fnStyle + ":fnType" + _FunctionType + ":";
        }
    }
    public string label
    {
        get { return _label; }
        set { _label = value;
            _displayText = returnTextLines(_label, 15);
        }
    }
    public List<string> displayText
    {
        get { return _displayText; }
        set { _displayText = value; }
    }
    private List<string> returnTextLines(string text, int length)
    {
        var textLines = new List<string>();
        string[] textWords = text.Split(" ");
        int tL = 0;
        textLines.Add("");
        int lL = length;
        foreach (string tWord in textWords)
        {
            if (tWord.Length <= lL)
            {
                if (lL < length)
                {
                    textLines[tL] += " ";
                }
                textLines[tL] += tWord;
                lL -= tWord.Length;
            }
            else if (tWord.Length > length)
            {
                string tempWord = tWord;
                while (tempWord.Length > 0)
                {
                    textLines.Add("");
                    tL++;
                    lL = length;
                    string addWord = tempWord.Substring(0, Math.Min(length + 1, tempWord.Length));
                    textLines[tL] += addWord;
                    lL -= addWord.Length;
                    tempWord = tempWord.Substring(addWord.Length);
                }
            }
            else
            {
                textLines.Add("");
                tL++;
                textLines[tL] += tWord;
                lL = length - tWord.Length;
            }
        }
        return textLines;
    }
}
