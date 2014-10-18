using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GLibDNet.Update
{
	[CLSCompliantAttribute(true)]
	public abstract class Scene : UpdatableContents, DrawableContents
	{
		/// <summary>
		/// パラメータ
		/// </summary>
		private SceneParameter param;

		/// <summary>
		/// シーンを生成
		/// </summary>
		/// <param name="param">パラメータ</param>
		public Scene( SceneParameter param )
		{
			this.param = param;
		}

		/// <summary>
		/// シーンの内容を準備。
		/// 画像の読み込みなどの処理を記述します。
		/// </summary>
		/// <returns>true:準備完了 false:準備中</returns>
		public abstract Boolean setup();

		/// <summary>
		/// シーンの内容を描画
		/// </summary>
		/// <param name="g"></param>
		public abstract DrawResultEnum drawMyself(Graphics g);

		/// <summary>
		/// シーンのパラメータを取得
		/// </summary>
		/// <returns>パラメータ</returns>
		public SceneParameter getParam()
		{
			return this.param;
		}
	}
}
