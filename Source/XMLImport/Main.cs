using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace XMLImport
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void btImport_Click(object sender, EventArgs e)
        {
            if (System.IO.Directory.Exists(tbPath.Text))
            {
                string[] xmls = Directory.GetFiles(tbPath.Text, "*.xml");
                foreach (string sxml in xmls)
                {
                    ImportFile(sxml);
                }
            }
            else
            {
                MessageBox.Show("目录不存在！");
            }
        }

        private bool ImportFile(string FileName)
        {
            if (File.Exists(FileName))
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(FileName);
                XmlNode xn = xmldoc.SelectSingleNode("PEOPLE");
                foreach (XmlNode xn1 in xn.ChildNodes)
                {
                    // 将节点转换为元素，便于得到节点的属性值
                    XmlElement xe = (XmlElement)xn1;
                    string PERSONID = xe.GetAttribute("PERSONID");
                    string NAME = xe.GetElementsByTagName("NAME").Item(0).InnerText;
                    string ADDRESS = xe.GetElementsByTagName("ADDRESS").Item(0).InnerText;
                    string TEL = xe.GetElementsByTagName("TEL").Item(0).InnerText;
                    string EMAIL = xe.GetElementsByTagName("EMAIL").Item(0).InnerText;
                    MessageBox.Show(string.Format("PERSONID:{0} NAME:{1} ADDRESS:{2} TEL:{3} EMAIL:{4}", PERSONID, NAME, ADDRESS, TEL, EMAIL));
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        private void btSelectPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                tbPath.Text = fbd.SelectedPath;
            }
        }
    }
}
