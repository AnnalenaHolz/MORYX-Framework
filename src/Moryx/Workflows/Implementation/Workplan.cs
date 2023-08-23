// Copyright (c) 2023, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Moryx.Workplans
{
    /// <summary>
    /// Default implementation of IWorkplan
    /// </summary>
    [DataContract]
    public class Workplan : IWorkplan, IPersistentObject
    {
        /// <summary>
        /// Create a new workplan instance
        /// </summary>
        public Workplan() : this(new List<IConnector>(), new List<IWorkplanStep>())
        {
        }
        public override bool Equals(object obj)
        {
            if (!(obj is Workplan))
            {
                return false;
            }
            Workplan newWorkplan = (Workplan)obj;
           

            var startConnector = this.Connectors.FirstOrDefault(x => x.Name.Equals("Start")); //FirstOrDefault gibt ein oder kein Element an
            var endConnector = this.Connectors.FirstOrDefault(x => x.Name.Equals("End"));
            var failedConnector = this.Connectors.FirstOrDefault(x => x.Name.Equals("Failed"));
            var nextStep = this.Steps.FirstOrDefault(x => x.Inputs.Any(y =>y.Equals(startConnector)));

            var newStartConnector = newWorkplan.Connectors.FirstOrDefault(x => x.Name.Equals("Start"));
            var newEndConnector = newWorkplan.Connectors.FirstOrDefault(x => x.Name.Equals("End"));
            var newFailedConnector = newWorkplan.Connectors.FirstOrDefault(x => x.Name.Equals("Failed"));
            var newNextStep = newWorkplan.Steps.FirstOrDefault(x => x.Inputs.Any(y => y.Equals(newStartConnector)));

            bool end = false;
            List<IWorkplanStep> stepsToCompare = new List<IWorkplanStep>();
            List<IWorkplanStep> newStepsToCompare = new List<IWorkplanStep>();
            List<IWorkplanStep> comparedSteps = new List<IWorkplanStep>();
            List<IWorkplanStep> newComparedSteps = new List<IWorkplanStep>();


            while (end != true)                                
            {
                CompareSteps(nextStep, newNextStep);
                comparedSteps.Add(nextStep);
                newComparedSteps.Add(newNextStep); //nur eine Liste? Habe ja geprüft, ob sie gleich sind

                for (int i = 0; i < nextStep.Outputs.Length; i++) //nur nextStep, da ich die Länge bereits verglichen habe
                {
                    if (nextStep.Outputs[i].Classification == newNextStep.Outputs[i].Classification)
                    {
                        if (!comparedSteps.Contains(this.Steps.FirstOrDefault(x => x.Inputs.Any(y => y.Equals(nextStep.Outputs[i])))))
                        {                                                
                            if (nextStep.Outputs[i] != endConnector)
                            {
                                if (nextStep.Outputs[i] != failedConnector)
                                {
                                    stepsToCompare.Add(this.Steps.FirstOrDefault(x => x.Inputs.Any(y => y.Equals(nextStep.Outputs[i]))));
                                }                                
                            }
                        }
                        if (!newComparedSteps.Contains(newWorkplan.Steps.FirstOrDefault(x => x.Inputs.Any(y => y.Equals(newNextStep.Outputs[i])))))
                        {
                            if (newNextStep.Outputs[i] != newEndConnector)
                            {
                                if(newNextStep.Outputs[i] != newFailedConnector)
                                {
                                    newStepsToCompare.Add(newWorkplan.Steps.FirstOrDefault(x => x.Inputs.Any(y => y.Equals(newNextStep.Outputs[i]))));
                                }
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }         
               
                stepsToCompare.Remove(nextStep);
                newStepsToCompare.Remove(newNextStep);            
               
                
                if (stepsToCompare.Count != 0 && newStepsToCompare.Count != 0)
                {
                    nextStep = stepsToCompare[0];
                    newNextStep = newStepsToCompare[0];
                }
                else if ((stepsToCompare.Count != 0 && newStepsToCompare.Count == 0) || (stepsToCompare.Count == 0 && newStepsToCompare.Count != 0))
                {
                    return false;
                }
                else
                {
                    end = true;
                }

            }
            return true; //richtig?
        }
        private bool CompareSteps(IWorkplanStep stepNext, IWorkplanStep stepNextNew)
        {
            if (stepNext.GetType() == stepNextNew.GetType()) //compares Steps 
            {
            }
            else
            {
                return false;
            }

            if (stepNext.Outputs.Length == stepNextNew.Outputs.Length) //compares number of Connectors
            {
            }
            else
            {
                return false;
            }           
            return true;
        }


        /// <summary>
        /// Private constructor used for new and restored workplans
        /// </summary>
        private Workplan(List<IConnector> connectors, List<IWorkplanStep> steps)
        {
            _connectors = connectors;
            _steps = steps;
        }

        /// <see cref="IWorkplan"/>
        public long Id { get; set; }

        ///<see cref="IWorkplan"/>
        public string Name { get; set; }

        ///<see cref="IWorkplan"/>
        public int Version { get; set; }

        ///<see cref="IWorkplan"/>
        public WorkplanState State { get; set; }

        /// <summary>
        /// Current biggest id in the workplan
        /// </summary>
        public int MaxElementId { get; set; }

        /// <summary>
        /// Editable list of connectors
        /// </summary>
        [DataMember]
        private List<IConnector> _connectors;

        /// <see cref="IWorkplan"/>
        public IEnumerable<IConnector> Connectors => _connectors;

        /// <summary>
        /// Editable list of steps
        /// </summary>
        [DataMember]
        private List<IWorkplanStep> _steps;

        /// <see cref="IWorkplan"/>
        public IEnumerable<IWorkplanStep> Steps => _steps;

        /// <summary>
        /// Add a range of connectors to the workplan
        /// </summary>
        public void Add(params IWorkplanNode[] nodes)
        {
            foreach (var node in nodes)
            {
                node.Id = ++MaxElementId;
                if (node is IConnector)
                    _connectors.Add((IConnector)node);
                else
                    _steps.Add((IWorkplanStep)node);
            }
        }
        /// <summary>
        /// Removes a node from the workplan
        /// </summary>
        public bool Remove(IWorkplanNode node)
        {
            return node is IConnector ? _connectors.Remove((IConnector)node) : _steps.Remove((IWorkplanStep)node);
        }

        /// <summary>
        /// Restore a workplan with a list of connectors and steps
        /// </summary>
        public static Workplan Restore(List<IConnector> connectors, List<IWorkplanStep> steps)
        {
            return new Workplan(connectors, steps);
        }
    }
}
