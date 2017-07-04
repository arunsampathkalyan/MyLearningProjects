using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmSamples.CustomClass
{

    public class Voter
    {
        public Voter()
        {
            ID = new Guid();
        }

        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime DOB { get; set; }
        public string Address { get; set; }
        public Guid ID { get; }
    }
    public class Voters : CollectionBase
    {
        public Voters()
        {

        }

        public Voter this[int index]
        {
            get { return (Voter)this.List[index]; }
            set { this.List[index] = value; }
        }

        public int IndexOf(Voter voterItem)
        {
            if (voterItem != null)
                return base.List.IndexOf(voterItem);
            return -1;
        }

        public int Add(Voter voterItem)
        {
            if (voterItem != null && voterItem.Age >= 18)
                return this.List.Add(voterItem);
            return -1;
        }


        public void Remove(Voter voterItem)
        {
            this.InnerList.Remove(voterItem);
        }


        public void AddRange(Voters voterItems)
        {
            this.InnerList.AddRange(voterItems);
        }

        public void Insert(int index, Voter voterItem)
        {
            if (index <= List.Count && voterItem != null && voterItem.Age >= 18)
                this.List.Insert(index, voterItem);
        }

        public bool Contains(Voter voterItem)
        {
            return this.List.Contains(voterItem);
        }
    }
}
