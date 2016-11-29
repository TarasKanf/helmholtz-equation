using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelmholtzEquation.Process
{
    public class ProcessHandler
    {
        private Process Process { get; set; }

        public List<State> States { get; set; }

        public ProcessHandler()
        {
            States = new List<State>();
            Process = new Process();
        }

        public void StartProcess()
        {
            bool stepExecuted = false;
            do
            {
                stepExecuted = Process.ExecuteStep();
                if (stepExecuted)
                {
                    States.Add(
                        Process.GetState());
                }
            }
            while (stepExecuted);
        }

        public void GoToState(State state)
        {
            Process.SetState(state);
        }
    }
}
