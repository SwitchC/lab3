using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Xml;


namespace lab3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetComboBox(comboBox1);
            comboBox1.Text = comboBox1.Items[0].ToString();
            SetComboBox(comboBox2);
            comboBox2.Text = comboBox2.Items[0].ToString();;
        }
        private List<Files> files = new List<Files>();
        private List<Files> FILES = new List<Files>();
        private class Files
        {
            public string Path { get; set; }
            public string Name { get; set; }
            public Files() { }
            public Files(string p,string n)
            {
                Path = p;
                Name = n;
            }
        }
        private static class State
        {
            public static string ComboBox3;
            public static string ComboBox4;
        }
        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            BeforeSelect(e);
        }
        private void Drives(dynamic treeView, string drive)
        {
            try
            {
                TreeNode d = new TreeNode {Text=drive };
                FillNode(d, drive);
                treeView.Nodes.Add(d);
            }
            catch (Exception ex) {}
        }
        private void FillNode(TreeNode driveNode, string path)
        {
            try
            {
                string[] dirs = Directory.GetDirectories(path);
                foreach (string dir in dirs)
                {
                    TreeNode dirNode = new TreeNode();
                    dirNode.Text = dir.Remove(0,dir.LastIndexOf("\\")+1);
                    driveNode.Nodes.Add(dirNode);
                }
            }
            catch (Exception ex) { }
        }
        private void treeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            BeforeSelect(e);
        }
        private void BeforeSelect(dynamic e )
        {
            e.Node.Nodes.Clear();
            string[] dirs;
            try
            {
                if (Directory.Exists(e.Node.FullPath))
                {
                    dirs = Directory.GetDirectories(e.Node.FullPath);
                    if (dirs.Length > 0)
                    {
                        for (int i = 0; i < dirs.Length; i++)
                        {
                            TreeNode dirNode = new TreeNode(new DirectoryInfo(dirs[i]).Name);
                            FillNode(dirNode, dirs[i]);
                            e.Node.Nodes.Add(dirNode);
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }
        public void SetComboBox(dynamic comboBox)
        {
            comboBox.Items.Clear();
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (var d in drives)
            {
                comboBox.Items.Add(d.Name);
            }
        }     
        private void comboBox1_Click(object sender, EventArgs e)
        {
            SetComboBox(comboBox1);
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            Drives(treeView1,comboBox1.Text);
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            treeView2.Nodes.Clear();
            Drives(treeView2, comboBox2.Text);
        }
        private void comboBox2_Click(object sender, EventArgs e)
        {
            SetComboBox(comboBox2);
        }
        private void treeView2_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            BeforeSelect(e);
        }

        private void treeView2_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            BeforeSelect(e);
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            comboBox3.Items.Clear();
            files.Clear();
            try
            {
                if ((Directory.GetFiles(e.Node.FullPath)).Length > 0)
                {
                    foreach (string f in Directory.GetFiles(e.Node.FullPath))
                    {
                        FileInfo file1 = new FileInfo(f);
                        if (comboBox3.Items.Contains(file1.Extension) == false)
                        {
                            comboBox3.Items.Add(file1.Extension);
                        }
                        Files file = new Files(f, f.Remove(0, f.LastIndexOf("\\") + 1));
                        files.Add(file);
                    }
                }
                List<Files> files1 = new List<Files>();
                foreach (var f in files)
                {
                    FileInfo t = new FileInfo(f.Path);
                    if (t.Extension == State.ComboBox3)
                    {
                        files1.Add(f);
                    }
                }
                listBox1.DataSource = files1;
                listBox1.DisplayMember = "Name";
                listBox1.ValueMember = "Path";
            }
            catch (Exception ex) 
            {
                string[] s = new string[] { "Немає доступу" };
                listBox1.DataSource =s;
                listBox1.DisplayMember = null;
                listBox1.ValueMember = null;
                comboBox3.Text = "";
                State.ComboBox3 = "";
            }
            
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            State.ComboBox3 = comboBox3.Text;
            listBox1.Text="";
            try
            {
                List<Files> files1 = new List<Files>();
                foreach (var f in files)
                {
                    FileInfo t = new FileInfo(f.Path);
                    if (t.Extension == State.ComboBox3)
                    {
                        files1.Add(f);
                    }
                }
                listBox1.DataSource = files1;
                listBox1.DisplayMember = "Name";
                listBox1.ValueMember = "Path";
            }
            catch (Exception ex)
            {
                string[] s = new string[] { "Немає доступу" };
                listBox1.DataSource = s;
                listBox1.DisplayMember = null;
                listBox1.ValueMember = null;
                comboBox3.Text = "";
                State.ComboBox3 = "";
            }
        }

        private void treeView2_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            comboBox4.Items.Clear();
            FILES.Clear();
            try
            {
                if ((Directory.GetFiles(e.Node.FullPath)).Length > 0)
                {
                    foreach (string f in Directory.GetFiles(e.Node.FullPath))
                    {
                        FileInfo file1 = new FileInfo(f);
                        if (comboBox4.Items.Contains(file1.Extension) == false)
                        {
                            comboBox4.Items.Add(file1.Extension);
                        }
                        Files file = new Files(f, f.Remove(0, f.LastIndexOf("\\") + 1));
                        FILES.Add(file);
                    }
                }
                List<Files> files1 = new List<Files>();
                foreach (var f in FILES)
                {
                    FileInfo t = new FileInfo(f.Path);
                    if (t.Extension == State.ComboBox4)
                    {
                        files1.Add(f);
                    }
                }
                listBox2.DataSource = files1;
                listBox2.DisplayMember = "Name";
                listBox2.ValueMember = "Path";
            }
            catch (Exception ex)
            {
                string[] s = new string[] { "Немає доступу" };
                listBox2.DataSource = s;
                listBox2.DisplayMember = null;
                listBox2.ValueMember = null;
                comboBox4.Text = "";
                State.ComboBox4 = "";
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            State.ComboBox4 = comboBox4.Text;
            listBox2.Text = "";
            try
            {
                List<Files> files1 = new List<Files>();
                foreach (var f in FILES)
                {
                    FileInfo t = new FileInfo(f.Path);
                    if (t.Extension == State.ComboBox4)
                    {
                        files1.Add(f);
                    }
                }
                listBox2.DataSource = files1;
                listBox2.DisplayMember = "Name";
                listBox2.ValueMember = "Path";
            }
            catch (Exception ex)
            {
                string[] s = new string[] { "Немає доступу" };
                listBox2.DataSource = s;
                listBox2.DisplayMember = null;
                listBox2.ValueMember = null;
                comboBox4.Text = "";
                State.ComboBox4 = "";
            }
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            FileInfo f = new FileInfo(listBox1.SelectedValue.ToString());
            if (f.Extension == ".html")
            {
                HTMLProcessing(listBox1.SelectedValue.ToString());
                return;
            }
            try
            {
                Process.Start(new ProcessStartInfo(listBox1.SelectedValue.ToString()) { UseShellExecute = true });
            }
            catch (Exception)
            {
            }
        }

        private void listBox2_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo(listBox2.SelectedValue.ToString()) { UseShellExecute = true });
            }
            catch (Exception)
            {
            }
        }

        private void HTMLProcessing(string path)
        {
         


        }
    }
}
