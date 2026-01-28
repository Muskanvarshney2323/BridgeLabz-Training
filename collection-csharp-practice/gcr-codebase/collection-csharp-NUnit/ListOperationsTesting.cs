using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CollectionNUnit.Basic
{
    /// <summary>
    /// ListManager Class: Implements list operations
    /// </summary>
    public class ListManager
    {
        public void AddElement(List<int> list, int element)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list), "List cannot be null");
            }

            list.Add(element);
        }

        public bool RemoveElement(List<int> list, int element)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list), "List cannot be null");
            }

            return list.Remove(element);
        }

        public int GetSize(List<int> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list), "List cannot be null");
            }

            return list.Count;
        }

        public bool Contains(List<int> list, int element)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list), "List cannot be null");
            }

            return list.Contains(element);
        }

        public void ClearList(List<int> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list), "List cannot be null");
            }

            list.Clear();
        }

        public int GetFirst(List<int> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list), "List cannot be null");
            }

            if (list.Count == 0)
            {
                throw new InvalidOperationException("List is empty");
            }

            return list[0];
        }

        public int GetLast(List<int> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list), "List cannot be null");
            }

            if (list.Count == 0)
            {
                throw new InvalidOperationException("List is empty");
            }

            return list[list.Count - 1];
        }
    }

    /// <summary>
    /// ListOperationsTesting: NUnit test cases for ListManager class
    /// Tests list operations like add, remove, and size verification
    /// </summary>
    [TestFixture]
    public class ListOperationsTesting
    {
        private ListManager listManager;
        private List<int> testList;

        [SetUp]
        public void SetUp()
        {
            listManager = new ListManager();
            testList = new List<int>();
        }

        #region Add Element Tests

        [Test]
        public void AddElement_ToEmptyList_ElementIsAdded()
        {
            // Arrange & Act
            listManager.AddElement(testList, 5);

            // Assert
            Assert.AreEqual(1, testList.Count);
            Assert.AreEqual(5, testList[0]);
        }

        [Test]
        public void AddElement_MultipleElements_AllElementsAdded()
        {
            // Arrange & Act
            listManager.AddElement(testList, 1);
            listManager.AddElement(testList, 2);
            listManager.AddElement(testList, 3);

            // Assert
            Assert.AreEqual(3, testList.Count);
            Assert.Contains(1, testList);
            Assert.Contains(2, testList);
            Assert.Contains(3, testList);
        }

        [Test]
        public void AddElement_DuplicateElements_BothAdded()
        {
            // Arrange & Act
            listManager.AddElement(testList, 5);
            listManager.AddElement(testList, 5);

            // Assert
            Assert.AreEqual(2, testList.Count);
            Assert.AreEqual(5, testList[0]);
            Assert.AreEqual(5, testList[1]);
        }

        [Test]
        public void AddElement_NegativeNumbers_ElementsAdded()
        {
            // Arrange & Act
            listManager.AddElement(testList, -10);
            listManager.AddElement(testList, -5);

            // Assert
            Assert.AreEqual(2, testList.Count);
            Assert.Contains(-10, testList);
            Assert.Contains(-5, testList);
        }

        [Test]
        public void AddElement_Zero_ElementIsAdded()
        {
            // Arrange & Act
            listManager.AddElement(testList, 0);

            // Assert
            Assert.AreEqual(1, testList.Count);
            Assert.AreEqual(0, testList[0]);
        }

        [Test]
        public void AddElement_ToNullList_ThrowsArgumentNullException()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>(() => listManager.AddElement(null, 5));
        }

        #endregion

        #region Remove Element Tests

        [Test]
        public void RemoveElement_ExistingElement_ElementRemoved()
        {
            // Arrange
            testList.Add(5);
            testList.Add(10);
            testList.Add(15);

            // Act
            bool removed = listManager.RemoveElement(testList, 10);

            // Assert
            Assert.IsTrue(removed);
            Assert.AreEqual(2, testList.Count);
            Assert.IsFalse(testList.Contains(10));
        }

        [Test]
        public void RemoveElement_NonExistingElement_ReturnsFalse()
        {
            // Arrange
            testList.Add(5);
            testList.Add(10);

            // Act
            bool removed = listManager.RemoveElement(testList, 20);

            // Assert
            Assert.IsFalse(removed);
            Assert.AreEqual(2, testList.Count);
        }

        [Test]
        public void RemoveElement_FromEmptyList_ReturnsFalse()
        {
            // Act
            bool removed = listManager.RemoveElement(testList, 5);

            // Assert
            Assert.IsFalse(removed);
            Assert.AreEqual(0, testList.Count);
        }

        [Test]
        public void RemoveElement_FirstElement_CorrectlyRemoved()
        {
            // Arrange
            testList.Add(1);
            testList.Add(2);
            testList.Add(3);

            // Act
            bool removed = listManager.RemoveElement(testList, 1);

            // Assert
            Assert.IsTrue(removed);
            Assert.AreEqual(2, testList[0]);
        }

        [Test]
        public void RemoveElement_LastElement_CorrectlyRemoved()
        {
            // Arrange
            testList.Add(1);
            testList.Add(2);
            testList.Add(3);

            // Act
            bool removed = listManager.RemoveElement(testList, 3);

            // Assert
            Assert.IsTrue(removed);
            Assert.AreEqual(2, testList[testList.Count - 1]);
        }

        [Test]
        public void RemoveElement_DuplicateElements_OnlyFirstRemoved()
        {
            // Arrange
            testList.Add(5);
            testList.Add(5);
            testList.Add(5);

            // Act
            bool removed = listManager.RemoveElement(testList, 5);

            // Assert
            Assert.IsTrue(removed);
            Assert.AreEqual(2, testList.Count);
        }

        [Test]
        public void RemoveElement_FromNullList_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => listManager.RemoveElement(null, 5));
        }

        #endregion

        #region Get Size Tests

        [Test]
        public void GetSize_EmptyList_ReturnsZero()
        {
            // Act
            int size = listManager.GetSize(testList);

            // Assert
            Assert.AreEqual(0, size);
        }

        [Test]
        public void GetSize_SingleElement_ReturnsOne()
        {
            // Arrange
            testList.Add(5);

            // Act
            int size = listManager.GetSize(testList);

            // Assert
            Assert.AreEqual(1, size);
        }

        [Test]
        public void GetSize_MultipleElements_ReturnsCorrectCount()
        {
            // Arrange
            testList.Add(1);
            testList.Add(2);
            testList.Add(3);
            testList.Add(4);
            testList.Add(5);

            // Act
            int size = listManager.GetSize(testList);

            // Assert
            Assert.AreEqual(5, size);
        }

        [Test]
        public void GetSize_AfterAddAndRemove_ReturnsCorrectSize()
        {
            // Arrange
            listManager.AddElement(testList, 1);
            listManager.AddElement(testList, 2);
            listManager.AddElement(testList, 3);

            // Act
            listManager.RemoveElement(testList, 2);
            int size = listManager.GetSize(testList);

            // Assert
            Assert.AreEqual(2, size);
        }

        [Test]
        public void GetSize_NullList_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => listManager.GetSize(null));
        }

        #endregion

        #region Combined Operation Tests

        [Test]
        public void CombinedOperations_AddRemoveAndSize_AllCorrect()
        {
            // Arrange & Act
            listManager.AddElement(testList, 10);
            Assert.AreEqual(1, listManager.GetSize(testList));

            listManager.AddElement(testList, 20);
            listManager.AddElement(testList, 30);
            Assert.AreEqual(3, listManager.GetSize(testList));

            listManager.RemoveElement(testList, 20);
            // Assert
            Assert.AreEqual(2, listManager.GetSize(testList));
            Assert.Contains(10, testList);
            Assert.Contains(30, testList);
            Assert.IsFalse(testList.Contains(20));
        }

        [Test]
        public void CombinedOperations_MultipleOperations_ListConsistent()
        {
            // Arrange & Act
            for (int i = 1; i <= 5; i++)
            {
                listManager.AddElement(testList, i * 10);
            }

            Assert.AreEqual(5, listManager.GetSize(testList));

            listManager.RemoveElement(testList, 30);
            // Assert
            Assert.AreEqual(4, listManager.GetSize(testList));
        }

        #endregion

        #region Helper Method Tests

        [Test]
        public void Contains_ExistingElement_ReturnsTrue()
        {
            // Arrange
            testList.Add(5);
            testList.Add(10);

            // Act
            bool contains = listManager.Contains(testList, 5);

            // Assert
            Assert.IsTrue(contains);
        }

        [Test]
        public void Contains_NonExistingElement_ReturnsFalse()
        {
            // Arrange
            testList.Add(5);

            // Act
            bool contains = listManager.Contains(testList, 10);

            // Assert
            Assert.IsFalse(contains);
        }

        [Test]
        public void ClearList_ListWithElements_BecomesEmpty()
        {
            // Arrange
            testList.Add(1);
            testList.Add(2);
            testList.Add(3);

            // Act
            listManager.ClearList(testList);

            // Assert
            Assert.AreEqual(0, testList.Count);
        }

        [Test]
        public void GetFirst_ListWithElements_ReturnsFirstElement()
        {
            // Arrange
            testList.Add(10);
            testList.Add(20);
            testList.Add(30);

            // Act
            int first = listManager.GetFirst(testList);

            // Assert
            Assert.AreEqual(10, first);
        }

        [Test]
        public void GetLast_ListWithElements_ReturnsLastElement()
        {
            // Arrange
            testList.Add(10);
            testList.Add(20);
            testList.Add(30);

            // Act
            int last = listManager.GetLast(testList);

            // Assert
            Assert.AreEqual(30, last);
        }

        [Test]
        public void GetFirst_EmptyList_ThrowsInvalidOperationException()
        {
            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => listManager.GetFirst(testList));
        }

        [Test]
        public void GetLast_EmptyList_ThrowsInvalidOperationException()
        {
            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => listManager.GetLast(testList));
        }

        #endregion

        [TearDown]
        public void TearDown()
        {
            testList?.Clear();
            testList = null;
            listManager = null;
        }
    }
}
