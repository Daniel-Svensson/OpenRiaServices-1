using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.ExceptionServices;
using System.Security;
using System.Threading;
using System.Windows.Threading;

namespace OpenRiaServices.Silverlight.Testing
{
    /// <summary>
    /// Abstract base class for all Unit Tests in WCF RIA Services.
    /// </summary>
    [DebuggerStepThrough]
    public abstract class UnitTestBase
    {
        // The number of timeouts we allow by default
        protected const int DefaultTimeoutThreshold = 2;

        // The length of time we wait by default before timing out
        protected const int DefaultTimeoutInSeconds = 60;
        protected const int DebuggingTimeoutInSeconds = 600;

        // Delay between conditional evaluations
        private const int DefaultStepInMilliseconds = 100;

        // The number of timeouts
        private static int NumberOfTimeouts;

        [SecuritySafeCritical]
        protected UnitTestBase()
        {
            var syncContext = SynchronizationContext.Current;
            if (!(syncContext is DispatcherSynchronizationContext))
            {
                SynchronizationContext.SetSynchronizationContext(new DispatcherSynchronizationContext());
            }
        }

        #region Assert nested class

        /// <summary>
        /// This nested Assert class is a <see cref="SecuritySafeCritical"/> entry point to
        /// the normal MSTest class.   It is required because the MSTest version is SecurityCritical
        /// and cannot be used from partial trust unit tests
        /// </summary>
        [SecuritySafeCritical]
        public static class Assert
        {
            // --- IsTrue ---
            public static void IsTrue(bool condition)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(condition);
            }
            public static void IsTrue(bool condition, string message)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(condition, message);
            }
            public static void IsTrue(bool condition, string message, object[] parameters)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(condition, message, parameters);
            }

            // --- IsFalse ---
            public static void IsFalse(bool condition, string message)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsFalse(condition, message);
            }
            public static void IsFalse(bool condition)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsFalse(condition);
            }
            public static void IsFalse(bool condition, string message, object[] parameters)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsFalse(condition, message, parameters);
            }

            // --- Inconclusive ---
            public static void Inconclusive()
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.Inconclusive();
            }
            public static void Inconclusive(string message)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.Inconclusive(message);
            }
            public static void Inconclusive(string message, object[] parameters)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.Inconclusive(message, parameters);
            }

            // --- Fail ---
            public static void Fail()
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.Fail();
            }
            public static void Fail(string message)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.Fail(message);
            }
            public static void Fail(string message, object[] parameters)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.Fail(message, parameters);
            }

            // --- IsInstanceOfType ---
            public static void IsInstanceOfType(object instance, Type type)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsInstanceOfType(instance, type);
            }
            public static void IsInstanceOfType(object instance, Type type, string message)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsInstanceOfType(instance, type, message);
            }
            public static void IsInstanceOfType(object instance, Type type, string message, object[] parameters)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsInstanceOfType(instance, type, message, parameters);
            }

            // --- IsNotInstanceOfType ---
            public static void IsNotInstanceOfType(object instance, Type type)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotInstanceOfType(instance, type);
            }
            public static void IsNotInstanceOfType(object instance, Type type, string message)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotInstanceOfType(instance, type, message);
            }
            public static void IsNotInstanceOfType(object instance, Type type, string message, object[] parameters)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotInstanceOfType(instance, type, message, parameters);
            }

            // --- IsNull ---
            public static void IsNull(object instance)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNull(instance, "but was {0}", instance);
            }
            public static void IsNull(object instance, string message)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNull(instance, message);
            }
            public static void IsNull(object instance, string message, object[] parameters)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNull(instance, message, parameters);
            }

            // --- IsNotNull ---
            public static void IsNotNull(object instance)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(instance);
            }
            public static void IsNotNull(object instance, string message)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(instance, message);
            }
            public static void IsNotNull(object instance, string message, object[] parameters)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(instance, message, parameters);
            }

            // --- AreEqual ---
            public static void AreEqual(object expected, object actual)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected, actual);
            }
            public static void AreEqual<T>(T expected, T actual)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual<T>(expected, actual);
            }
            public static void AreEqual<T>(T expected, T actual, string message, object[] parameters)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual<T>(expected, actual, message, parameters);
            }
            public static void AreEqual(double expected, double actual, double delta)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected, actual, delta);
            }
            public static void AreEqual(double expected, double actual, double delta, string message, object[] parameters)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected, actual, delta, message, parameters);
            }
            public static void AreEqual(float expected, float actual, float delta)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected, actual, delta);
            }
            public static void AreEqual(float expected, float actual, float delta, string message, object[] parameters)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected, actual, delta, message, parameters);
            }
            public static void AreEqual(object expected, object actual, string message)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected, actual, message);
            }
            public static void AreEqual(string expected, string actual, bool ignoreCase)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected, actual, ignoreCase);
            }
            public static void AreEqual(string expected, string actual, bool ignoreCase, CultureInfo cultureInfo)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected, actual, ignoreCase, cultureInfo);
            }
            public static void AreEqual(string expected, string actual, bool ignoreCase, CultureInfo cultureInfo, string message, object[] parameters)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected, actual, ignoreCase, cultureInfo, message, parameters);
            }
            public static void AreEqual(string expected, string actual, bool ignoreCase, string message)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected, actual, ignoreCase, message);
            }
            public static void AreEqual<T>(T expected, T actual, string message)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual<T>(expected, actual, message);
            }
            public static void AreEqual(double expected, double actual, double delta, string message)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected, actual, delta, message);
            }
            public static void AreEqual(float expected, float actual, float delta, string message)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected, actual, delta, message);
            }
            public static void AreEqual(object expected, object actual, string message, object[] parameters)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected, actual, message, parameters);
            }

            // --- AreNotEqual ---
            public static void AreNotEqual(object expected, object actual)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreNotEqual(expected, actual);
            }
            public static void AreNotEqual<T>(T expected, T actual)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreNotEqual<T>(expected, actual);
            }
            public static void AreNotEqual<T>(T expected, T actual, string message, object[] parameters)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreNotEqual<T>(expected, actual, message, parameters);
            }
            public static void AreNotEqual(double expected, double actual, double delta)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreNotEqual(expected, actual, delta);
            }
            public static void AreNotEqual(double expected, double actual, double delta, string message, object[] parameters)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreNotEqual(expected, actual, delta, message, parameters);
            }
            public static void AreNotEqual(float expected, float actual, float delta)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreNotEqual(expected, actual, delta);
            }
            public static void AreNotEqual(float expected, float actual, float delta, string message, object[] parameters)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreNotEqual(expected, actual, delta, message, parameters);
            }
            public static void AreNotEqual(object expected, object actual, string message)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreNotEqual(expected, actual, message);
            }
            public static void AreNotEqual(string expected, string actual, bool ignoreCase)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreNotEqual(expected, actual, ignoreCase);
            }
            public static void AreNotEqual(string expected, string actual, bool ignoreCase, CultureInfo cultureInfo)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreNotEqual(expected, actual, ignoreCase, cultureInfo);
            }
            public static void AreNotEqual(string expected, string actual, bool ignoreCase, CultureInfo cultureInfo, string message, object[] parameters)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreNotEqual(expected, actual, ignoreCase, cultureInfo, message, parameters);
            }
            public static void AreNotEqual(string expected, string actual, bool ignoreCase, string message)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreNotEqual(expected, actual, ignoreCase, message);
            }
            public static void AreNotEqual<T>(T expected, T actual, string message)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreNotEqual<T>(expected, actual, message);
            }
            public static void AreNotEqual(double expected, double actual, double delta, string message)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreNotEqual(expected, actual, delta, message);
            }
            public static void AreNotEqual(float expected, float actual, float delta, string message)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreNotEqual(expected, actual, delta, message);
            }
            public static void AreNotEqual(object expected, object actual, string message, object[] parameters)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreNotEqual(expected, actual, message, parameters);
            }

            // --- AreSame ---
            public static void AreSame(object expected, object actual)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreSame(expected, actual);
            }
            public static void AreSame(object expected, object actual, string message)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreSame(expected, actual, message);
            }
            public static void AreSame(object expected, object actual, string message, object[] parameters)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreSame(expected, actual, message, parameters);
            }

            // --- AreNotSame ---
            public static void AreNotSame(object expected, object actual)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreNotSame(expected, actual);
            }
            public static void AreNotSame(object expected, object actual, string message)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreNotSame(expected, actual, message);
            }
            public static void AreNotSame(object expected, object actual, string message, object[] parameters)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreNotSame(expected, actual, message, parameters);
            }

            // --- Equals ---
            public static new void Equals(object expected, object actual)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.Equals(expected, actual);
            }

            // --- ReplaceNullChars ---
            public static string ReplaceNullChars(string value)
            {
                return Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ReplaceNullChars(value);
            }
        }
        #endregion // Assert nested class

        public virtual void Enqueue(Action d)
        {
            if (UnitTestBase.NumberOfTimeouts >= UnitTestBase.DefaultTimeoutThreshold)
            {
                Assert.Inconclusive("The test was not attempted because the number of tests that have timed-out has exceeded the threshold.");
            }

            Action<Action> invoke = InvokeAction;
            Dispatcher.CurrentDispatcher.BeginInvoke(invoke, DispatcherPriority.Background, d);
        }

        [SecuritySafeCritical]
        private static void InvokeAction(Action action)
        {
            var syncContext = SynchronizationContext.Current;
            try
            {
                // All callbacks should default to normal
                SynchronizationContext.SetSynchronizationContext(
                    // TODO: Cachethis (per class/thread?)
                    new DispatcherSynchronizationContext(
                        Dispatcher.CurrentDispatcher, DispatcherPriority.Normal));

                action();
            }
            finally
            {
                SynchronizationContext.SetSynchronizationContext(syncContext);
            }
        }

        public virtual void EnqueueCallback(Action testCallbackDelegate)
        {
            this.Enqueue(testCallbackDelegate);
        }

        public virtual void EnqueueConditional(Func<bool> conditionalDelegate)
        {
            this.EnqueueConditional(conditionalDelegate, string.Empty);
        }

        public virtual void EnqueueConditional(Func<bool> conditionalDelegate, string timeoutMessage)
        {
            this.EnqueueConditional(conditionalDelegate,
                Debugger.IsAttached ? UnitTestBase.DebuggingTimeoutInSeconds : UnitTestBase.DefaultTimeoutInSeconds,
                timeoutMessage);
        }

        public virtual void EnqueueConditional(Func<bool> conditionalDelegate, int timeoutInSeconds)
        {
            this.EnqueueConditional(conditionalDelegate, timeoutInSeconds, string.Empty);
        }

        public void EnqueueConditional(Func<bool> conditionalDelegate, int timeoutInSeconds, string timeoutMessage)
        {
            DateTime endTime = DateTime.Now.AddSeconds(timeoutInSeconds);


            this.Enqueue(
                () =>
                {
                    // Below will run at Render prio

                    if (conditionalDelegate())
                        return;

                    Action action = null;

                    action = () =>
                    {
                        if (!conditionalDelegate())
                        {
                            if (DateTime.Now >= endTime)
                            {
                                UnitTestBase.NumberOfTimeouts++;
                                Assert.Fail(UnitTestBase.ComposeTimeoutMessage(timeoutInSeconds, timeoutMessage));
                            }

                            Dispatcher.CurrentDispatcher.BeginInvoke(action, DispatcherPriority.Render, null);
                            Thread.Sleep(UnitTestBase.DefaultStepInMilliseconds);

                        }
                    };

                    action();
                });
        }

        private static string ComposeTimeoutMessage(int timeoutInSeconds, string timeoutMessage)
        {
            string failureMessage =
                "The test was unable to satisfy the condition within the specified " +
                timeoutInSeconds + " second timeout.";
            if (!string.IsNullOrEmpty(timeoutMessage))
            {
                failureMessage += "\n " + timeoutMessage;
            }

            return failureMessage;
        }

        public virtual void EnqueueDelay(TimeSpan delay)
        {
            this.Enqueue(() => Thread.Sleep((int)delay.TotalMilliseconds));
        }

        public void EnqueueDelay(int milliseconds)
        {
            this.EnqueueDelay(TimeSpan.FromMilliseconds(milliseconds));
        }

        public virtual void EnqueueTestComplete()
        {
            this.ProcessQueue();
        }

        [SecuritySafeCritical]
        private void ProcessQueue()
        {
            var frame = new DispatcherFrame();

            // This will create dispatcher for the current thread if there is none yet
            var dispatcher = Dispatcher.CurrentDispatcher;

            dispatcher.BeginInvoke(
            new Action(() => frame.Continue = false),
            DispatcherPriority.ContextIdle);

            // Enqued actions are processed to

            ExceptionDispatchInfo exception = null;

            // Stop processing on exception
            DispatcherUnhandledExceptionEventHandler unhandledEvent = (object sender, DispatcherUnhandledExceptionEventArgs e) =>
            {
                exception = ExceptionDispatchInfo.Capture(e.Exception);

                e.Handled = true;
                frame.Continue = false;
            };

            // record all qued callbacks
            List<DispatcherOperation> pendingOperations = new List<DispatcherOperation>();
            DispatcherHookEventHandler operationAdded = (sender, args) =>
            {
                pendingOperations.Add(args.Operation);
            };
            DispatcherHookEventHandler operationExecuted = (sender, args) =>
            {
                pendingOperations.Remove(args.Operation);
            };

            try
            {

                dispatcher.UnhandledException += unhandledEvent;
                dispatcher.Hooks.OperationPosted += operationAdded;
                dispatcher.Hooks.OperationCompleted += operationExecuted;

                Dispatcher.PushFrame(frame);

                Dispatcher.Yield(DispatcherPriority.ApplicationIdle).GetAwaiter().GetResult();
                if (exception != null)
                    exception.Throw();
            }
            finally
            {
                dispatcher.UnhandledException -= unhandledEvent;
                dispatcher.Hooks.OperationPosted -= operationAdded;
                dispatcher.Hooks.OperationCompleted -= operationExecuted;

                // Abort any unfinished operations
                foreach (var op in pendingOperations)
                    if (op.Status != DispatcherOperationStatus.Completed)
                    {
                        op.Abort();

                        Assert.IsTrue(exception != null
                            //|| pendingOperations.Count == 0
                            // The DispatcherFrame posts a "no-op" send event upon exit
                            // this should be the only invocation in the queue
                            || pendingOperations.Count == 1 && op.Priority == DispatcherPriority.Send
                            , "Not all callbacks executed, but no exception thrown");
                    }

            }
        }
    }
}
