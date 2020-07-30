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

            int x = (int)Math.Round(Students.Count * .2, 0);

            List<double> studentGrades = Students
                .OrderByDescending(x => x.AverageGrade)
                .Select(x => x.AverageGrade)
                .ToList();

            switch (averageGrade)
            {
                case var g when g > studentGrades[x]:
                    return 'A';

                case var g when g > studentGrades[x * 2] && g < studentGrades[x]:
                    return 'B';

                case var g when g > studentGrades[x * 3] && g < studentGrades[x * 2]:
                    return 'C';

                case var g when g > studentGrades[x * 4] && g < studentGrades[x * 3]:
                    return 'D';

                default:
                    break;
            }

            return 'F';
        }
    }
}
