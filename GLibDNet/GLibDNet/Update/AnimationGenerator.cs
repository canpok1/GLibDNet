using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;
using System.Diagnostics;
using GLibDNet.Util;
using System.Threading.Tasks;

namespace GLibDNet.Update
{
	/// <summary>
	/// <newpara>アニメーションを生成するクラスです。</newpara>
	/// </summary>
	internal class AnimationGenerator
	{
		/// <summary>
		/// ロック取得時のタイムアウト時間(ms)
		/// </summary>
		private static int TIMEOUT = 5000;

		/// <summary>
		/// インスタンス
		/// </summary>
		private static AnimationGenerator singleton = null;

		/// <summary>
		/// <newpara>ディスプレイのリフレッシュレート</newpara>
		/// </summary>
		private Byte hz;

		/// <summary>
		/// <newpara>アニメーション生成用スレッド</newpara>
		/// </summary>
		private Task animationTask;

		/// <summary>
		/// <newpara>アニメーション終了フラグ</newpara>
		/// </summary>
		private Boolean endFlag;

		/// <summary>
		/// <newpara>描画対象コンテンツのリスト</newpara>
		/// </summary>
		private List<DrawableContents> contentsList;

		/// <summary>
		/// <newpara>排他制御用のロック</newpara>
		/// </summary>
		private ReaderWriterLock rwLock;

		/// <summary>
		/// <newpara>オフスクリーンバッファ</newpara>
		/// </summary>
		private Image offScreenBuff;

		/// <summary>
		/// <newparam>表示領域</newparam>
		/// </summary>
		private DisplayArea displayArea;

		/// <summary>
		/// ログ記録インスタンス
		/// </summary>
		private Logger logger;

		/// <summary>
		/// <newpara>コンストラクタです。</newpara>
		/// </summary>
		private AnimationGenerator()
		{
			this.hz = 30;
			this.animationTask = null;
			this.endFlag = true;
			this.contentsList = new List<DrawableContents>();
			this.rwLock = new ReaderWriterLock();
			this.offScreenBuff = null;
			this.displayArea = null;
			this.logger = LoggerGetter.getInstance().getLogger(
							System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		}

		/// <summary>
		/// <newpara>インスタンスを取得します。</newpara>
		/// </summary>
		/// <returns></returns>
		public static AnimationGenerator getInstance()
		{
			if (AnimationGenerator.singleton == null)
			{
				AnimationGenerator.singleton = new AnimationGenerator();
			}

			return AnimationGenerator.singleton;
		}

		/// <summary>
		/// アニメーションを開始します。
		/// </summary>
		/// <param name="hz">画面のリフレッシュレート</param>
		/// <param name="size">画面サイズ</param>
		/// <param name="area">表示領域</param>
		public void start( Byte hz, Size size, DisplayArea area )
		{
			// 引数チェック
			if (size == null)
			{
				throw new ArgumentNullException("size", "画面サイズがnullです。");
			}
			if (area == null)
			{
				throw new ArgumentNullException("area", "表示領域がnullです。");
			}

			// アニメーション起動中かをチェック
			if (this.isRunning() == true)
			{
				throw new ApplicationException("アニメーションはすでに起動しています。");
			}

			this.hz = hz;
			this.endFlag = false;
			this.displayArea = area;
			this.offScreenBuff = new Bitmap(size.Width, size.Height);
			this.animationTask = Task.Factory.StartNew(
				() =>
				{
					this.loop();
				} );
		}

		/// <summary>
		/// アニメーションを停止しスレッドが終了するまで待機します。
		/// </summary>
		public void stop()
		{
			if (this.isRunning() == true)
			{
				this.logger.info(
				"アニメーションスレッドを停止します。" );

				this.endFlag = true;

				this.logger.info(
					"アニメーションスレッド停止まで待機" );
				this.animationTask.Wait();
			}

			this.logger.info(
				"アニメーションスレッド停止を確認" );
		}

		/// <summary>
		/// <newpara>アニメーション中かを判定します。</newpara>
		/// </summary>
		public Boolean isRunning()
		{
			if( animationTask == null )
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		/// <summary>
		/// <newpara>アニメーション生成を行うループです。</newpara>
		/// </summary>
		private void loop()
		{
			try
			{
				this.logger.info(
					"アニメーションスレッド開始");

				long waitTime = 1000 / this.hz;

				this.logger.debug(
					"リフレッシュレート[" + this.hz.ToString() + "]"
					+ " 待機時間[" + waitTime.ToString() + "(ms)]");

				Stopwatch stopwatch = new Stopwatch();

				long sleepTime = 0;

				Graphics g = Graphics.FromImage(this.offScreenBuff);

				List<DrawableContents> removeList = new List<DrawableContents>();

				while (this.endFlag == false)
				{
					stopwatch.Reset();
					stopwatch.Start();

					try
					{
						this.logger.debug(
							"AnimationGenerator更新");

						this.logger.debug(
							"アニメーションロック取得" );
						this.rwLock.AcquireWriterLock( AnimationGenerator.TIMEOUT );

						// 描画対象を描画
						foreach (DrawableContents content in this.contentsList)
						{
							if (content.drawMyself(g) == DrawResultEnum.ONCE)
							{
								removeList.Add(content);
							}
						}

						// 一度だけ描画するものを描画対象から外す
						foreach (DrawableContents content in removeList)
						{
							this.contentsList.Remove(content);
						}

						removeList.Clear();
					}
					finally
					{
						this.logger.debug(
							"アニメーションロック解放" );
						this.rwLock.ReleaseWriterLock();
					}

					// 表示領域を更新
					this.displayArea.updateArea(new Bitmap(this.offScreenBuff));

					stopwatch.Stop();

					// スリープ時間を計算
					sleepTime = waitTime - stopwatch.ElapsedMilliseconds;

					if (sleepTime > 0)
					{
						this.logger.debug(
							sleepTime.ToString() + "(ms)スリープ");
						Thread.Sleep((int)sleepTime);
					}
					else
					{
						this.logger.info(
							"スリープしない(" + sleepTime.ToString() + ")");
					}
				}

				this.offScreenBuff = null;
			}
			catch(Exception e)
			{
				this.logger.fatal( e );
			}
			finally
			{
				this.contentsList.Clear();
				this.animationTask = null;

				GLib.getInstance().stop();
				this.logger.info(
					"アニメーションスレッド停止");
			}
		}

		/// <summary>
		/// <newpara>描画対象コンテンツのリストを設定します。</newpara>
		/// </summary>
		public void setList( List<DrawableContents> list )
		{
			// 要素チェック
			if (list == null)
			{
				throw new ArgumentNullException(
					"list",
					"nullにすることはできません。");
			}

			try
			{
				this.rwLock.AcquireWriterLock(AnimationGenerator.TIMEOUT);
				this.logger.debug(
					"アニメーションロック取得");

				foreach (DrawableContents content in list)
				{
					if (this.contentsList.Contains(content) == false)
					{
						this.contentsList.Add(content);
					}
				}
			}
			finally
			{
				this.rwLock.ReleaseWriterLock();
				this.logger.debug(
					"アニメーションロック解放");
			}
		}

	}
}
