using System.ComponentModel;

namespace JAO_PI.Core.Controller
{
    class Worker
    {
        public static BackgroundWorker SaveWorker;
        public static BackgroundWorker CompileWorker;

        public Worker()
        {
            if (SaveWorker == null)
            {
                SaveWorker = new BackgroundWorker();
                SaveWorker.WorkerReportsProgress = true;
                SaveWorker.WorkerSupportsCancellation = true;

                //SaveWorker.DoWork += new DoWorkEventHandler(worker_DoWork);
                //SaveWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            }

            if(CompileWorker == null)
            {
                CompileWorker = new BackgroundWorker();
                CompileWorker.WorkerReportsProgress = true;
                CompileWorker.WorkerSupportsCancellation = true;

                //CompileWorker.DoWork += new DoWorkEventHandler(worker_DoWork);
                //CompileWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            }
        }
    }
}
