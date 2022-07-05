using System;

namespace algorytm2
{
    public class Lesson
    {
        public int Start { get; set; } = 0;
        public int End { get; set; } = 0;
    }

    public class LessonInClass
    {
        public List<Lesson> Lessons { get; set; } = new List<Lesson>();
        public int CoutLessons { get; set; } = 0;

        public bool CheckCanAddLesson(Lesson lesson)
        {
            if (Lessons.Count == 0)
            {
                return false;
            }
            if (CheckIsFirstLesson(lesson))
            {
                return true;
            }
            else if (CheckIsLastLesson(lesson))
            {
                return true;
            }
            else if (checkIsBetweenLesson(lesson))
            {
                return true;
            }
            return false;
        }

        private bool checkIsBetweenLesson(Lesson lesson)
        {
            if(CoutLessons <= 1) 
            {
                return false;
            }

            for(int i = 0; i < Lessons.Count-1 ; i++)
            {
                Lesson previousLesson = Lessons[i];
                Lesson nextLesson = Lessons[i+1];
                if(previousLesson.End < lesson.Start && nextLesson.Start > lesson.End)
                {
                    Lessons.Insert(i, lesson);
                    return true;
                }
            }
            return false;
        }

        private bool CheckIsLastLesson(Lesson lesson)
        {
            if (lesson.Start > Lessons[CoutLessons-1].End)
            {
                Lessons.Add(lesson);
                return true;
            }
            return false;
        }

        private bool CheckIsFirstLesson(Lesson lesson)
        {
            if (lesson.End < Lessons[0].Start)
            {
                Lessons.Insert(0, lesson);
                return true;
            }
            return false;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        { 
            List<(int start, int end)> listAllLesson = new List<(int start, int end)>() { (30, 75), (0, 50), (60, 150) };
            List<(int start, int end)> listAllLesson2 = new List<(int start, int end)>() { (0, 75), (0, 35), (40, 90), (20, 70) };
            List<(int start, int end)> listAllLesson3 = new List<(int start, int end)>() { (0, 75), (0, 35), (40, 90), (20, 70), (37, 38), (36, 36) };
            Console.WriteLine(CountClassForLesson(listAllLesson));
            Console.WriteLine(CountClassForLesson(listAllLesson2));
            Console.WriteLine(CountClassForLesson(listAllLesson3));

        }

        public static int CountClassForLesson(List<(int start, int end)> lessons){
            List<LessonInClass> classes = new List<LessonInClass>();
            var listAllLesson = lessons.OrderBy(p => p.start).OrderBy(p => p.end).ToList();

            while (listAllLesson.Count > 0)
            {
                var isAddToList = false;
                var les = listAllLesson.First();
                Lesson lesson = new Lesson() { Start = les.start, End = les.end };

                foreach (var item in classes)
                {
                    if (item.CheckCanAddLesson(lesson))
                    {
                        item.CoutLessons++;
                        isAddToList = true;
                        break;
                    }
                }

                if (!isAddToList)
                {
                    LessonInClass newClass = new LessonInClass();
                    newClass.Lessons.Add(lesson);
                    newClass.CoutLessons++;
                    classes.Add(newClass);
                }
                listAllLesson.RemoveAt(0);
            }
            return classes.Count;
        }

    }
}
