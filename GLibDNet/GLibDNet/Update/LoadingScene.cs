using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GLibDNet.Update
{
	[CLSCompliantAttribute(true)]
	public abstract class LoadingScene : Scene
	{
		/// <summary>
		/// ロード後のシーン
		/// このシーンをロードします。
		/// </summary>
		private Scene nextScene;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="nextScene"></param>
		public LoadingScene(Scene nextScene, SceneParameter param) : base( param )
		{
			// 引数チェック
			if (nextScene == null)
			{
				throw new ArgumentNullException("nextScene", "nullにできません。");
			}
			if (nextScene is LoadingScene)
			{
				throw new ArgumentException("nextScene", "LoadingSceneを設定することはできません。");
			}

			this.nextScene = nextScene;
		}

		/// <summary>
		/// 更新カテゴリ
		/// </summary>
		/// <returns></returns>
		public sealed override byte getCategory()
		{
			return 0;
		}

		/// <summary>
		/// 更新レベル
		/// </summary>
		/// <returns></returns>
		public override sealed Byte getUpdateLevel()
		{
			return 0;
		}

		/// <summary>
		/// 並列更新
		/// </summary>
		public override void parallelUpdate()
		{
			// 何も処理を行いません。
		}

		/// <summary>
		/// 次のシーンの準備を行います。
		/// 派生クラスでオーバーライドする場合、基底クラスのメソッドを必ず呼び出してください。
		/// </summary>
		/// <returns>シーンの状態</returns>
		public override UpdatedStateEnum syncronousUpdate()
		{
			Boolean result = this.nextScene.setup();

			if (result == true)
			{
				GameLoopGenerator.getInstance().addContents(this.nextScene);
				return UpdatedStateEnum.Remove;
			}
			else
			{
				return UpdatedStateEnum.Continue;
			}
		}
	}
}
