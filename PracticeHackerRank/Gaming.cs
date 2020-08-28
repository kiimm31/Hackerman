using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PracticeHackerRank
{
    class Gaming
    {
        #region AliceRank
        [Test]
        [TestCaseSource(nameof(Rankings))]
        public void alice(int[] scores, int[] alice)
        {
            List<int> returnList = new List<int>();

            List<int> ranks = scores.ToList().Distinct().OrderByDescending(x => x).ToList();

            Dictionary<int, int> rankDic = new Dictionary<int, int>();

            foreach (int aliceScore in alice.ToList())
            {
                if (rankDic.ContainsKey(aliceScore))
                {
                    returnList.Add(rankDic[aliceScore]);
                }
                else
                {
                    List<int> TempList = new List<int>();

                    TempList.AddRange(ranks);

                    TempList.Add(aliceScore);

                    int myRank = TempList.Distinct().OrderByDescending(x => x).ToList().IndexOf(aliceScore) + 1;

                    returnList.Add(myRank);

                    rankDic.Add(aliceScore, myRank);
                }
            }

            Assert.IsNotNull(returnList);
        }

        private int GetRank(int[] score, int v)
        {
            List<int> ranking = score.ToList();

            ranking.Add(v);

            ranking = ranking.OrderByDescending(x => x).ToList();

            int previousScore = 0;

            int rank = 1;
            foreach (int player in ranking)
            {
                if (player < previousScore)
                {
                    rank++;
                }
                if (player == v)
                {
                    return rank;
                }

                previousScore = player;

            }
            return 1;
        }

        static object[] Rankings =
        {
            new object[] { new int[] { 100, 100, 50, 40, 40, 20, 10 }, new int[] { 5, 25, 50, 120 } }
        };
        #endregion

        #region CardGame
        [Test]
        [TestCase(1, 3, 5, 7, 9, ExpectedResult = "BOB")]
        [TestCase(7, 4, 6, 5, 8, ExpectedResult = "ANDY")]
        public string gamingArray(params int[] arr1)
        {
            List<int> arr = arr1.ToList();

            char turn = 'B';

            var i = arr.OrderBy(x => x);

            if (arr.OrderBy(x => x).SequenceEqual(arr))
            {
                // all is inorder, dont need to play already know who will win depending on even odd

                int evenOdd = arr.Count() % 2;

                if (evenOdd == 1)
                {
                    return "BOB";
                }
                else
                {
                    return "ANDY";
                }
            }

            while (arr.Any())
            {
                int indexLargest = arr.IndexOf(arr.Max());

                int countLeft = arr.Count() - indexLargest;

                arr.RemoveRange(indexLargest, countLeft);

                turn = switchTurn(turn);
            }


            if (turn == 'B')
            {
                return "ANDY";
            }
            else
            {
                return "BOB";
            }
        }

        private char switchTurn(char turn)
        {
            if (turn == 'B')
            {
                return 'A';
            }
            else
            {
                return 'B';
            }
        }
        #endregion




    }
}
