using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GLibDNet;
using GLibDNet.Update;

namespace TestWindowForm
{
    public partial class Form1 : Form
    {
		/// <summary>
		/// �Q�[����ʂł��B
		/// </summary>
        public Form1()
        {
            InitializeComponent();

			int width = this.pictureBox1.Size.Width;
			int height = this.pictureBox1.Size.Height;

			// �`��pBitmap
			Bitmap bmp = new Bitmap(width, height);

			this.pictureBox1.Image = bmp;

			GLib engine = GLib.getInstance();

		}

		/// <summary>
		/// �`�悵�܂��B
		/// </summary>
		public void drawMyself()
		{
			Graphics g = Graphics.FromImage(this.pictureBox1.Image);

			int colorNo = new Random().Next(3);
			switch (colorNo)
			{
				case 0:
					g.Clear(Color.Aqua);
					break;
				case 1:
					g.Clear(Color.Red);
					break;
				default:
					g.Clear(Color.White);
					break;
			}

			this.pictureBox1.Refresh();
		}

		/// <summary>
		/// �{�^�����N���b�N�����Ƃ��̃C�x���g�B
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button1_Click(object sender, EventArgs e)
		{
			this.drawMyself();
		}
    }
}
