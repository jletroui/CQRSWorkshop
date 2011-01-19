using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Commanding.ServiceModel;
using Ncqrs.Eventing.ServiceModel.Bus;
using Ncqrs.Eventing.Storage;
using Ncqrs;
using CommandHandlers;
using Ncqrs.Eventing;
using Ncqrs.Config;
using Ncqrs.Commanding;
using NUnit.Framework;
using System.Reflection;
using Domain;

namespace Tests
{
    public abstract class DomainTestCaseBase : INow
    {
        private Dictionary<string, Guid> ids = new Dictionary<string, Guid>();
        private DateTime now = DateTime.Now;

        protected Guid Id(string label)
        {
            if (!ids.ContainsKey(label))
            {
                ids.Add(label, Guid.NewGuid());
            }
            return ids[label];
        }

        public DateTime Now()
        {
            return now;
        }

        protected DomainOutcomeBuilder Given(params ICommand[] commands)
        {
            return new DomainOutcomeBuilder(commands, this);
        }

        class TestEventBus : IEventBus
        {
            public IList<IEvent> PublishedEvents = new List<IEvent>();

            public void Publish(IEnumerable<IEvent> eventMessages)
            {
                foreach (var e in eventMessages)
                {
                    PublishedEvents.Add(e);
                }
            }

            public void Publish(IEvent eventMessage)
            {
                Publish(new IEvent[] { eventMessage });
            }
        }

        public class DomainOutcomeBuilder : IEnvironmentConfiguration
        {
            private readonly TestEventBus bus = new TestEventBus();
            private readonly CommandService cmdService = new CommandService();
            private readonly InMemoryEventStore evtStore = new InMemoryEventStore();
            private readonly Dictionary<Type, object> container = new Dictionary<Type, object>();
            private readonly IEnumerable<ICommand> commands;
            private readonly INow now;
            private ICommand when;

            public DomainOutcomeBuilder(IEnumerable<ICommand> cmds, INow now)
            {
                this.now = now;

                cmdService.RegisterExecutor(new AddNewMediaCommandHandler());
                cmdService.RegisterExecutor(new RegisterNewCustomerCommandHandler());
                cmdService.RegisterExecutor(new CustomerRentMediaCommandHandler());
                cmdService.RegisterExecutor(new CustomerReturnMediaCommandHandler());

                container.Add(typeof(IEventBus), bus);
                container.Add(typeof(ICommandService), cmdService);
                container.Add(typeof(IEventStore), evtStore);
                container.Add(typeof(INow), now);

                NcqrsEnvironment.Configure(this); 
                
                commands = cmds;
            }

            public DomainOutcomeBuilder When(ICommand command)
            {
                when = command;
                return this;
            }

            public void ThenExpect(params IEvent[] events)
            {
                try
                {
                    foreach (var cmd in commands)
                    {
                        cmdService.Execute(cmd);
                    }

                    bus.PublishedEvents.Clear();

                    cmdService.Execute(when);

                    for (int i = 0; i < events.Length && i < bus.PublishedEvents.Count; i++)
                    {
                        // Test type
                        Assert.AreEqual(events[i].GetType(), bus.PublishedEvents[i].GetType(), BuildEventExpectedActual(events));

                        // Test content
                        foreach (var field in events[i].GetType().GetFields())
                        {
                            Assert.AreEqual(
                                field.GetValue(events[i]),
                                field.GetValue(bus.PublishedEvents[i]),
                                "Was expecting '{0}' for property '{1}' but got '{2}' in the {3}th event of type '{4}'",
                                field.GetValue(events[i]),
                                field.Name,
                                field.GetValue(bus.PublishedEvents[i]),
                                i,
                                events[i].GetType().Name);
                        }
                    }

                    // Test length
                    Assert.AreEqual(events.Length, bus.PublishedEvents.Count, BuildEventExpectedActual(events));
                }
                finally
                {
                    NcqrsEnvironment.Deconfigure();
                }
            }

            private string BuildEventExpectedActual(IEvent[] events)
            {
                StringBuilder resVal = new StringBuilder("Was expecting:\r\n");
                foreach (var evt in events)
                {
                    resVal.AppendFormat("\r\n{0}", evt.GetType().Name);
                }
                resVal.AppendLine("\r\nBut got:");
                foreach (var evt in bus.PublishedEvents)
                {
                    resVal.AppendFormat("\r\n{0}", evt.GetType().Name);
                }
                return resVal.ToString();
            }

            public void ThenExpect(ErrorCode code)
            {
                try
                {
                    foreach (var cmd in commands)
                    {
                        cmdService.Execute(cmd);
                    }

                    try
                    {
                        cmdService.Execute(when);
                        Assert.Fail("Was expecting a 'BusinessRuleViolatedException' but got nothing");
                    }
                    catch (BusinessRuleViolatedException ex)
                    {
                        Assert.AreEqual(code, ex.ErrorCode);
                    }
                }
                finally
                {
                    NcqrsEnvironment.Deconfigure();
                }
            }

            bool IEnvironmentConfiguration.TryGet<T>(out T result)
            {
                object item;
                bool resVal = container.TryGetValue(typeof(T), out item);
                result = (T)item;
                return resVal;
            }
        }
    }
}
