using System.ComponentModel;

namespace JAO_PI.Core.Controller
{
    class Worker
    {
        public static BackgroundWorker SaveWorker = null;
        public static BackgroundWorker CompileWorker = null;

        public Worker()
        {
            if (SaveWorker == null)
            {
                SaveWorker = new BackgroundWorker();
                SaveWorker.WorkerReportsProgress = true;
                SaveWorker.WorkerSupportsCancellation = true;
                SaveWorker.DoWork += new DoWorkEventHandler(EventsManager.Worker.Save_DoWork);
                SaveWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(EventsManager.Worker.Save_Completed);
            }

            if(CompileWorker == null)
            {
                CompileWorker = new BackgroundWorker();
                CompileWorker.WorkerReportsProgress = true;
                CompileWorker.WorkerSupportsCancellation = true;
                
                CompileWorker.DoWork += new DoWorkEventHandler(EventsManager.Worker.Compiler_DoWork);
                CompileWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(EventsManager.Worker.Compiler_Completed);
            }
        }
    }
}
