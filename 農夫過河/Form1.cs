using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 農夫過河
{
    
    public partial class Form1 : Form
    {
        private Data data;
        private ListBox ListBox;
        public Form1()
        {
            
            InitializeComponent();
            SetListMode();
            Intialize();
            
        }
        private void Intialize()
        {
            InitializeData();
            ChangeData();
            IntializeForm();
        }
        private void InitializeData()
        {
            data = new Data();
        }
        private void IntializeForm()
        {
            ChangeButtonState(false);
            button1.Text = "-->";
            ListBox = listBox1;
            ListBox.SelectedIndex = 0;
        }
        private void SetListMode()
        {
            listBox1.SelectionMode = SelectionMode.One;
            listBox2.SelectionMode = SelectionMode.One;
        }
        private void ChangeData()
        {
            listBox1.DataSource = null;
            listBox2.DataSource = null;
            listBox1.DataSource = data.list_Left;
            listBox2.DataSource = data.list_Right;
        }
        private void ChangeButtonState(bool Enable)
        {
            button2.Enabled = Enable;
            button4.Enabled = Enable;
        }
        /// <summary>
        /// 更新ListBox的hightlight狀態，並且顯示在目前移動的項目上
        /// </summary>
        /// <param name="item"></param>
        private void RefreshListBox (string item)
        {
            ListBox.SelectedIndex = -1;
            if (ListBox == listBox1)
            {
                ListBox = listBox2;
                data.ChangeDirection(false);
                button1.Text = "<--";
            }
            else
            {
                ListBox = listBox1;
                data.ChangeDirection(true);
                button1.Text = "-->";
            }
            ListBox.SelectedItem = item;
        }
        /// <summary>
        /// 用箭頭提醒使用者目前移動的方向
        /// </summary>
        /// <param name="Select"></param>
        /// <param name="UnSelect"></param>
        private void ListBoxMouseDown(ListBox Select, ListBox UnSelect)
        {
            button1.Text = (Select == listBox1 ? "-->" : "<--");
            if (Select.SelectedItem != null)
            {
                UnSelect.SelectedIndex = -1;
                ListBox = Select;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string item = (string)ListBox.SelectedItem;
            // 用意在於之後如果item要做修改，只要改變數右邊的判斷式
            if(data.Move(item))
            {
                ChangeData();
                if (data.Check() == "Fail")
                {
                    MessageBox.Show(data.Check());
                    Intialize();
                }
                else if (data.Check() == "Pass")
                {
                    ChangeButtonState(true);
                    RefreshListBox(item);
                    data.AddBackData();
                }
                else if (data.Check() == "Finish")
                {
                    if (MessageBox.Show($"{data.Check()}{Environment.NewLine}Reset or not", "Confirm Message", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    // https://www.cnblogs.com/rainman/archive/2013/06/03/3116283.html
                    {
                        Intialize();
                    }
                }
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            // Back
            if (data.Back())
            {
                ChangeData();
                listBox1.SelectedIndex = -1;
                listBox2.SelectedIndex = -1;

                if (data.list_Right.Count == 0)
                    // 代表回到最初的狀態
                {
                    ChangeButtonState(false);
                    listBox1.SelectedIndex = 0;
                    button1.Text = "-->";
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            // Reset
            InitializeData();
            ChangeData();
        }
        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            data.ChangeDirection(true);
            ListBoxMouseDown(listBox1, listBox2);
        }
        private void listBox2_MouseDown(object sender, MouseEventArgs e)
        {
            data.ChangeDirection(false);
            ListBoxMouseDown(listBox2, listBox1);
        }
    }
}
