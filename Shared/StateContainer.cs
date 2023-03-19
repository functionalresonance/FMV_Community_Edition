using System.Xml;
using System.Xml.Linq;

namespace FMV_Standard.Shared
{
    public class StateContainer
    {
        public string selectedFn { get; set; } = "-1";
        public string touchAction { get; set; } = "auto";
        public int newFnStyle { get; set; } = 0;
        public string selectedLabel { get; set; } = "";
        public string AspectLabelsDisplay { get; set; } = "";
        public double tempZoomF { get; set; } = 1;
        public double tempZoomA { get; set; } = 1;
        public double scaleZoom { get; set; } = 1.5;
        public double canvasWidth { get; set; }
        public double canvasHeight { get; set; }
        public double viewWidth { get; set; } = 0;
        public double viewHeight { get; set; } = 0;
        public XmlDocument[] projectData_Undo { get; set; } = new XmlDocument[10];
        public List<Coupling> couplingList { get; set; } = new List<Coupling>();
        public List<Function> functionList { get; set; } = new List<Function>();
        public string fnName { get; set; } = "";
        public bool isDisabled { get; set; } = true;
        public void defaultFnLabel()
        {
            projectData_Undo[0].SelectSingleNode("//FM/Functions/Function[IDNr=" + selectedFn + "]/IDName")!.InnerText = selectedFn;
            functionList.Find(x => x.IDNr == selectedFn)!.label = selectedFn;
        }
        public void sFnIsInput(string isInput)
        {
            projectData_Undo[0].SelectSingleNode("//FM/Functions/Function[IDNr=" + selectedFn + "]/@isInput")!.InnerText = isInput;
            functionList.Find(x => x.IDNr == selectedFn)!.isInput = isInput;
        }
        public void sFnOrphans(int orphans)
        {
            projectData_Undo[0].SelectSingleNode("//FM/Functions/Function[IDNr=" + selectedFn + "]/@orphans")!.InnerText = orphans.ToString();
            functionList.Find(x => x.IDNr == selectedFn)!.orphans = orphans;
        }
        public void sFnFunctionType(string FunctionType)
        {
            projectData_Undo[0].SelectSingleNode("//FM/Functions/Function[IDNr=" + selectedFn + "]/FunctionType")!.InnerText = FunctionType;
            functionList.Find(x => x.IDNr == selectedFn)!.FunctionType = FunctionType;
        }
        public void sFnIDName(string IDName)
        {
            projectData_Undo[0].SelectSingleNode("//FM/Functions/Function[IDNr=" + selectedFn + "]/IDName")!.InnerText = IDName;
            functionList.Find(x => x.IDNr == selectedFn)!.label = IDName;
        }
        public void updateFnXY(double x, double y)
        {
            projectData_Undo[0].SelectSingleNode("//FM/Functions/Function[IDNr=" + selectedFn + "]/@x")!.InnerText = x.ToString("#.##");
            projectData_Undo[0].SelectSingleNode("//FM/Functions/Function[IDNr=" + selectedFn + "]/@y")!.InnerText = y.ToString("#.##");
        }
        public void updateAspectXY(double x, double y, string directionX, string directionY)
        {
            projectData_Undo[0].SelectSingleNode("//FM/Aspects/Aspect[Name=\"" + selectedLabel + "\"]/@x")!.InnerText = x.ToString("#.##");
            projectData_Undo[0].SelectSingleNode("//FM/Aspects/Aspect[Name=\"" + selectedLabel + "\"]/@y")!.InnerText = y.ToString("#.##");
            projectData_Undo[0].SelectSingleNode("//FM/Aspects/Aspect[Name=\"" + selectedLabel + "\"]/@directionX")!.InnerText = directionX;
            projectData_Undo[0].SelectSingleNode("//FM/Aspects/Aspect[Name=\"" + selectedLabel + "\"]/@directionY")!.InnerText = directionY;
        }
    }
}
