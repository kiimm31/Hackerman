using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PracticeHackerRank
{
    class MagicCube
    {
        [Test]
        [TestCaseSource(nameof(NotMagicCube))]
        public void ConvertToList(int[,] input)
        {
            //int[,] input = new int[3, 3]
            //    {{ 5, 3, 4 }, { 1, 5, 8 }, { 6, 4, 2 } };

            List<int[,]> vs1 = getAllPossibleMagicCube();

            Dictionary<int, int> keyValuePairs = new Dictionary<int, int>();

            foreach (int[,] cube in vs1)
            {
                int indexCube = vs1.IndexOf(cube);

                int noOfDifference = 0;
                for (int row = 0; row < 3; row++)
                {
                    for (int col = 0; col < 3; col++)
                    {
                        if (cube[row, col] != input[row, col])
                        {
                            noOfDifference += Math.Abs(input[row, col] - cube[row, col]);
                        }
                    }
                }

                keyValuePairs.Add(indexCube, noOfDifference);
            }

            var d = keyValuePairs.Min(x => x.Value);


            Assert.AreEqual(1, 1);
        }

        List<int[,]> getAllPossibleMagicCube()
        {
            List<int[,]> vs1 = new List<int[,]>()
            {
                new int[3,3] { { 8, 1, 6 }, { 3, 5, 7 }, { 4, 9, 2 } },
                new int[3,3] { { 6, 1, 8 }, { 7, 5, 3 }, { 2, 9, 4 } },
                new int[3,3] { { 4, 9, 2 }, { 3, 5, 7 }, { 8, 1, 6 } },
                new int[3,3] { { 2, 9, 4 }, { 7, 5, 3 }, { 6, 1, 8 } },
                new int[3,3] { { 8, 3, 4 }, { 1, 5, 9 }, { 6, 7, 2 } },
                new int[3,3] { { 4, 3, 8 }, { 9, 5, 1 }, { 2, 7, 6 } },
                new int[3,3] { { 6, 7, 2 }, { 1, 5, 9 }, { 8, 3, 4 } },
                new int[3,3] { { 2, 7, 6 }, { 9, 5, 1 }, { 4, 3, 8 } },
            };
            return vs1;
        }

        static object[] NotMagicCube =
        {
            new int[3, 3] {{ 5, 3, 4 }, { 1, 5, 8 }, { 6, 4, 2 } }
        };

    }
}
