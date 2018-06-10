using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook :BaseGradeBook
    {
        public RankedGradeBook(string name, bool isWeighted) :base (name,  isWeighted)
        {
            Type = Enums.GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count>4)
            {
                int positionInClass = GetPositionInClass(averageGrade);
                if (positionInClass >= Students.Count * 0.8)
                {
                    return 'A';
                }
                else if (positionInClass >= Students.Count * 0.6)
                {
                    return 'B';
                }
                else if (positionInClass >= Students.Count * 0.4)
                {
                    return 'C';
                }
                else if (positionInClass >= Students.Count * 0.2)
                {
                    return 'D';
                }
                else
                {
                    return 'F';
                }
            }
            else
            {
                throw new InvalidOperationException("Ranked-grading requires a minimum of 5 students to work");
            }
        }

        private int GetPositionInClass(double averageGrade)
        {
            List<Student> SortedByGrade = Students.OrderBy(s => s.AverageGrade).ToList();
            return SortedByGrade.FindIndex(s => s.AverageGrade == averageGrade);
        }

        public override void CalculateStatistics()
        {
            if (base.Students.Count<5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }
            base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name)
        {
            if (base.Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }
            base.CalculateStudentStatistics(name);
        }
    }
}
