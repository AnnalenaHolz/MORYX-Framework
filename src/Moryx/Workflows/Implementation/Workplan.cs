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
            Workplan wNew = (Workplan)obj;

            //this.Steps.OrderBy(x => x.Name).ToList(); //der alte Workplan
            //a.Steps.OrderBy(x => x.Name).ToList(); //der neue Workplan

            var connectorStart = this.Connectors.FirstOrDefault(x => x.Name.Equals("Start")); //FirstOrDefault gibt ein oder kein Element an
            var connectorEnd = this.Connectors.FirstOrDefault(x => x.Name.Equals("End"));
            var connectorFailed = this.Connectors.FirstOrDefault(x => x.Name.Equals("Failed"));
            var stepNext = this.Steps.FirstOrDefault(x => x.Inputs.Any(y =>y.Equals(connectorStart)));

            var connectorStartNew = wNew.Connectors.FirstOrDefault(x => x.Name.Equals("Start"));
            var connectorEndNew = wNew.Connectors.FirstOrDefault(x => x.Name.Equals("End"));
            var connectorFailedNew = wNew.Connectors.FirstOrDefault(x => x.Name.Equals("Failed"));
            var stepNextNew = wNew.Steps.FirstOrDefault(x => x.Inputs.Any(y => y.Equals(connectorStartNew)));

            bool end = false;
            List<IWorkplanStep> steps = new List<IWorkplanStep>();
            List<IWorkplanStep> stepsNew = new List<IWorkplanStep>();
            List<IWorkplanStep> comparedSteps = new List<IWorkplanStep>();
            List<IWorkplanStep> comparedStepsNew = new List<IWorkplanStep>();


            while (end != true) //läuft mind. einmal durch                                
            {
                CompareSteps(stepNext, stepNextNew);
                comparedSteps.Add(stepNext);
                comparedStepsNew.Add(stepNextNew); //nur eine Liste? Habe ja geprüft, ob sie gleich sind

                for (int i = 0; i < stepNext.Outputs.Length; i++) //nur stepNext, da ich die Länge bereits verglichen habe
                {
                    if (stepNext.Outputs[i].Classification == stepNextNew.Outputs[i].Classification)
                    {
                        if (!comparedSteps.Contains(this.Steps.FirstOrDefault(x => x.Inputs.Any(y => y.Equals(stepNext.Outputs[i])))))
                        {                                                
                            if (stepNext.Outputs[i] != connectorEnd)
                            {
                                if (stepNext.Outputs[i] != connectorFailed)
                                {
                                    steps.Add(this.Steps.FirstOrDefault(x => x.Inputs.Any(y => y.Equals(stepNext.Outputs[i]))));
                                }                                
                            }
                        }
                        if (!comparedStepsNew.Contains(wNew.Steps.FirstOrDefault(x => x.Inputs.Any(y => y.Equals(stepNextNew.Outputs[i])))))
                        {
                            if (stepNextNew.Outputs[i] != connectorEndNew)
                            {
                                if(stepNextNew.Outputs[i] != connectorFailedNew)
                                {
                                    stepsNew.Add(wNew.Steps.FirstOrDefault(x => x.Inputs.Any(y => y.Equals(stepNextNew.Outputs[i]))));
                                }
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }         
               
                steps.Remove(stepNext); //über foreach oder egal
                stepsNew.Remove(stepNextNew);

                //if ((c != connectorEnd || c != connectorFailed) && (cNew != connectorEndNew || cNew != connectorFailed))
                //{
                //    stepNext = this.Steps.FirstOrDefault(x => x.Inputs.Equals(c)); //-> dann brauche ich t nicht mehr
                //    stepNextNew = wNew.Steps.FirstOrDefault(x => x.Inputs.Equals(cNew));
                //}
               
                
                if (steps.Count !=0 && stepsNew.Count !=0)
                {
                    stepNext = steps[0];
                    stepNextNew = stepsNew[0];
                }
                else if ((steps.Count != 0 && stepsNew.Count == 0) || (steps.Count == 0 && stepsNew.Count != 0))
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
        private bool CompareSteps(IWorkplanStep stepNext, IWorkplanStep stepNextNew) //???wNew
        {
            if (stepNext.GetType() == stepNextNew.GetType()) //Schritte vergleichen 
            {
            }
            else
            {
                return false;
            }

            if (stepNext.Outputs.Length == stepNextNew.Outputs.Length) //Anzahl der Connectoren vergleichen
            {
            }
            else
            {
                return false;
            }

            ////doch unnötig, da Anzahl überprüft wird und danach der nächste Step
            //List<IWorkplanStep> currentlyOutputs = new List<IWorkplanStep>(); //wird bei jedem aufrufen eine neue Liste erstelle? Muss ich am ende list.Clear(); machen?
            //List<IWorkplanStep> currentlyOutputsNew = new List<IWorkplanStep>();
            //foreach (var connector in stepNext.Outputs)
            //{             
            //    currentlyOutputs.Add(this.Steps.FirstOrDefault(x => x.Inputs.Equals(connector))); //lokale Liste aller Steps, die durch den Connecctor mit dem aktuellen Step verbunden sond
                
            //}
            //foreach (var connectorNew in stepNextNew.Outputs)
            //{
            //    currentlyOutputsNew.Add(wNew.Steps.FirstOrDefault(x => x.Inputs.Equals(connectorNew))); //wie auf wNew zugreifen können?
                
            //}

            //if (currentlyOutputs.SequenceEqual(currentlyOutputsNew)) //Outputs (verbundene Steps) vergleichen (z.B. bei einer Schleife, da diese schon bei comparedSteps sind, wenn zu kontrollierende Steps eingetragen werden)
            //{
            //}
            //else
            //{
            //    return false;
            //}
            ////currentlyOutputs.Clear(); currentlyOutputsNew.Clear();
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
