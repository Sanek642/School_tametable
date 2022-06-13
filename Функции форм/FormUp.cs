using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_tametable
{
    public static class FormUp
    {
        //Информационное сообщение пользователю
        public static void MessegeOk(string error)
        {
            MessageBox.Show(
            error, "Ошибка!",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information,
            MessageBoxDefaultButton.Button1,
            MessageBoxOptions.DefaultDesktopOnly);

        }

        //Сообщение выбора для пользователя
        public static DialogResult MessegeYesNo(string question)
        {
            return MessageBox.Show(
                   question, "Изменения информации",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);

        }

        //Получение текущей строки датагрид
        public static long CurIndex(DataGridView dg)
        {
            if (dg.Rows.Count > 0)
                return (long)dg.CurrentRow.Cells[0].Value;
            else throw new Exception("Таблица пуста!");
        }

        public static void RightMouse(MainForm form,DataGridViewCellMouseEventArgs e, DataGridView dg)
        {
            if (e.Button == MouseButtons.Right)
            {
                try
                {
                    dg.ClearSelection();
                    dg[e.ColumnIndex, e.RowIndex].Selected = true;
                }
                catch
                {
                    form.TopMost = true;
                    FormUp.MessegeOk("Указатель мыши должнен находиться в границах таблицы!");
                }
            }
        }
        //Получение значения из ComboBox
        //public static string CurIndexCB(ComboBox cb)
        //{
       //    return cb.SelectedValue.ToString();
       // }

    }
}
