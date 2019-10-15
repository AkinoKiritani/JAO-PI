using System.ComponentModel;

namespace JAO_PI.Core.Controller
{
    public class Worker
    {
        public static BackgroundWorker SaveWorker           = null;
        public static BackgroundWorker CompileWorker        = null;
        public static BackgroundWorker CloseAllWorker       = null;
        public static BackgroundWorker CloseAllButWorker    = null;

        public Worker()
        {
            if (SaveWorker == null)
            {
                SaveWorker = new BackgroundWorker
                {
                    WorkerReportsProgress = true,
                    WorkerSupportsCancellation = true
                };

                SaveWorker.DoWork += new DoWorkEventHandler(EventsManager.Worker.Save_DoWork);
                SaveWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(EventsManager.Worker.Save_Completed);
            }

            if(CompileWorker == null)
            {
                CompileWorker = new BackgroundWorker
                {
                    WorkerReportsProgress = true,
                    WorkerSupportsCancellation = true
                };

                CompileWorker.DoWork += new DoWorkEventHandler(EventsManager.Worker.Compiler_DoWork);
                CompileWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(EventsManager.Worker.Compiler_Completed);
            }

            if(CloseAllWorker == null)
            {
                CloseAllWorker = new BackgroundWorker
                {
                    WorkerReportsProgress = true,
                    WorkerSupportsCancellation = true
                };

                CloseAllWorker.DoWork += new DoWorkEventHandler(EventsManager.Worker.CloseAll_DoWork);
                CloseAllWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(EventsManager.Worker.CloseAll_Completed);
            }

            if (CloseAllButWorker == null)
            {
                CloseAllButWorker = new BackgroundWorker
                {
                    WorkerReportsProgress = true,
                    WorkerSupportsCancellation = true
                };

                CloseAllButWorker.DoWork += new DoWorkEventHandler(EventsManager.Worker.CloseAllBut_DoWork);
                CloseAllButWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(EventsManager.Worker.CloseAllBut_Completed);
            }
        }
    }
}
