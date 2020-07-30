using System;
using System.Collections.Generic;
using System.Linq;
using GradeBook.Enums;
namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name, bool isWeighted) : base(name, isWeighted)
        {
            Type = GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < 5)
            {
                throw new InvalidOperationException("Ranked-grading requires a minimum of 5 students to work.");
            }

            int percent = (int)Math.Round(Students.Count * .2, 0);

            List<double> studentGrades = Students
                .OrderByDescending(x => x.AverageGrade)
                .Select(x => x.AverageGrade)
                .ToList();

            switch (averageGrade)
            {
                case var g when g > studentGrades[percent]:
                    return 'A';

                case var g when g > studentGrades[(percent * 2)]:
                    return 'B';

                case var g when g > studentGrades[(percent * 3)]:
                    return 'C';

                case var g when g > studentGrades[(percent * 4)]:
                    return 'D';

                default:
                    break;
            }

            return 'F';
        }

        public override void CalculateStatistics()
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }

            base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name)
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }
            base.CalculateStudentStatistics(name);
        }
    }
}