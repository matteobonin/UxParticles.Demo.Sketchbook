namespace UxParticles.Demo.Libs.Diagnostics
{
    using System;
    using System.Diagnostics;
    using System.Windows.Threading;

    public static class DispatcherDebugExtensions
    {
        public static void Log(this Dispatcher dispatcher, string logMessage, Action<long> action = null )
        {
            Debug.WriteLine($"[START] {logMessage}");

            var sw = new Stopwatch();
            sw.Start();

            dispatcher.BeginInvoke(
                DispatcherPriority.Loaded, 
                new Action(
                    () =>
                        {
                            sw.Stop();
                            Debug.WriteLine($"[END] {logMessage} elapsed in {sw.ElapsedMilliseconds} ms");
                            if (action != null)
                            {
                                try
                                {
                                    action(sw.ElapsedMilliseconds);
                                }
                                catch (Exception ex)
                                {
                                    Debug.WriteLine($"[EXCEPTION] {logMessage} {ex.Message} - {ex}");
                                }
                            }
                        }));
        }
    }
}