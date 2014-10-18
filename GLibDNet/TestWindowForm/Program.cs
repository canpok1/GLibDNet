using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using GLibDNet;
using System.Drawing;
using GLibDNet.Util;
using GLibDNet.Config;

namespace TestWindowForm
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
			System.Threading.Thread.CurrentThread.Name = "Main";

			GLib engine = GLib.getInstance(
										new StandardLoggerFactory() );

			ConfigUtility.load(null);

			Byte updateRate = ConfigUtility.getByte("UPDATE_RATE");
			Byte refleshRate = ConfigUtility.getByte("REFRESH_RATE");
			Size size = new Size(
								ConfigUtility.getInteger("W_WIDTH"),
								ConfigUtility.getInteger("W_HEIGHT"));

			GameProperties properties
						= new GameProperties(
										updateRate,				// ゲームの更新頻度
										refleshRate,			// リフレッシュレート
										new TestSceneFactory(),	// SceneFactory
										size);					// 画面サイズ

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Form form = engine.getFrame( properties, null );
			Application.Run( form );
        }
    }
}
