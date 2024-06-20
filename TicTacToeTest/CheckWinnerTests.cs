using System.Reflection;
using TicTacToe;

namespace TicTacToeTest
{
    [TestClass]
    public class CheckWinnerTests
    {
        [TestMethod]
        public void CheckNoWinner()
        {
            var winner = MainWindow.CheckWinner(new int[,]
            {
                { 0, 0, 0 },
                { 0, 0, 0 },
                { 0, 0, 0 },
            });
            Assert.AreEqual(winner, 0);

            winner = MainWindow.CheckWinner(new int[,]
            {
                { 0, 2, 0 },
                { 0, 1, 1 },
                { 0, 0, 2 },
            });
            Assert.AreEqual(winner, 0);

            winner = MainWindow.CheckWinner(new int[,]
            {
                { 1, 2, 0 },
                { 2, 1, 1 },
                { 2, 1, 2 },
            });
            Assert.AreEqual(winner, 0);
        }

        [TestMethod]
        public void CheckHorizontalWinner()
        {
            var winner = MainWindow.CheckWinner(new int[,]
            {
                { 1, 1, 1 },
                { 2, 2, 0 },
                { 0, 0, 0 },
            });
            Assert.AreEqual(winner, 1);

            winner = MainWindow.CheckWinner(new int[,]
            {
                { 0, 1, 0 },
                { 2, 2, 2 },
                { 1, 0, 1 },
            });
            Assert.AreEqual(winner, 2);

            winner = MainWindow.CheckWinner(new int[,]
            {
                { 0, 0, 2 },
                { 0, 2, 0 },
                { 1, 1, 1 },
            });
            Assert.AreEqual(winner, 1);
        }

        [TestMethod]
        public void CheckVerticalWinner()
        {
            var winner = MainWindow.CheckWinner(new int[,]
            {
                { 1, 0, 0 },
                { 1, 2, 0 },
                { 1, 0, 2 },
            });
            Assert.AreEqual(winner, 1);

            winner = MainWindow.CheckWinner(new int[,]
            {
                { 0, 1, 0 },
                { 2, 1, 2 },
                { 0, 1, 0 },
            });
            Assert.AreEqual(winner, 1);

            winner = MainWindow.CheckWinner(new int[,]
            {
                { 0, 1, 2 },
                { 0, 1, 2 },
                { 1, 0, 2 },
            });
            Assert.AreEqual(winner, 2);
        }

        [TestMethod]
        public void CheckDiagonalWinner()
        {
            var winner = MainWindow.CheckWinner(new int[,]
            {
                { 1, 0, 0 },
                { 0, 1, 2 },
                { 0, 2, 1 },
            });
            Assert.AreEqual(winner, 1);

            winner = MainWindow.CheckWinner(new int[,]
            {
                { 0, 1, 2 },
                { 0, 2, 1 },
                { 2, 1, 0 },
            });
            Assert.AreEqual(winner, 2);
        }

        [TestMethod]
        public void CheckTie()
        {
            var winner = MainWindow.CheckWinner(new int[,]
            {
                { 1, 2, 2 },
                { 2, 1, 1 },
                { 1, 1, 2 },
            });
             Assert.AreEqual(winner, 3);
        }
    }
}