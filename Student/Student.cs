using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Microsoft.ServiceFabric.Actors.Client;
using Student.Interfaces;

namespace Student
{
    
    [StatePersistence(StatePersistence.Persisted)]
    internal class Student : Actor, IStudent
    {
        /// <summary>
        /// Initializes a new instance of Student
        /// </summary>
        /// <param name="actorService">The Microsoft.ServiceFabric.Actors.Runtime.ActorService that will host this actor instance.</param>
        /// <param name="actorId">The Microsoft.ServiceFabric.Actors.ActorId for this actor instance.</param>
        public Student(ActorService actorService, ActorId actorId)
            : base(actorService, actorId)
        {
        }

        public Task<string> Details()
        {
            return StateManager.GetStateAsync<string>("Name");
        }

        public Task Initilize(string name)
        {
            return this.StateManager.SetStateAsync("Name", name);
        }
        
        protected override Task OnActivateAsync()
        {
            ActorEventSource.Current.ActorMessage(this, "Actor activated.");
            return this.StateManager.TryAddStateAsync("count", 0);
        }

       
    }
}
