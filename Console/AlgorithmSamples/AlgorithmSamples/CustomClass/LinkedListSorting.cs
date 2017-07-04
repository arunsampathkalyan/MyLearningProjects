using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmSamples.CustomClass
{
    //https://channel9.msdn.com/Forums/TechOff/A-simple-and-pure-C-implementation-of-the-good-old-linked-list
    public class LList
    {
        public class LNode
        {
            public object content;
            public LNode next;
        }

        private LNode head;
        private LNode current;
        private int size;

        public int Count
        { get { return size; } }

        public void Add(object nodeContent)
        {
            size++;
            var node = new LNode()
            {
                content = nodeContent
            };


            if (head == null)
            {
                head = node;
            }
            else
            {
                current.next = node;
            }
            current = node;
        }

        public void ListNodes()
        {
            LNode tempNode = head;
            while (tempNode != null)
            {
                Console.WriteLine(tempNode.content);
                tempNode = tempNode.next;
            }
        }

        public LNode RetrieveNode(int position)
        {
            LNode tempNode = head;
            int count = 0;
            var node = RetriveNodeUsingRecursive(head, count, position);
            while (tempNode != null)
            {
                if (count == position - 1)
                {
                    return tempNode;
                }
                count++;
                tempNode = tempNode.next;
            }
            return null;
        }


        public LNode RetriveNodeUsingRecursive(LNode currNode, int currPos, int nthItem)
        {
            if (currPos == nthItem - 1)
                return currNode;
            else
                return RetriveNodeUsingRecursive(currNode.next, ++currPos, nthItem);

            /*
             * What is the difference between ++i and i++?

             * ++i will increment the value of i, and then return the incremented value.
             * ++i is known as postfix.
             i = 1;
             j = ++i;
             (i is 2, j is 2)

             i++ will increment the value of i, but return the original value that i held before being incremented.
             i++ is known as postfix.
             i = 1;
             j = i++;
             (i is 2, j is 1)
             */
        }
        public bool Delete(int position)
        {
            LNode curNode = head;
            LNode nextNode = head;
            int count = 0;

            if (position == 1)
            {
                if (position == size)
                {
                    head = null;
                    current = null;
                    size--;
                    return true;
                }
                else if (position > size)
                {
                    return false;
                }
                else
                {
                    head = head.next;
                    size--;
                    return true;
                }
            }
            if (position > 1 && position <= size)
            {
                while (nextNode != null)
                {
                    if (count == position - 1)
                    {
                        curNode.next = nextNode.next;
                        size--;
                        return true;
                    }
                    count++;
                    curNode = nextNode;
                    nextNode = nextNode.next;
                }
            }
            return false;
        }


    }
}
