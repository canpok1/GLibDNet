using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GLibDNet.Update
{
	/// <summary>
	/// シーンの切り替えを行うクラスです。
	/// </summary>
	public class SceneSwitcher
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		private SceneSwitcher()
		{
		}

		/// <summary>
		/// 次のシーンを生成します。
		/// </summary>
		/// <param name="nextSceneNo">次のシーン番号</param>
		public static void swtichScene( int nextSceneNo, SceneParameter param )
		{
			try
			{
				SceneFactory f = GLib.getInstance().getProperties().getSceneFactory();
				Scene nextScene = f.create( nextSceneNo, param );
				LoadingScene loading = f.createLoadingScene( nextScene );

				GameLoopGenerator.getInstance().addContents( loading );
			}
			catch( Exception e )
			{
				throw new ArgumentException( "シーン生成に失敗", e );
			}
		}
	}
}
