/*
 Copyright (C) 2008 Siarhei Novik (snovik@gmail.com)
  
 This file is part of QLNet Project http://qlnet.sourceforge.net/

 QLNet is free software: you can redistribute it and/or modify it
 under the terms of the QLNet license.  You should have received a
 copy of the license along with this program; if not, license is  
 available online at <http://qlnet.sourceforge.net/License.html>.
  
 QLNet is a based on QuantLib, a free-software/open-source library
 for financial quantitative analysts and developers - http://quantlib.org/
 The QuantLib license is available online at http://quantlib.org/license.shtml.
 
 This program is distributed in the hope that it will be useful, but WITHOUT
 ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS
 FOR A PARTICULAR PURPOSE.  See the license for more details.
*/
using System;
using System.Collections.Generic;
using System.Text;
using QLNet;

namespace QLNet
{
    //! Base class for event
    //! This class acts as a base class for the actual event implementations.
    public abstract class Event : IObservable
    {
        //! Event interface
        //! returns the date at which the event occurs
        public abstract Date date();

        //! returns true if an event has already occurred before a date
        public virtual bool hasOccurred(Date d) { return date() <= d; }
        public virtual bool hasOccurred(Date d, bool includeToday)
        {
            if (includeToday) return date() < d;
            else return date() <= d;
        }

        // observable interface
        public event Callback notifyObserversEvent;
        public void registerWith(Callback handler) { notifyObserversEvent += handler; }
        public void unregisterWith(Callback handler) { notifyObserversEvent -= handler; }
        protected void notifyObservers()
        {
            Callback handler = notifyObserversEvent;
            if (handler != null)
            {
                handler();
            }
        }


        public virtual void accept(IAcyclicVisitor v)
        {
            if (v != null)
                v.visit(this);
            else
                throw new ApplicationException("not an event visitor");
        }
    }
}
