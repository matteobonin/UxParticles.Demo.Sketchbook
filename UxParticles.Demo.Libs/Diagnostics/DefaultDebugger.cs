namespace UxParticles.Demo.Libs.Diagnostics
{
    using System;
    using System.Collections.Concurrent;
    using System.Diagnostics;
    using System.Threading.Tasks;

    public class DefaultDebugger
    {
        private static readonly BlockingCollection<string> MessagesBlockingCollection = new BlockingCollection<string>();

        private static Action<string> logger;

        private DefaultDebugger()
        {
            Task.Run(() => ConsumeCollection());
        }

        public static void Reset(Action<string> actionLogger)
        {
            logger = actionLogger;
        }

        public static void WriteLine(string args)
        {
            MessagesBlockingCollection.Add(args);
        }

        private static void ConsumeCollection()
        {
            while (!MessagesBlockingCollection.IsCompleted)
            {
                foreach (var message in MessagesBlockingCollection.GetConsumingEnumerable())
                {
                    WriteMessage(message);
                }
            }
        }

        private static void WriteMessage(string args)
        {
            if (logger == null)
            {
                Debug.WriteLine(args);
                return;
            }

            logger(args);
        }
    }
}