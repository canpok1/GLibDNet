using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GLibDNet.Update
{
	[CLSCompliantAttribute(true)]
	public interface SceneFactory
	{
		/// <summary>
		/// Sceneを生成します。
		/// 最初に生成されるSceneは0番です。
		/// </summary>
		/// <param name="sceneNo">生成するSceneの番号</param>
		/// <returns>生成したScene</returns>
		Scene create( int sceneNo, SceneParameter param );

		/// <summary>
		/// LoadingSceneを生成します。
		/// </summary>
		/// <param name="nextScene">LoadSceneで準備するScene</param>
		/// <returns>LoadingScene</returns>
		LoadingScene createLoadingScene( Scene nextScene );
	}
}
