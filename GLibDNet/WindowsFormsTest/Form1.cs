using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GLibDNet;
using GLibDNet.Util;

namespace WindowsFormsTest
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void END_Click( object sender, EventArgs e )
		{
			this.Close();
		}

		private void button1_Click( object sender, EventArgs e )
		{
			GLib engine = GLib.getInstance( new StandardLoggerFactory() );
			GameProperties properties = new GameProperties( 30, 30, new TestSceneFactory(), new Size( 500, 500 ) );
			Form form = engine.getFrame( properties, null );

			this.Hide();
			form.ShowDialog();
			this.Show();

			form.Dispose();
		}
	}
}
