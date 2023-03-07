public class Aspect
{
    private string type;
    private string index;
    private string orphan;
    private string name;
    private string fnIDNr;

    public Aspect(string type, string index, string orphan, string name, string fnIDNr)
    {
        this.type = type;
        this.index = index;
        this.orphan = orphan;
        this.name = name;
        this.fnIDNr = fnIDNr;
    }
    public string Type
    {
        get { return type; }
        set { type = value; }
    }
    public string IDNr
    {
        get { return index; }
        set { index = value; }
    }
    public string Orphan
    {
        get { return orphan; }
        set { orphan = value; }
    }
    public string IDName
    {
        get { return name; }
        set { name = value; }
    }
    public string FunctionIDNr
    {
        get { return fnIDNr; }
        set { fnIDNr = value; }
    }
}
