using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Morken.SisMon.Negocio
{
    public class KmlReader
    {
        private int Orden = 0;
        public List<Morken.SisMon.Entidad.path_class> path_list;

        public static string fileName = string.Format("{0}{1}",
                    System.Web.HttpContext.Current.Server.MapPath("~"),
                    "\\Content\\Mapa.kml");

        public void CargarMapa()
        {
            try
            {
                string text = string.Format("file://{0}", fileName);

                XmlReader xmlReader = XmlReader.Create(text);
                if (!xmlReader.ReadToFollowing("Document"))
                {
                }
                else
                {
                    this.path_list = new List<Morken.SisMon.Entidad.path_class>();
                    this.Orden = 0;
                    this.read_contained_paths(xmlReader, "", this.path_list);
                    xmlReader.Close();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void read_contained_paths(XmlReader xml_reader, string container_path, List<Morken.SisMon.Entidad.path_class> path_list)
        {
            if (!xml_reader.ReadToDescendant("name")) { return; }
            string text = xml_reader.ReadElementContentAsString();
            while (xml_reader.Read())
            {
                if (xml_reader.NodeType == XmlNodeType.EndElement) { return; }
                if (xml_reader.NodeType == XmlNodeType.Element)
                {
                    if (string.Compare(xml_reader.Name, "Placemark") == 0)
                    {
                        if (!xml_reader.ReadToDescendant("name")) { return; }
                        string text2 = xml_reader.ReadElementContentAsString();
                        if (xml_reader.ReadToNextSibling("LineString"))
                        {
                            if (!xml_reader.ReadToDescendant("coordinates")) { return; }
                            string text3 = xml_reader.ReadElementContentAsString().Trim();

                            Morken.SisMon.Entidad.path_class item = new Morken.SisMon.Entidad.path_class(
                                this.Orden++,
                                string.Concat(new object[] { container_path, '/', text, '/', text2 }),
                                text3);

                            path_list.Add(item);
                            xml_reader.ReadEndElement();
                            xml_reader.ReadEndElement();
                        }
                        else
                        {
                            xml_reader.Skip();
                        }
                    }
                    else if (string.Compare(xml_reader.Name, "Folder") == 0)
                    {
                        this.read_contained_paths(xml_reader, container_path + '/' + text, path_list);
                        xml_reader.Skip();
                    }
                    else if (string.Compare(xml_reader.Name, "Document") == 0)
                    {
                        this.read_contained_paths(xml_reader, container_path + '/' + text, path_list);
                        xml_reader.Skip();
                    }
                    else
                    {
                        xml_reader.Skip();
                    }
                }
            }
        }
    }
}
