using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GLibDNet.Update;
using GLibDNet.Util;

namespace GLibDNet
{
	/// <summary>
	/// ゲームの起動に必要な設定
	/// </summary>
	[CLSCompliantAttribute(true)]
	public class GameProperties
	{
		/// <summary>
		/// ゲームの更新頻度
		/// </summary>
		private Byte fps = 30;

		/// <summary>
		/// 画面のリフレッシュレート
		/// </summary>
		private Byte hz = 60;

		/// <summary>
		/// SceneFactory
		/// </summary>
		private SceneFactory sceneFactory;

		/// <summary>
		/// ゲーム画面のサイズ
		/// </summary>
		private Size frameSize;

		/// <summary>
		/// プロパティを生成します。
		/// </summar>
		public GameProperties(
				Byte fps,
				Byte hz,
				SceneFactory sf,
				Size size)
		{
			// 引数チェック
			if (fps <= 0)
			{
				throw new ArgumentOutOfRangeException("fps", "0以下にはできません。");
			}
			if (hz <= 0)
			{
				throw new ArgumentOutOfRangeException("hz", "0以下にはできません。");
			}
			if (sf == null)
			{
				throw new ArgumentNullException("sf", "nullにできません。");
			}
			if (size == null)
			{
				throw new ArgumentNullException("size", "画面サイズがnullです。");
			}

			this.fps = fps;
			this.hz = hz;
			this.sceneFactory = sf;
			this.frameSize = new Size(size.Width, size.Height);
		}

		/// <summary>
		/// ゲームの更新頻度を取得します。
		/// </summary>
		/// <returns>ゲームの更新頻度</returns>
		public Byte getFPS()
		{
			return this.fps;
		}

		/// <summary>
		/// 画面のリフレッシュレートを取得します。
		/// </summary>
		/// <returns>画面のリフレッシュレート</returns>
		public Byte getHz()
		{
			return this.hz;
		}

		/// <summary>
		/// SceneFactoryを取得します。
		/// </summary>
		/// <returns>SceneFactory</returns>
		public SceneFactory getSceneFactory()
		{
			return this.sceneFactory;
		}

		/// <summary>
		/// ゲーム画面のサイズを取得します。
		/// </summary>
		/// <returns></returns>
		public Size getFrameSize()
		{
			return this.frameSize;
		}
	}
}
