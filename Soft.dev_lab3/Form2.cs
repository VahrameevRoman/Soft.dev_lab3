using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Soft.dev_lab3
{
    public partial class Form2 : Form
    {
        readonly Form1 form1;
        public Form2(Form1 owner)
        {
            form1 = owner;
            InitializeComponent();
        }

        private void buttonVisibleFormArray_Click(object sender, EventArgs e)
        {
            bool check = true;
            if (CheckForPositiveNumber(textBoxColumns.Text) == false)
            {
                check = false;
            }
            if (CheckForPositiveNumber(textBoxRows.Text) == false)
            {
                check = false;
            }
            if (check)
            {
                CreateArray();
            }
            else
            {
                MessageBox.Show(
                "Введите корректные значения переменных.",
                "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void CreateArray()
        {
            dataGridView3.Visible = true;
            dataGridView3.ColumnCount = Convert.ToInt32(textBoxColumns.Text);
            dataGridView3.RowCount = Convert.ToInt32(textBoxRows.Text);
            buttonCreateArray.Visible = true;
        }

        private bool CheckForPositiveNumber(string str)
        {
            double number;
            if (double.TryParse(str, out number) && (number > 0)) return true;
            return false;
        }

        private void buttonCreateArray_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
            "Создать матрицу с текущими введёнными данными?",
            "Внимание",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Information,
            MessageBoxDefaultButton.Button1,
            MessageBoxOptions.DefaultDesktopOnly);
            if (result == DialogResult.Yes) 
            {
                SetValueDGForm1();
            }
        }
        private void SetValueDGForm1()
        {
            CheckValueDG();
            form1.dataGridView1.Rows.Clear();
            form1.dataGridView1.Columns.Clear();
            form1.dataGridView1.ColumnCount = dataGridView3.ColumnCount;
            form1.dataGridView1.RowCount = dataGridView3.RowCount;
            for (int i = 0; i < form1.dataGridView1.ColumnCount; i++)
            {
                for (int j = 0; j < form1.dataGridView1.RowCount; j++)
                {
                    form1.dataGridView1.Rows[j].Cells[i].Value = dataGridView3.Rows[j].Cells[i].Value;
                }
            }
            this.Close();
        }

        private void CheckValueDG()
        {
            for (int i = 0; i < form1.dataGridView1.ColumnCount; i++)
            {
                for (int j = 0; j < form1.dataGridView1.RowCount; j++)
                {
                    if (CheckForPositiveNumber(dataGridView3.Rows[j].Cells[i].Value.ToString()) == false)
                    {
                        dataGridView3.Rows[j].Cells[i].Value = 0;
                    }
                }
            }
        }
    }
}
