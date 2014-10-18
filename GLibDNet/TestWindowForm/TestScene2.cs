using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GLibDNet.Update;
using System.Drawing;
using GLibDNet.Util;
using GLibDNet.Key;
using GLibDNet.Drawing.Shapes;

namespace TestWindowForm
{
	class TestScene2 : Scene
	{
		/// <summary>
		/// ロガー
		/// </summary>
		Logger logger;

		/// <summary>
		/// 背景色
		/// </summary>
		private Color backColor;

		/// <summary>
		/// 更新回数
		/// </summary>
		private int updateCount;

		/// <summary>
		/// 図形
		/// </summary>
		private Shape shape;

		/// <summary>
		/// セットアップに必要な更新回数
		/// </summary>
		private static int setupCompleteCount = 30;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public TestScene2( SceneParameter param ) : base( param )
		{
			this.logger = LoggerGetter.getInstance().getLogger(
							System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
			this.backColor = Color.White;
			this.updateCount = 0;
			this.shape = new GLibDNet.Drawing.Shapes.Rectangle(40, 40, 50, 50);
		}

		/// <summary>
		/// 更新カテゴリ
		/// </summary>
		/// <returns></returns>
		public override Byte getCategory()
		{
			return 0;
		}

		/// <summary>
		/// 更新レベル
		/// </summary>
		/// <returns></returns>
		public override Byte getUpdateLevel()
		{
			return 0;
		}

		/// <summary>
		/// 並列更新
		/// </summary>
		public override void parallelUpdate()
		{
			this.logger.debug(
				"並列更新");
		}
		
		/// <summary>
		/// 同期更新
		/// </summary>
		/// <returns></returns>
		public override UpdatedStateEnum syncronousUpdate()
		{
			this.logger.debug(
				"同期更新");

			UpdatedStateEnum result = UpdatedStateEnum.Continue;

			if (InputManager.getInstance().isOneTimeModeKeyPressed(InputManager.KeyCodeEnum.ESC) == true)
			{
				result = UpdatedStateEnum.Remove;
			}
			if (InputManager.getInstance().isOneTimeModeKeyPressed(InputManager.KeyCodeEnum.ML) == true)
			{
				SceneSwitcher.swtichScene(2, null);
				result = UpdatedStateEnum.Remove;
			}

			return result;
		}

		/// <summary>
		/// 描画
		/// </summary>
		/// <param name="g"></param>
		public override DrawResultEnum drawMyself(Graphics g)
		{
			this.logger.debug(
				"TestScene描画");

			g.Clear(this.backColor);

			Font ft = new Font("MSGoshic", 20);
			Point pt = new Point(20, 20);
			String st = "scene 2 point " + InputManager.getInstance().getMousePoint();
			g.DrawString(
				st,
				ft,
				Brushes.Black,
				pt);

			shape.drawMyself(g);

			return DrawResultEnum.ONCE;
		}

		/// <summary>
		/// シーンの準備
		/// </summary>
		/// <returns>true:準備完了 false:準備中</returns>
		public override Boolean setup()
		{
			if (this.updateCount < TestScene2.setupCompleteCount)
			{
				this.updateCount++;
				this.logger.info("TestScene2の準備中");
				return false;
			}
			else
			{
				this.logger.info("TestScene2の準備完了");
				return true;
			}
		}

		/// <summary>
		/// シーンのリソース解放
		/// </summary>
		/// <returns>true:解放完了 false:解放中</returns>
		public override void cleanup()
		{
			this.logger.info("TestScene2のリソース解放中");
		}
	}
}
