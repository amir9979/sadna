using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observer
{
  public  class Observable
    {
        List<Observer> Observers;


        public void addObserver(Observer o)
        {
            Observers.Add(o);
        }

        public void deleteObserver(Observer o){
            Observers.Remove(o);
        }

        void notifyObservers()
        {
            notifyObservers(null);
        }
        
        void notifyObservers(Object arg)
        {
            foreach (Observer obs in Observers)
            {
                obs.update(this, arg);
            }


        }

    }
}
