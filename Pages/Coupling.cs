public class Coupling
{
    private List<string> _displayText;
    private string _label;
    private double _x;
    private double _y;

    public Coupling(string Name, double x, double y, string directionX, string directionY, string notGroup, string outputFn, string label, string toFn, string toType)
    {
        this.Name = Name;
        _x = x;
        _y = y;
        this.directionX = directionX;
        this.directionY = directionY;
        this.notGroup = notGroup;
        this.outputFn = outputFn;
        this.toFn = toFn;
        _label = label;
        _displayText = returnTextLines(_label, 15);
        this.toType = toType;
    }
    public string UpdateCurve()
    {
        string[] curve = new string[10]
        {
            drawTox.ToString("#.##"),
            drawToy.ToString("#.##"),
            drawFromx.ToString("#.##"),
            drawFromy.ToString("#.##"),
            drawAx.ToString("#.##"),
            drawAy.ToString("#.##"),
            drawBx.ToString("#.##"),
            drawBy.ToString("#.##"),
            drawIntx.ToString("#.##"),
            drawInty.ToString("#.##")
        };
        return string.Join("|", curve);
    }
    public string UpdateCurve2()
    {
        string[] curve2 = new string[13] {
            "M", drawFromx.ToString("#.##"), drawFromy.ToString("#.##"),
            "Q", drawAx.ToString("#.##"), drawAy.ToString("#.##"), drawIntx.ToString("#.##"), drawInty.ToString("#.##"),
            "Q", drawBx.ToString("#.##"), drawBy.ToString("#.##"), drawTox.ToString("#.##"), drawToy.ToString("#.##")
        };
        resetPosition();
        return string.Join(" ", curve2);
    }
    public string Name { get; set; }
    public double x
    {
        get { return _x; }
        set
        {
            _x = value;
            resetPosition();
        }
    }
    public double y
    {
        get { return _y; }
        set
        {
            _y = value;
            resetPosition();
        }
    }
    public string directionX { get; set; }
    public string directionY { get; set; }
    public string notGroup { get; set; }
    public string outputFn { get; set; }
    public string toFn { get; set; }
    public string label
    {
        get { return _label; }
        set { _label = value;
            _displayText = returnTextLines(_label, 15);
            resetPosition();
        }
    }
    public double labelX { get; set; }
    public double labelY { get; set; }
    public double Twidth { get; set; }
    public string toType { get; set; }
    public double drawTox { get; set; }
    public double drawToy { get; set; }
    public double drawFromx { get; set; }
    public double drawFromy { get; set; }
    public double drawAx { get; set; }
    public double drawAy { get; set; }
    public double drawBx { get; set; }
    public double drawBy { get; set; }
    public double drawIntx { get; set; }
    public double drawInty { get; set; }
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
    public void resetPosition()
    {
        double tempRwidth = 0;
        if (directionX == "from")
        {
            labelX = (drawIntx + x * (drawFromx - drawIntx));
            for (int i = 0; i < _displayText.Count; i++)
            {
                tempRwidth = Math.Max(tempRwidth, _displayText[i].Length);
            }
        }
        else
        {
            labelX = (drawIntx + x * (drawIntx - drawTox));
            for (int i = 0; i < _displayText.Count; i++)
            {
                tempRwidth = Math.Max(tempRwidth, _displayText[i].Length);
            }
        }
        Twidth = tempRwidth * 4 + 4;
        if (directionY == "from")
        {
            labelY = (drawInty + y * (drawFromy - drawInty) - (2.5 + _displayText.Count * 4));
        }
        else
        {
            labelY = (drawInty + y * (drawInty - drawToy) - (2.5 + _displayText.Count * 4));
        }
    }
}
