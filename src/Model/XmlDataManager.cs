using System.Collections.Generic;
using System.Xml.Linq;

namespace Model
{
    public class XmlDataManager
    {
        public static List<Point> LoadPoints(string filename)
        {
            var doc = XDocument.Load(filename);
            var points = new List<Point>();
            foreach (var pt in doc.Root.Elements("Point"))
                points.Add(new Point(double.Parse(pt.Element("X").Value), double.Parse(pt.Element("Y").Value)));
            return points;
        }
        public static void SavePoints(string filename, List<Point> points)
        {
            var doc = new XDocument(new XElement("Points"));
            foreach (var p in points)
                doc.Root.Add(new XElement("Point", new XElement("X", p.X), new XElement("Y", p.Y)));
            doc.Save(filename);
        }
    }
}
