public class Function
{
    private string _label;
    private string _fnStyle;
    private string _FunctionType;
    public Function(string IDNr, double x, double y, string fnStyle, string FunctionType, string label, int orphans, string isInput, string fnColorStyle, string fnColorValue )
    {
        this.IDNr = IDNr;
        this.x = x;
        this.y = y;
        _fnStyle = fnStyle;
        _FunctionType = FunctionType;
        _label = label;
        this.orphans = orphans;
        this.isInput = isInput;
        this.fnColorStyle = fnColorStyle;
        this.fnColorValue = fnColorValue;
        options = fnStyle + ":" + FunctionType + ":";
    }
    public string options { get; set; }
    public string IDNr { get; set; }
    public int orphans { get; set; }
    public string isInput { get; set; }
    public string fnColorStyle { get; set; }
    public string fnColorValue { get; set; }
    public double x { get; set; }
    public double y { get; set; }
    public bool dragFn { get; set; } = false;
    public string fnClass { get; set; } = "fn-hover";
    public string fnStyle
    {
        get { return _fnStyle; }
        set { 
            _fnStyle = value;
            options = _fnStyle + ":" + _FunctionType + ":";
        }
    }
    public string FunctionType
    {
        get { return _FunctionType; }
        set { 
            _FunctionType = value;
            options = _fnStyle + ":" + _FunctionType + ":";
        }
    }
    public string label
    {
        get { return _label; }
        set { _label = value;
        }
    }
    public List<string> ReturnTextLines(int length)
    {
        var textLines = new List<string>();
        string[] textWords = _label.Split(" ");
        int tL = 0;
        textLines.Add("");
        int lL = length;
        foreach (string tWord in textWords)
        {
            var doTWord = tWord;
            if (doTWord.StartsWith(Environment.NewLine))
            {
                textLines.Add("");
                tL++;
                lL = length;
                doTWord = tWord.Replace(Environment.NewLine, "");
            } 
            if (doTWord.Contains(Environment.NewLine) && !doTWord.EndsWith(Environment.NewLine))
            {
                if (lL < length)
                {
                    textLines[tL] += " ";
                }
                string[] splitLines = doTWord.Split(Environment.NewLine);
                int sLi = 0;
                foreach (string splitWord in splitLines)
                {
                    doTWord = splitWord;
                    if (sLi < splitLines.Length-1)
                    {
                        textLines[tL] += splitWord;
                        textLines.Add("");
                        tL++;
                        lL = length;
                    }
                    sLi ++;
                }
            }
            if (doTWord.Length <= lL)
            {
                if (lL < length)
                {
                    textLines[tL] += " ";
                }
                textLines[tL] += doTWord;
                lL -= doTWord.Length;
                if (doTWord.EndsWith(Environment.NewLine))
                {
                    textLines.Add("");
                    tL++;
                    lL = length;
                }
            }
            else if (doTWord.Length > length)
            {
                string tempWord = doTWord;
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
                textLines[tL] += doTWord;
                lL = length - doTWord.Length;
            }
        }
        return textLines;
    }
}
