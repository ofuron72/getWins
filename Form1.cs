using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace getWins
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public delegate bool EnumWindowsProc(IntPtr hWnd, int lParam);

        // Импортируем функции из User32.dll
        [DllImport("user32.dll")]
        public static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, int lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        public static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        protected bool EnumWindowsCallback(IntPtr hWnd, int lParam)
        {
           
            int size = GetWindowTextLength(hWnd);

          
            if (size > 0 && IsWindowVisible(hWnd))
            {
                StringBuilder stringBuilder = new StringBuilder(size + 1);
                GetWindowText(hWnd, stringBuilder, stringBuilder.Capacity);

                
                listBox1.Items.Add($"HWND: {hWnd}, Title: {stringBuilder}\n");
            }
            return true; 
        }


        private void button1_Click(object sender, EventArgs e)
        {
            EnumWindows(new EnumWindowsProc(EnumWindowsCallback), 0);
        }
    }
}
