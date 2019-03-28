using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SQLServerDatabaseDiff
{
    public class BaseAnalyzer
    {
        private MainForm m_Form;
        private Thread m_Thread;
        private volatile String m_Status;
        private volatile int m_Progress_Max;
        private volatile int m_Progress_Current;
        private volatile bool m_Finished;
        private DateTime m_StartTime;
        private DateTime m_CurrentTime;
        private TimeSpan m_ElapsedTime;
        private DateTime m_RemainTime;

        #region Properties

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public String Status
        {
            get { return m_Status; }
            set { m_Status = value; }
        }

        /// <summary>
        /// Gets or sets the progress_ max.
        /// </summary>
        /// <value>The progress_ max.</value>
        public int Progress_Max
        {
            get { return m_Progress_Max; }
            set { m_Progress_Max = value; }
        }

        /// <summary>
        /// Gets or sets the progress_ current.
        /// </summary>
        /// <value>The progress_ current.</value>
        public int Progress_Current
        {
            get { return m_Progress_Current; }
            set { m_Progress_Current = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="BaseAnalyzer"/> is finished.
        /// </summary>
        /// <value><c>true</c> if finished; otherwise, <c>false</c>.</value>
        public bool Finished
        {
            get { return m_Finished; }
            set { m_Finished = value; }
        }
        /// <summary>
        /// Gets or sets the start time.
        /// </summary>
        /// <value>The start time.</value>
        public DateTime StartTime
        {
            get { return m_StartTime; }
            set { m_StartTime = value; }
        }

        /// <summary>
        /// Gets or sets the elapsed time.
        /// </summary>
        /// <value>The elapsed time.</value>
        public TimeSpan ElapsedTime
        {
            get { return m_ElapsedTime; }
            set { m_ElapsedTime = value; }
        }

        /// <summary>
        /// Gets or sets the remain time.
        /// </summary>
        /// <value>The remain time.</value>
        public DateTime RemainTime
        {
            get { return m_RemainTime; }
            set { m_RemainTime = value; }
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseAnalyzer"/> class.
        /// </summary>
        /// <param name="Target">The target.</param>
        public BaseAnalyzer(MainForm Target)
        {
            m_Form = Target;
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run()
        {
            m_StartTime = DateTime.Now;
            m_Finished = false;
            m_Thread = new Thread(new ThreadStart(DoWork));
            m_Thread.Start();
        }

        /// <summary>
        /// Aborts this instance.
        /// </summary>
        public void Abort()
        {
            if (m_Thread != null)
            {
                m_Thread.Abort();
                m_Finished = true;
            }
        }

        /// <summary>
        /// Does the work.
        /// </summary>
        private void DoWork()
        {
            lock (m_Form.Bases)
            {
                if (m_Form.Bases[MainForm.Base1].fetchData(this, m_Form.Configuration) == false) return;
                if (m_Form.Bases[MainForm.Base2].fetchData(this, m_Form.Configuration) == false) return;
                m_Form.Bases[MainForm.Base1].qualifyVersus(this, m_Form.Bases[MainForm.Base2], Direction.Out);
                m_Form.Bases[MainForm.Base2].qualifyVersus(this, m_Form.Bases[MainForm.Base1], Direction.In);
            }

            m_Finished = true;
        }

        /// <summary>
        /// Does the qualify work.
        /// </summary>
        private void DoQualifyWork()
        {
            lock (m_Form.Bases)
            {
                if (m_Form.Bases[MainForm.Base1].fetchData(this, m_Form.Configuration) == false) return;
                if (m_Form.Bases[MainForm.Base2].fetchData(this, m_Form.Configuration) == false) return;
                m_Form.Bases[MainForm.Base1].qualifyVersus(this, m_Form.Bases[MainForm.Base2], Direction.Out);
                m_Form.Bases[MainForm.Base2].qualifyVersus(this, m_Form.Bases[MainForm.Base1], Direction.In);
            }

            m_Finished = true;
        }

        /// <summary>
        /// Computes the timing.
        /// </summary>
        public void ComputeTiming()
        {
            m_CurrentTime = DateTime.Now;
            m_ElapsedTime = (m_CurrentTime - m_StartTime);
            if (m_ElapsedTime.TotalSeconds > 1 && m_Progress_Current > 0 && m_Progress_Max > 0 && m_Progress_Current <= m_Progress_Max)
            {
                double seconds = m_ElapsedTime.TotalSeconds;
                seconds = (seconds / m_Progress_Current) * (m_Progress_Max - m_Progress_Current);
                m_RemainTime = new DateTime((long)(seconds * 10000000));
            }
            else
            {
                m_RemainTime = new DateTime(0);
            }
        }
    }
}
