using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GLibDNet.Update;
using GLibDNet.Util;

namespace TestWindowForm
{
	class TestSceneFactory : SceneFactory
	{
		private Logger logger;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public TestSceneFactory()
		{
			this.logger = LoggerGetter.getInstance().getLogger(
							System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		}

		/// <summary>
		/// Sceneを生成します。
		/// </summary>
		/// <param name="sceneNo"></param>
		/// <returns></returns>
		public Scene create(int sceneNo, SceneParameter param )
		{
			this.logger.debug(
				"TestSceneを生成");

			switch (sceneNo)
			{
				case 0:
					return new TestScene1( param );
				case 1:
					return new TestScene2( param );
				case 2:
					return new TestScene3( param );
			}

			throw new ArgumentException("sceneNo", "対応するシーンがありません。");
		}

		/// <summary>
		/// LoadingSceneを生成します。
		/// </summary>
		/// <param name="nextScene"></param>
		/// <returns></returns>
		public LoadingScene createLoadingScene(Scene nextScene)
		{
			try
			{
				return new TestLoadingScene(nextScene, null);
			}
			catch (Exception e)
			{
				throw new ArgumentException("LoadingScene生成に失敗", e);
			}
		}
	}
}
