using Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace com.amtec.action
{
    public class transExcel
    {
        public Application xlsApp { get; set; }

        public Workbook xlsBook { get; set; }

        public Worksheet xlsSheet { get; set; }

        public Workbook xlsBook2 { get; set; }

        public Worksheet xlsSheet2 { get; set; }

        public transExcel()
        {
            this.xlsApp = new Application();
        }

        public void closeExcel()
        {
            xlsBook.Close(false, Type.Missing, Type.Missing);
            xlsBook2.Close(false, Type.Missing, Type.Missing);
            xlsApp.Quit();
            KillSpecialExcel(xlsApp);

            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlsApp);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlsBook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlsSheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlsBook2);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlsSheet2);

            xlsSheet = null;
            xlsBook = null;
            xlsApp = null;
            xlsSheet2 = null;
            xlsBook2 = null;
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);
        private static void KillSpecialExcel(Excel.Application m_objExcel)
        {
            if (m_objExcel != null)
            {
                int lpdwProcessId;
                GetWindowThreadProcessId(new IntPtr(m_objExcel.Hwnd), out lpdwProcessId);
                System.Diagnostics.Process.GetProcessById(lpdwProcessId).Kill();
            }
        }
    }
}
