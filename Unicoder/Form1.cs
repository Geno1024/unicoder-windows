using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Unicoder
{
    public partial class Form1 : Form
    {
        bool[] isEditing = { false, false };
        
        public Form1()
        {
            InitializeComponent();
            // UCD ucd = new UCD();
            new GenosBox.Networking().DownloadFileWithForm("http://www.baidu.com", "p",  () => { } , () => { });
        }
        
        private void Text_TextChanged(object sender, EventArgs e)
        {
            try
            {
                isEditing[0] = true;
                if (isEditing[0] && !isEditing[1])
                {
                    string unicode_values = "";
                    foreach (char item in text.Text)
                    {
                        unicode_values += Convert.ToString(item, 16) + " ";
                    }
                    unicode.Text = unicode_values;
                }
                isEditing[0] = false;
            }
            catch (Exception ex)
            {
                unicode.Text = ex.Message;
            }
        }

        private void Unicode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                isEditing[1] = true;
                if (!isEditing[0] && isEditing[1])
                {
                    string text_values = "";
                    string[] unicode_values = unicode.Text.Split(' ');
                    foreach (string item in unicode_values)
                    {
                        if (!item.Equals(""))
                            text_values += Convert.ToChar(Convert.ToInt32(item, 16));
                    }
                    text.Text = text_values;
                }
                isEditing[1] = false;
            }
            catch (Exception ex)
            {
                text.Text = ex.Message;
            }
        }

        public void OnDownloa()
        { }
    }
}
