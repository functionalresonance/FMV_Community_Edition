using FMV_Standard.Shared;
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
        public string aspectLabelsDisplay { get; set; } = "";
        public double tempZoomF { get; set; } = 1;
        public double tempZoomA { get; set; } = 1;
        public double scaleZoom { get; set; } = 1.5;
        public double canvasWidth { get; set; }
        public double canvasHeight { get; set; }
        public double viewWidth { get; set; } = 0;
        public double viewHeight { get; set; } = 0;
        public XmlDocument[] projectData_Undo { get; set; } = new XmlDocument[700];
        private readonly int undoLength = 700;
        private readonly int moveBack = 200;
        public string[] selectedFn_Undo { get; set; } = new string[700];
        public int undoIndex { get; set; } = 0;
        public List<Coupling> couplingList { get; set; } = new List<Coupling>();
        public List<Function> functionList { get; set; } = new List<Function>();
        public string fnName { get; set; } = "";
        public bool isDisabled { get; set; } = true;
        public List<Aspect> aspectsList { get; set; } = new List<Aspect>();
        public List<Aspect> outputsList { get; set; } = new List<Aspect>();
        public bool showModal { get; set; } = false;
        public bool showColorPicker { get; set; } = false;
        public string fileName { get; set; } = "FMV_new.xfmv";
        public bool fileLoaded { get; set; } = false;
        public string inputDropStatus { get; set; } = "file-input-zone-drop";
        public string debugOutput { get; set; } = "";
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
        public void deleteFn()
        {
            if (aspectsList.FindAll(x => x.FunctionIDNr == selectedFn).Count == 0 && projectData_Undo[0].SelectNodes("//FM//Outputs/Output[FunctionIDNr=\"" + selectedFn + "\"]")?.Count == 0)
            {
                updateUndo();
                projectData_Undo[0].SelectSingleNode("//FM/Functions")!.RemoveChild(projectData_Undo[0].SelectSingleNode("//FM/Functions/Function[IDNr=" + selectedFn + "]")!);
                functionList.RemoveAll(x => x.IDNr == selectedFn);
                couplingList.RemoveAll(x => x.outputFn == selectedFn || x.toFn == selectedFn);
                XmlNodeList oldChildren = projectData_Undo[0].SelectNodes("//FM/Aspects/Aspect[@outputFn='" + selectedFn + "' or @toFn='" + selectedFn + "']")!;
                foreach (XmlNode oldChild in oldChildren)
                {
                    projectData_Undo[0].SelectSingleNode("//FM/Aspects")!.RemoveChild(oldChild);
                }
                selectedFn = "-1";
                isDisabled = true;
                fnName = "";
            }
        }
        public void sFnIDName(string IDName)
        {
            updateUndo();
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
        public void updateColor(string selectedColor)
        {
            updateUndo();
            if (projectData_Undo[0].SelectSingleNode("//FM/Functions/Function[IDNr=" + selectedFn + "]/@style") == null)
            {
                XmlAttribute fnColorStyle = projectData_Undo[0].CreateAttribute("style");
                projectData_Undo[0].SelectSingleNode("//FM/Functions/Function[IDNr=" + selectedFn + "]")!.Attributes!.Append(fnColorStyle);
            }
            projectData_Undo[0].SelectSingleNode("//FM/Functions/Function[IDNr=" + selectedFn + "]/@style")!.InnerText = "custom";
            functionList.Find(x => x.IDNr == selectedFn)!.fnColorStyle = "custom";
            if (projectData_Undo[0].SelectSingleNode("//FM/Functions/Function[IDNr=" + selectedFn + "]/@color") == null)
            {
                XmlAttribute fnColorValue = projectData_Undo[0].CreateAttribute("color");
                projectData_Undo[0].SelectSingleNode("//FM/Functions/Function[IDNr=" + selectedFn + "]")!.Attributes!.Append(fnColorValue);
            }
            projectData_Undo[0].SelectSingleNode("//FM/Functions/Function[IDNr=" + selectedFn + "]/@color")!.InnerText = selectedColor;
            functionList.Find(x => x.IDNr == selectedFn)!.fnColorValue = selectedColor;
        }
        public void updateUndo()
        {
            if (undoIndex > undoLength - 5)
            {
                Array.Copy(projectData_Undo, moveBack + 1, projectData_Undo, 1, undoIndex - moveBack);
                Array.Copy(selectedFn_Undo, moveBack + 1, selectedFn_Undo, 1, undoIndex - moveBack);
                undoIndex -= moveBack;
            }
            undoIndex += 1;
            projectData_Undo[undoIndex] = (XmlDocument)projectData_Undo[0].Clone();
            selectedFn_Undo[undoIndex] = selectedFn;
            projectData_Undo[undoIndex + 1] = new XmlDocument();
            projectData_Undo[undoIndex + 2] = new XmlDocument();
        }
        public void reSetAspect(string[] dictArray, double xO, double yO, double aX, double aY, int aFW)
        {
            var aName = string.Join("|", dictArray);
            bool appendNew = false;
            double labelDx = 0;
            double labelDy = 0;
            string directionX = "from";
            string directionY = "to";
            string notGroup = "true";
            XmlNode? aspectI = projectData_Undo[0].SelectSingleNode("//FM/Aspects/Aspect[Name=\"" + aName + "\"]");
            if (aspectI == null)
            {
                aspectI = projectData_Undo[0].CreateElement("Aspect");
                XmlAttribute aspectIx = projectData_Undo[0].CreateAttribute("x");
                aspectIx.Value = "0";
                aspectI.Attributes!.Append(aspectIx);
                XmlAttribute aspectIy = projectData_Undo[0].CreateAttribute("y");
                aspectIy.Value = "0";
                aspectI.Attributes.Append(aspectIy);
                XmlAttribute aspectIdx = projectData_Undo[0].CreateAttribute("directionX");
                aspectIdx.Value = "from";
                aspectI.Attributes.Append(aspectIdx);
                XmlAttribute aspectIdy = projectData_Undo[0].CreateAttribute("directionY");
                aspectIdy.Value = "to";
                aspectI.Attributes.Append(aspectIdy);
                XmlAttribute aspectIof = projectData_Undo[0].CreateAttribute("outputFn");
                aspectIof.Value = dictArray[0];
                aspectI.Attributes.Append(aspectIof);
                XmlAttribute aspectItf = projectData_Undo[0].CreateAttribute("toFn");
                aspectItf.Value = dictArray[2];
                aspectI.Attributes.Append(aspectItf);
                XmlNode aspectIname = projectData_Undo[0].CreateElement("Name");
                aspectIname.InnerText = aName;
                aspectI.AppendChild(aspectIname);
                appendNew = true;
            }
            else
            {
                labelDx = double.Parse(aspectI.SelectSingleNode("@x")?.InnerText ?? "0");
                labelDy = double.Parse(aspectI.SelectSingleNode("@y")?.InnerText ?? "0");
                directionX = aspectI.SelectSingleNode("@directionX")?.InnerText ?? "from";
                directionY = aspectI.SelectSingleNode("@directionY")?.InnerText ?? "to";
                aspectI.RemoveChild(aspectI.SelectSingleNode("Curve")!);
                if (aspectI.SelectSingleNode("Curve2") != null)
                {
                    aspectI.RemoveChild(aspectI.SelectSingleNode("Curve2")!);
                }
                if (aspectI.SelectSingleNode("@notGroup") != null)
                {
                    notGroup = aspectI.SelectSingleNode("@notGroup")?.InnerText ?? "true";
                }
            }
            XmlNode aspectIcurve = projectData_Undo[0].CreateElement("Curve");
            XmlNode aspectIcurve2 = projectData_Undo[0].CreateElement("Curve2");
            Coupling addCoupling = new(aName, labelDx, labelDy, directionX, directionY, notGroup, dictArray[0], dictArray[1], dictArray[2], dictArray[3],
                functionList.Find(x => x.IDNr == dictArray[0])!.x + xO, functionList.Find(x => x.IDNr == dictArray[0])!.y + yO, 0, 0);
            couplingList.Add(addCoupling);
            addCoupling.ReturnTextLines(aFW);
            aspectIcurve2.InnerText = addCoupling.reDrawLines(functionList.Find(x => x.IDNr == dictArray[2])!.x + aX,
                functionList.Find(x => x.IDNr == dictArray[2])!.y +aY, false);
            aspectIcurve.InnerText = addCoupling.curve;
            aspectI.AppendChild(aspectIcurve);
            aspectI.AppendChild(aspectIcurve2);
            if (appendNew) projectData_Undo[0].SelectSingleNode("//FM/Aspects")!.AppendChild(aspectI);
        }
    }
}
