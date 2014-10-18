using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GLibDNet.Update;
using System.Windows.Forms;
using GLibDNet.Util;
using System.Threading;
using GLibDNet.Sound;

[assembly : CLSCompliant(true)]
namespace GLibDNet
{
	/// <summary>
	/// 
	/// </summary>
	[CLSCompliantAttribute(true)]
	public class GLib
    {
        /// <summary>
        /// クラスのインスタンスです。
        /// </summary>
		private static GLib singleton = null;

		/// <summary>
		/// ゲームのパラメータです。
		/// </summary>
		private GameProperties properties;

		/// <summary>
		/// ゲーム画面を描画するフレームです。
		/// </summary>
		private GameFrame frame;

		/// <summary>
		/// シャットダウン条件監視スレッドです。
		/// </summary>
		private Thread shutdownThread;

		/// <summary>
		/// 表示領域の表示状態です。
		/// true:表示 false:非表示
		/// </summary>
		private Boolean displayAreaActivation;

		/// <summary>
		/// ロガーです。
		/// </summary>
		private Logger logger;

        /// <summary>
        /// <newpara>インスタンスを生成します。</newpara>
        /// </summary>
		private GLib( LoggerFactory loggerFactory )
		{
			LoggerGetter lg = LoggerGetter.getInstance();
			if (loggerFactory != null)
			{
				lg.setFactory(loggerFactory);
			}
			this.logger = lg.getLogger(
							System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

			this.properties = null;
			this.frame = null;
			this.shutdownThread = null;
			this.displayAreaActivation = false;
		}

        /// <summary>
        /// <newpara>インスタンスを取得します。</newpara>
        /// </summary>
		/// <param name="loggerFactory">LoggerFactory</param>
        /// <returns>インスタンス</returns>
		public static GLib getInstance( LoggerFactory loggerFactory )
		{
			if (GLib.singleton == null)
			{
				GLib.singleton = new GLib(loggerFactory);
			}

			return GLib.singleton;
		}


		/// <summary>
		/// インスタンスを取得します。
		/// </summary>
		/// <returns>インスタンス</returns>
		public static GLib getInstance()
		{
			return GLib.singleton;
		}

		/// <summary>
		/// ゲームを停止します。
		/// </summary>
		public void stop()
		{
			if (this.shutdownThread == null)
			{
				this.shutdownThread = new Thread(new ThreadStart(this.shutdownProcess));
				this.shutdownThread.Name = "ShutdownThread";

				this.logger.debug("シャットダウンスレッド生成");

				this.shutdownThread.Start();
			}
			else
			{
				this.logger.debug(
					"シャットダウンの処理は起動中です。");
			}
		}


		/// <summary>
		/// ゲームの停止処理です。
		/// </summary>
		private void shutdownProcess()
		{
			this.logger.info(
				"停止処理開始");

			GameLoopGenerator.getInstance().stop();
			AnimationGenerator.getInstance().stop();
			SoundManager.getInstance().clear();

			if (this.displayAreaActivation == true)
			{
				// フレームが開いていたら閉じる
				if (this.frame != null)
				{
					this.frame.BeginInvoke(
						(MethodInvoker)delegate
						{
							this.frame.Close();
							this.frame.Dispose();
						}
					);
				}
				this.displayAreaActivation = false;
			}

			this.shutdownThread = null;
			this.logger.info(
				"停止処理終了");
		}

		/// <summary>
		/// ゲーム画面描画用フレームを取得します。
		/// このメソッドを呼ぶことでアニメーションの生成が開始されます。
		/// </summary>
		/// <returns>フレーム</returns>
		public Form getFrame( GameProperties properties, SceneParameter param )
		{
			if( properties == null )
			{
				throw new ArgumentNullException( "properties", "パラメータをnullにはできません。" );
			}

			if( this.displayAreaActivation == true )
			{
				this.logger.warn(
					"すでにフレームは生成されています。" );
				return this.frame;
			}

			this.properties = properties;

			this.frame = new GameFrame( this.properties.getFrameSize() );
			AnimationGenerator.getInstance().start(
				this.properties.getHz(),
				this.properties.getFrameSize(),
				this.frame
				);

			SceneSwitcher.swtichScene( 0, param );

			return this.frame;
		}


		/// <summary>
		/// ゲーム設定を取得します。
		/// </summary>
		/// <returns>ゲーム設定</returns>
		public GameProperties getProperties()
		{
			return this.properties;
		}


		/// <summary>
		/// DisplayAreaの表示状態を更新します。
		/// </summary>
		/// <param name="activation">表示状態</param>
		internal void setDisplayAreaActivation(Boolean activation)
		{
			this.displayAreaActivation = activation;
		}
    }
}
