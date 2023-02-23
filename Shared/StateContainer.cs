using System.Xml;

namespace BlazorApp4.Shared
{
    public class StateContainer
    {
        public string selectedFn { get; set; } = "-1";
        public string previousFn { get; set; } = "-1";
        public double tempZoomF { get; set; } = 1;
        public double tempZoomA { get; set; } = 1;
        public double scaleZoom { get; set; } = 1.5;
        public double canvasWidth { get; set; }
        public double canvasHeight { get; set; }
        public double viewWidth { get; set; } = 0;
        public double viewHeight { get; set; } = 0;
        public XmlNodeList? sAs { get; set; }
        public XmlDocument[] projectData_Undo { get; set; } = new XmlDocument[10];
        public XmlNode? sFn { get; set; }
    }
}
