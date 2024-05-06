using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;
using System.Security;

namespace Soft.dev_lab3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonInput_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
            "Получить матрицу из файла? (Ответ \"нет\" означает ввод матрицы с клавиатуры.)",
            "Внимание",
            MessageBoxButtons.YesNoCancel,
            MessageBoxIcon.Information,
            MessageBoxDefaultButton.Button1,
            MessageBoxOptions.DefaultDesktopOnly);

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        dataGridView1.Rows.Clear();
                        int nColumns = 0;
                        foreach (var line in File.ReadLines(openFileDialog1.FileName))
                        {
                            nColumns++;
                        }
                        dataGridView1.ColumnCount = nColumns;
                        foreach (var line in File.ReadLines(openFileDialog1.FileName))
                        {
                            var array = line.Split();
                            dataGridView1.Rows.Add(array);
                        }
                    }
                    catch (SecurityException ex)
                    {
                        MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                        $"Details:\n\n{ex.StackTrace}");
                    }
                }
            }
            else if (result == System.Windows.Forms.DialogResult.No)
            {
                new Form2(this).Show(); // создаём дочернее окно
            }
        }

        private void buttonOutput_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(saveFileDialog1.FileName))
                {
                    try
                    {
                        for (int i = 0; i < dataGridView1.RowCount; i++)
                        {
                            for (int j = 0; j < dataGridView1.ColumnCount; j++)
                            {
                                sw.Write(dataGridView1.Rows[i].Cells[j].Value.ToString());
                                if (j < dataGridView1.ColumnCount - 1) sw.Write(" ");
                            }
                            sw.WriteLine();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            int size = dataGridView1.ColumnCount;
            dataGridView2.ColumnCount = size;
            dataGridView2.RowCount = 1;

            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                int maxValue = Convert.ToInt32(dataGridView1.Rows[0].Cells[i].Value);
                for (int j = 1; j < dataGridView1.RowCount - 1; j++)
                {
                    if (Convert.ToInt32(dataGridView1.Rows[j].Cells[i].Value) > maxValue)
                    {
                        maxValue = Convert.ToInt32(dataGridView1.Rows[j].Cells[i].Value);
                    }
                }
                dataGridView2.Rows[0].Cells[i].Value = maxValue;
            }
        }
    }
}
