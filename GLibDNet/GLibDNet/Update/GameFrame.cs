using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using GLibDNet.Util;
using GLibDNet.Key;

namespace GLibDNet.Update
{
	/// <summary>
	/// ゲーム画面が表示されるフレームです。
	/// </summary>
	internal class GameFrame : Form, DisplayArea
	{
		/// <summary>
		/// フレーム内の表示領域
		/// </summary>
		private PictureBox pictureBox;

		/// <summary>
		/// ログ記録インスタンス
		/// </summary>
		private Logger logger;

		/// <summary>
		/// フレームを生成します。
		/// </summary>
		/// <param name="size">表示領域のサイズ</param>
		public GameFrame(
			Size size)
		{
			// 引数チェック
			if (size == null)
			{
				throw new ArgumentNullException("size", "nullにはできません。");
			}
			if ( size.Width <= 0 || size.Height <= 0 )
			{
				throw new ArgumentOutOfRangeException(
					"size",
					"表示領域のサイズが不正[" + size.ToString() + "]");
			}

			this.logger = LoggerGetter.getInstance().getLogger(
							System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

			this.logger.debug(
				"GameFrame生成開始");

			this.logger.debug(
				"表示領域サイズ[" + size.ToString() + "]");

			this.Name = "GameFrame";
			this.ClientSize = new Size(size.Width, size.Height);

			this.pictureBox = new PictureBox();
			this.pictureBox.Location = new System.Drawing.Point(0, 0);
			this.pictureBox.Name = "DisplayArea";
			this.pictureBox.Size = size;
			this.pictureBox.TabIndex = 0;
			this.pictureBox.TabStop = false;

			this.pictureBox.Click += new System.EventHandler(this.GameFrame_Click);
			this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GameFrame_MouseDown);
			this.pictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GameFrame_MouseUp);
			this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GameFrame_MouseMove);

			this.Controls.Add(this.pictureBox);

			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GameFrame_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GameFrame_FormClosed);
			this.Load += new System.EventHandler(this.GameFrame_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GameFrame_KeyDown);
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GameFrame_KeyPress);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GameFrame_KeyUp);

			this.logger.debug(
				"GameFrame生成完了");
		}

		/// <summary>
		/// フレームを更新します。
		/// </summary>
		public void updateArea(Image image)
		{
			if (this.InvokeRequired == true)
			{
				// 別スレッドから呼び出された
				this.BeginInvoke(
					(MethodInvoker)delegate {
						this.pictureBox.Image = image;
						this.pictureBox.Refresh();
					}
				);
			}
			else
			{
				// 同じスレッドから呼び出された
				this.pictureBox.Image = image;
				this.pictureBox.Refresh();
			}
		}

		// このメソッドは未使用です
		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// GameFrame
			// 
			this.ClientSize = new System.Drawing.Size(284, 262);
			this.Name = "GameFrame";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GameFrame_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GameFrame_FormClosed);
			this.Load += new System.EventHandler(this.GameFrame_Load);
			this.Click += new System.EventHandler(this.GameFrame_Click);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GameFrame_KeyDown);
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GameFrame_KeyPress);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GameFrame_KeyUp);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GameFrame_MouseDown);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GameFrame_MouseMove);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GameFrame_MouseUp);
			this.ResumeLayout(false);

		}


		/// <summary>
		/// フレームが初めて表示された時の処理です。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GameFrame_Load(object sender, EventArgs e)
		{
			Byte fps = GLib.getInstance().getProperties().getFPS();
			GameLoopGenerator.getInstance().start(fps);
			GLib.getInstance().setDisplayAreaActivation(true);
		}

		/// <summary>
		/// フレームが閉じられる前の処理です。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GameFrame_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.logger.info(
				"GameFrameが閉じられます");

		}


		/// <summary>
		/// キーが押された時の処理です。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GameFrame_KeyDown(object sender, KeyEventArgs e)
		{
			this.logger.debug(
				"KeyDownイベント発生[" + e.KeyData + "]");

			InputManager.KeyCodeEnum keyCode = this.convert(e.KeyData);
			InputManager.getInstance().pressed(keyCode);
		}


		/// <summary>
		/// キーが押して離された時の処理です。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GameFrame_KeyPress(object sender, KeyPressEventArgs e)
		{
			this.logger.debug(
				"KeyPressイベント発生");
		}


		/// <summary>
		/// キーが離された時の処理です。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GameFrame_KeyUp(object sender, KeyEventArgs e)
		{
			this.logger.debug(
				"KeyUpイベント発生[" + e.KeyData + "]");
			InputManager.KeyCodeEnum keyCode = this.convert(e.KeyData);
			InputManager.getInstance().released(keyCode);
		}


		/// <summary>
		/// 押されたボタンの情報をInputManager用に変換します。
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		private InputManager.KeyCodeEnum convert(Keys key)
		{
			InputManager.KeyCodeEnum result = InputManager.KeyCodeEnum.UNUSED;

			switch (key)
			{
				case Keys.Left:
					result = InputManager.KeyCodeEnum.LEFT;
					break;
				case Keys.Right:
					result = InputManager.KeyCodeEnum.RIGHT;
					break;
				case Keys.Up:
					result = InputManager.KeyCodeEnum.UP;
					break;
				case Keys.Down:
					result = InputManager.KeyCodeEnum.DOWN;
					break;
				case Keys.LButton:
					result = InputManager.KeyCodeEnum.ML;
					break;
				case Keys.RButton:
					result = InputManager.KeyCodeEnum.MR;
					break;
				case Keys.Escape:
					result = InputManager.KeyCodeEnum.ESC;
					break;
				case Keys.Space:
					result = InputManager.KeyCodeEnum.SPACE;
					break;
				case Keys.Enter:
					result = InputManager.KeyCodeEnum.ENTER;
					break;
			}

			return result;

		}


		/// <summary>
		/// 押されたボタンの情報をInputManager用に変換します。
		/// </summary>
		/// <param name="button"></param>
		/// <returns></returns>
		private InputManager.KeyCodeEnum convert(MouseButtons button)
		{
			InputManager.KeyCodeEnum result = InputManager.KeyCodeEnum.UNUSED;

			switch (button)
			{
				case MouseButtons.Left:
					result = InputManager.KeyCodeEnum.ML;
					break;

				case MouseButtons.Right:
					result = InputManager.KeyCodeEnum.MR;
					break;
			}

			return result;
		}


		/// <summary>
		/// フレームが閉じられた後の処理です。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GameFrame_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.logger.debug(
				"GameFrameが閉じられました。");
			GLib.getInstance().setDisplayAreaActivation(false);
			GLib.getInstance().stop();
		}

		/// <summary>
		/// クリックされた時の処理です。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GameFrame_Click(object sender, EventArgs e)
		{
			this.logger.debug(
				"Clickイベント発生");
		}

		/// <summary>
		/// マウスボタンが押された時の処理です。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GameFrame_MouseDown(object sender, MouseEventArgs e)
		{
			this.logger.debug(
				"MouseDownイベント発生");
			InputManager.getInstance().pressed(this.convert(e.Button));
		}

		/// <summary>
		/// マウスボタンが離された時の処理です。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GameFrame_MouseUp(object sender, MouseEventArgs e)
		{
			this.logger.debug(
				"MouseUpイベント発生");
			InputManager.getInstance().released(this.convert(e.Button));
		}

		/// <summary>
		/// マウスカーソルが移動したときの処理です。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GameFrame_MouseMove(object sender, MouseEventArgs e)
		{
			this.logger.debug(
				"MouseMoveイベント発生");
			InputManager.getInstance().mouseMoved(e.Location);
		}
	}
}
