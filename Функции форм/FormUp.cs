using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;
using System.Collections.Generic;



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

        public static void RightMouse(MainForm form, DataGridViewCellMouseEventArgs e, DataGridView dg)
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
        //Получение вычислительных параметров для дня
        public static void ListPar(List<LessonsTime> ldb, List<string> list)
        {
            foreach (var temp in ldb)
            {
                long longVarb = BitConverter.ToInt64(temp.TimeBeg, 0);
                DateTime dateTimeBeg = DateTime.FromBinary(longVarb);

                long longVare = BitConverter.ToInt64(temp.TimeEnd, 0);
                DateTime dateTimeEnd = DateTime.FromBinary(longVare);

                string pp = dateTimeBeg.TimeOfDay + " " + dateTimeEnd.TimeOfDay + " " + temp.Turn;
                list.Add(pp);

            }
            int res = 7 - ldb.Count;
            if (res != 0)
            {
                for (int i = 0; i < res; i++)
                {
                    list.Add(" ");
                }
            }

        }

        //Заполняем расписание
        public static void Time3(int count, List<LessonsTime> lt, Class sub, schoolContext db, List<LCE> list, MainForm form)
        {
            string day=null;
            for (int i = 0; i < count; i++)
            {

                //получаем новый список без дня который уже занят
                lt = lt.Where(x => x.DayOfWeek != day).ToList();

                foreach (var l in lt)
                {
                   
                        long em = sub.EmployeeSubject.Employees.IdEmployess;
                        long y = l.IdLt;
                        string cab = sub.Cabinet;
                        long cla = sub.NameClassesId;

                    if (!list.Exists(c => (c.LessonT == y && c.clas == cla)))
                    {
                        if (!list.Exists(c => (c.LessonT == y && c.Emp == em)))
                        {
                            if (!list.Exists(c => (c.LessonT == y && c.Cabinet == cab)))
                            {
                                Lesson les = new Lesson()
                                {
                                    LessonsTimeId = y,
                                    ClassesId = cla
                                };

                                db.Lessons.Add(les);
                                l.Lessons.Add(les);
                                sub.Lessons.Add(les);


                                Lesson.TruCathcSave(db, form, "Ошибка добавления данных!");

                                LCE tmpL = new LCE() { Emp = em, LessonT = y, clas = cla, Cabinet = cab };
                                list.Add(tmpL);
                                day = l.DayOfWeek;
                                break;
                            }
                            else { continue; }
                        }
                        else { continue; }
                    }
                    else { continue; }                   
                }

            }

        }

        //Деление на группы
        public static void Time2(int count, List<LessonsTime> lt, List<Class> sub, schoolContext db, List<LCE> list, MainForm form)
        {
            string day = null;
            long cla = sub[0].NameClassesId;
            //получаем список преподавателей и кабинетов
            List<string> cab = new List<string>();
            List<long> em = new List<long>();

            foreach(var t in sub)
            {
                cab.Add(t.Cabinet);
                em.Add(t.EmployeeSubject.Employees.IdEmployess);
            }

            //Получаем список свободного время для класса, преподавателя и кабинета

            List<LessonsTime> llt = new List<LessonsTime>();

            for (int i = 0; i < count; i++)
            {

                //получаем новый список без дня который уже занят
                lt = lt.Where(x => x.DayOfWeek != day).ToList();

                foreach (var l in lt)
                {

                    long y = l.IdLt;

                    if (!list.Exists(c => (c.LessonT == y && c.clas == cla)))
                    {
                        if (!list.Exists(c => (c.LessonT == y && em.Contains(c.Emp))))
                        {
                            if (!list.Exists(c => (c.LessonT == y && cab.Contains(c.Cabinet))))
                            {
                                llt.Add(l);
                                day = l.DayOfWeek;
                                break;
                            }
                            else { continue; }
                        }
                        
                    }
                }

            }

            foreach(var lessonT in llt)
            {
                foreach(var cl in sub)
                {
                    Lesson les = new Lesson()
                    {
                        LessonsTimeId = lessonT.IdLt,
                        ClassesId = cl.ClassesId
                    };

                    db.Lessons.Add(les);
                    lessonT.Lessons.Add(les);
                    cl.Lessons.Add(les);


                    Lesson.TruCathcSave(db, form, "Ошибка добавления данных!");

                    LCE tmpL = new LCE() { Emp = cl.EmployeeSubject.Employees.IdEmployess, LessonT = lessonT.IdLt, clas = cl.NameClassesId, Cabinet =  cl.Cabinet};
                    list.Add(tmpL);
                }
            }

        }

        //Деление на группы с признаком 2 подряд
        public static void Time1(int count, List<LessonsTime> lt, List<Class> sub, schoolContext db, List<LCE> list, MainForm form)
        {
            string day = null;
            long cla = sub[0].NameClassesId;

            //получаем список преподавателей и кабинетов
            List<string> cab = new List<string>();
            List<long> em = new List<long>();

            foreach (var t in sub)
            {
                cab.Add(t.Cabinet);
                em.Add(t.EmployeeSubject.Employees.IdEmployess);
            }

            //Получаем список свободного время для класса, преподавателя и кабинета


            List<LessonsTime> llt = new List<LessonsTime>();

            for (int i = 0; i < count/2; i++)
            {

                //получаем новый список без дня который уже занят
                lt = lt.Where(x => x.DayOfWeek != day).ToList();

                for (int j = 0; j < lt.Count; j++)
                {
                    
                    if (j + 1 < lt.Count)
                    {

                        long y = lt[j].IdLt;
                        long yy = lt[j + 1].IdLt;

                        if (lt[j].DayOfWeek == lt[j + 1].DayOfWeek)
                        {

                            if (!list.Exists(c => (c.LessonT == y && c.clas == cla))
                                && !list.Exists(c => (c.LessonT == yy && c.clas == cla)))
                            {

                                if (!list.Exists(c => (c.LessonT == y && em.Contains(c.Emp)))
                                    && !list.Exists(c => (c.LessonT == yy && em.Contains(c.Emp))))

                                {
                                    if (!list.Exists(c => (c.LessonT == y && cab.Contains(c.Cabinet)))
                                        && !list.Exists(c => (c.LessonT == yy && cab.Contains(c.Cabinet)))
                                        && lt[j].DayOfWeek == lt[j + 1].DayOfWeek)
                                    {
                                        llt.Add(lt[j]);
                                        llt.Add(lt[j + 1]);
                                        day = lt[j].DayOfWeek;
                                        break;
                                    }

                                    else { continue; }
                                }
                                
                            }
                            
                        }
                    }
                    
                }
            }

            foreach (var lessonT in llt)
            {
                foreach (var cl in sub)
                {
                    Lesson les = new Lesson()
                    {
                        LessonsTimeId = lessonT.IdLt,
                        ClassesId = cl.ClassesId
                    };

                    db.Lessons.Add(les);
                    lessonT.Lessons.Add(les);
                    cl.Lessons.Add(les);

                   Lesson.TruCathcSave(db, form, "Ошибка добавления данных!");

                    LCE tmpL = new LCE() { Emp = cl.EmployeeSubject.Employees.IdEmployess, LessonT = lessonT.IdLt, clas = cl.NameClassesId, Cabinet = cl.Cabinet };
                    list.Add(tmpL);
                }
            }

        }

    }
}
