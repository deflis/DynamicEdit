using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace DynamicEdit
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            ScriptEngine pse = Python.CreateEngine();
            ScriptScope scope = pse.CreateScope();
            scope.SetVariable("MainForm", this);
            
            ScriptSource source = pse.CreateScriptSourceFromFile(@"sample.py");
            source.Execute(scope);

            var keyEvent = scope.GetVariable<Func<char, Action>>("KeyEvent");
            Observable.FromEvent<KeyPressEventArgs>(this, "KeyPress")
                .Select(e => new { func = keyEvent(e.EventArgs.KeyChar), handled = e.EventArgs.Handled })
                .Where(e => e.func != null)
                .Subscribe(f => f.func());
            var exit = scope.GetVariable<Action>("Exit");
            Observable.FromEvent(e => ExitToolStripMenuItem.Click += e, e => ExitToolStripMenuItem.Click -= e)
                .Where(e => exit != null)
                .Subscribe(e => exit());
            

        }

        string Text { get; set; }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

        }
            
    }
}
