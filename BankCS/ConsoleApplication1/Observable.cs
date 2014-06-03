using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
  public  class Observable
    {
        public virtual List<Observer> Observers {get; set;}


        public virtual void addObserver(Observer o)
        {
            Observers.Add(o);
        }

        public virtual void deleteObserver(Observer o){
            Observers.Remove(o);
        }

        public virtual void notifyObservers()
        {
            notifyObservers(null);
        }
        
        public virtual void notifyObservers(Object arg)
        {
            foreach (Observer obs in Observers)
            {
                obs.update(this, arg);
            }


        }

    }
}
