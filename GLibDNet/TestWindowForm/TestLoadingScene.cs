using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GLibDNet.Update;
using System.Drawing;
using GLibDNet.Util;

namespace TestWindowForm
{
	class TestLoadingScene : LoadingScene
	{

		private Logger logger;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="nextScene"></param>
		public TestLoadingScene(Scene nextScene, SceneParameter param)
			: base(nextScene, param)
		{
			this.logger = LoggerGetter.getInstance().getLogger(
							System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		}


		public override DrawResultEnum drawMyself(Graphics g)
		{
			g.Clear(Color.Black);

			Font ft = new Font("MSGoshic", 20);
			Point pt = new Point(20, 20);
			String st = "now loading";
			g.DrawString(
				st,
				ft,
				Brushes.White,
				pt);

			return DrawResultEnum.ONCE;
		}

		public override bool setup()
		{
			this.logger.info("準備完了");
			return true;
		}

		public override void cleanup()
		{
			this.logger.info("リソース解放");
		}
	}
}
