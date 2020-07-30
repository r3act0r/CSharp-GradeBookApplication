using System;
using System.Collections.Generic;
using System.Linq;
using GradeBook.Enums;
namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name) : base(name)
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
    }
}
