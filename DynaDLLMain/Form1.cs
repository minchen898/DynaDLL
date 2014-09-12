using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace DynaDLLMain
{
    public partial class Form1 : Form
    {
        //Dictionary<string, dynamic> calculators;
        Dictionary<string, Type> calculators;

        public Form1()
        {
            InitializeComponent();

            //calculators = new Dictionary<string, dynamic>();
            calculators = new Dictionary<string, Type>();

            List<string> dlls = new List<string>();

            string path = Path.GetFullPath(".\\Calculators");
            foreach (string dllfile in Directory.GetFiles(path, "*.dll"))
            {
                string name = Path.GetFileNameWithoutExtension(dllfile);

                Assembly asm = Assembly.LoadFrom(dllfile);
                //dynamic obj = asm.CreateInstance(name + ".Calculator");
                //calculators.Add(name, obj);
                calculators.Add(name, asm.GetType(name + ".Calculator"));

                dlls.Add(name);
            }

            lbDlls.DataSource = dlls;
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            //int r = calculators[(string)lbDlls.SelectedValue].Calculate(int.Parse(txtA.Text), int.Parse(txtB.Text));
            int r = (int)calculators[(string)lbDlls.SelectedValue].InvokeMember("SCalculate", BindingFlags.InvokeMethod,
                Type.DefaultBinder, "", new object[] { int.Parse(txtA.Text), int.Parse(txtB.Text) });
            MessageBox.Show(r.ToString());
        }
    }
}
