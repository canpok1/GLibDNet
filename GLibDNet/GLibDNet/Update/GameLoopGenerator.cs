using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GLibDNet.Util;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GLibDNet.Update
{
	/// <summary>
	/// ゲームループを生成するクラスです
	/// </summary>
	internal class GameLoopGenerator
	{
		/// <summary>
		/// ロック取得時のタイムアウト時間(ms)
		/// </summary>
		private static int TIMEOUT = 5000;

		/// <summary>
		/// インスタンス
		/// </summary>
		private static GameLoopGenerator singleton;

		/// <summary>
		/// 更新用コンテンツ
		/// </summary>
		private Dictionary<Byte, List<UpdatableContents>> updateList;

		/// <summary>
		/// 更新頻度
		/// </summary>
		private Byte fps;

		/// <summary>
		/// ゲームループ用スレッド
		/// </summary>
		private Task gameLoopTask;

		/// <summary>
		/// 更新対象コンテンツ用ロック
		/// </summary>
		private ReaderWriterLock updateRwLock;

		/// <summary>
		/// ゲームループ停止フラグ
		/// </summary>
		private Boolean endFlag;

		/// <summary>
		/// 追加コンテンツ用ロック
		/// </summary>
		private ReaderWriterLock addRwLock;

		/// <summary>
		/// 更新対象コンテンツの一時保存リスト
		/// </summary>
		private List<UpdatableContents> addedContentsList;

		/// <summary>
		/// ログ記録用インスタンス
		/// </summary>
		private Logger logger;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		private GameLoopGenerator()
		{
			this.updateList = new Dictionary<byte, List<UpdatableContents>>();
			this.addedContentsList = new List<UpdatableContents>();
			this.fps = 1;
			this.gameLoopTask = null;
			this.updateRwLock = new ReaderWriterLock();
			this.addRwLock = new ReaderWriterLock();
			this.logger = LoggerGetter.getInstance().getLogger(
							System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
			this.endFlag = true;
		}

		/// <summary>
		/// インスタンスを取得します。
		/// </summary>
		/// <returns></returns>
		public static GameLoopGenerator getInstance()
		{
			if (GameLoopGenerator.singleton == null)
			{
				GameLoopGenerator.singleton = new GameLoopGenerator();
			}
			return GameLoopGenerator.singleton;
		}

		/// <summary>
		/// ゲームループを開始します。
		/// </summary>
		public void start(Byte fps)
		{
			// 引数チェック
			if (fps <= 0)
			{
				throw new ArgumentException(
					"fps",
					"1以上の値でなければなりません。" );
			}

			// ゲームループ動作中かをチェック
			if (this.isRunning() == true)
			{
				throw new ApplicationException("ゲームループはすでに起動しています。");
			}

			this.logger.debug(
				"ゲームループスレッド生成");

			this.endFlag = false;
			this.fps = fps;
			this.gameLoopTask = Task.Factory.StartNew(
				() =>
				{
					this.loop();
				});
		}


		/// <summary>
		/// ゲームループを停止し、スレッドが終了するまで待機します。
		/// </summary>
		public void stop()
		{
			if( this.isRunning() == true )
			{
				this.logger.info(
					"ゲームループスレッドを停止します。");

				this.endFlag = true;

				this.logger.info(
						"ゲームループスレッド停止まで待機");

				this.gameLoopTask.Wait();
			}

			this.logger.info(
				"ゲームループスレッド停止を確認");
		}

		/// <summary>
		/// ゲームループ起動中か判定
		/// </summary>
		/// <returns>true:起動中 false:起動していない</returns>
		public Boolean isRunning()
		{
			if (this.gameLoopTask == null)
			{
				return false;
			}
			else
			{
				return true;
			}
		}


		/// <summary>
		/// ゲーム用のループです。
		/// </summary>
		private void loop()
		{
			try
			{
				this.logger.info(
					"ゲームループスレッド開始");

				long waitTime = 1000 / this.fps;

				this.logger.debug(
					"FPS : " + this.fps.ToString());

				Stopwatch stopwatch = new Stopwatch();

				long sleepTime = 0;

				List<UpdatableContents> contentsList = new List<UpdatableContents>();
				List<ParallelUpdateProcess> processList = new List<ParallelUpdateProcess>();
				ParallelUpdateExecuter pUpdateExecuter = new ParallelUpdateExecuter();
				SyncronousUpdateExecuter sUpdateExecuter = new SyncronousUpdateExecuter();

				while ( this.endFlag == false )
				{
					stopwatch.Reset();
					stopwatch.Start();

					this.logger.debug(
							"更新リスト用書き込みロック取得");
					this.updateRwLock.AcquireWriterLock( GameLoopGenerator.TIMEOUT );
					try
					{
						// 追加コンテンツリストの要素を更新コンテンツリストに追加
						this.logger.debug(
								"追加リスト用書き込みロック取得");
						this.addRwLock.AcquireWriterLock( GameLoopGenerator.TIMEOUT );
						try
						{
							foreach (UpdatableContents content in this.addedContentsList)
							{
								this.addContentsToUpdateList(content);
							}
							this.addedContentsList.Clear();
						}
						finally
						{
							this.logger.debug(
									"追加リスト用書き込みロック解放");
							this.addRwLock.ReleaseWriterLock();
						}

						// 更新対象を取得
						foreach (Byte key in this.updateList.Keys)
						{
							List<UpdatableContents> targets = this.getList(key);
							ParallelUpdateProcess process = new ParallelUpdateProcess(targets);
							processList.Add(process);

							foreach (UpdatableContents content in targets)
							{
								contentsList.Add(content);
							}

							this.logger.debug(
								"更新対象数[カテゴリ" + key.ToString() + "] : " 
								+ targets.Count.ToString() );
						}

						this.logger.debug(
							"更新対象数[全カテゴリ] : "
							+ contentsList.Count.ToString());

						if (contentsList.Count <= 0)
						{
							this.endFlag = true;
						}

						// コンテンツをカテゴリ毎に更新
						pUpdateExecuter.execute(processList);

						// 例外が発生していないかをチェック
						foreach (ParallelUpdateProcess process in processList)
						{
							Exception e = process.getThrowedException();
							if (e != null)
							{
								throw e;
							}
						}

						// 全コンテンツを順番に更新
						List<UpdatableContents> removeList 
							= sUpdateExecuter.execute(contentsList);

						foreach (UpdatableContents content in removeList)
						{
							this.removeContent(content);
							content.cleanup();
						}

						processList.Clear();
						contentsList.Clear();

					}
					finally
					{
						this.logger.debug(
								"更新リスト用書き込みロック解放" );
						this.updateRwLock.ReleaseWriterLock();
					}

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

			}
			catch(Exception e)
			{
				this.logger.fatal( e );
			}
			finally
			{
				this.updateList.Clear();
				this.gameLoopTask = null;
				GLib.getInstance().stop();
				this.logger.info(
					"ゲームループスレッド停止");
			}
		}


		/// <summary>
		/// 更新リストの全コンテンツを順番に更新します。
		/// </summary>
		/// <returns>描画対象のコンテンツ</returns>
		private List<DrawableContents> updateAllContents()
		{
			Dictionary<Byte, List<UpdatableContents>>.KeyCollection keys
				= this.updateList.Keys;

			List<UpdatableContents> removeList = new List<UpdatableContents>();
			List<DrawableContents> drawList = new List<DrawableContents>();

			// 全コンテンツを順番に更新
			foreach (Byte key in keys)
			{
				foreach (UpdatableContents content in this.updateList[key])
				{
					// 順番に更新
					UpdatedStateEnum state = content.syncronousUpdate();

					if (state == UpdatedStateEnum.Remove)
					{
						// 削除対象リストに追加
						removeList.Add(content);
					}

					if ((content is DrawableContents) == true)
					{
						// 描画対象リストに追加
						DrawableContents drawable = (DrawableContents)content;
						drawList.Add(drawable);
					}
				}

				foreach (UpdatableContents content in removeList)
				{
					// 更新対象リストから除去
					this.removeContent(content);
				}

				removeList.Clear();
			}

			return drawList;
		}

		/// <summary>
		/// 更新対象コンテンツを一時保存リストに追加します。
		/// </summary>
		/// <param name="content">コンテンツ</param>
		public void addContents(UpdatableContents content)
		{
			// 引数チェック
			if (content == null)
			{
				throw new ArgumentNullException(
					"content",
					"nullにできません。");
			}

			this.addRwLock.AcquireWriterLock(GameLoopGenerator.TIMEOUT);
			try
			{
				this.addedContentsList.Add(content);
			}
			finally
			{
				this.addRwLock.ReleaseWriterLock();
			}

			this.logger.debug(
				"追加リストに追加(" + content.ToString() + ")");
		}

		/// <summary>
		/// コンテンツを更新対象リストに追加します。
		/// </summary>
		/// <param name="content">追加するコンテンツ</param>
		private void addContentsToUpdateList(UpdatableContents content)
		{
			// 引数チェック
			if (content == null)
			{
				throw new ArgumentNullException(
					"content",
					"nullにできません。");
			}

			Byte category = content.getCategory();

			List<UpdatableContents> list = this.getList(category);
			list.Add(content);
			list.Sort();
		}

		/// <summary>
		/// 指定カテゴリ番号以下の更新対象コンテンツを全てクリアします。
		/// カテゴリ番号3を指定した場合、4以上のカテゴリのコンテンツは全てクリアされます。
		/// カテゴリ番号0を指定した場合、全てのカテゴリのコンテンツがクリアされます。
		/// </summary>
		/// <param name="level">クリアするコンテンツのカテゴリ</param>
		private void clearContents(Byte category)
		{
			Dictionary<Byte, List<UpdatableContents>>.KeyCollection keys
				= this.updateList.Keys;

			foreach (Byte key in keys)
			{
				if (key >= category)
				{
					this.updateList[key].Clear();
					this.logger.debug(
								"カテゴリ[" + key.ToString() + "]のコンテンツを削除");
				}
			}
		}

		/// <summary>
		/// 更新対象から特定のコンテンツを削除します。
		/// </summary>
		/// <param name="content">削除するコンテンツ</param>
		private void removeContent(UpdatableContents content)
		{
			// 引数チェック
			if (content == null)
			{
				throw new ArgumentNullException("content", "nullにできません。");
			}

			List<UpdatableContents> list = this.updateList[content.getCategory()];
			list.Remove(content);
		}

		/// <summary>
		/// 指定カテゴリのコンテンツリストを取得します。
		/// コンテンツリストが存在しない場合は、リストを作成します。
		/// </summary>
		/// <param name="category">カテゴリ</param>
		/// <returns>コンテンツリスト</returns>
		private List<UpdatableContents> getList(Byte category)
		{
			List<UpdatableContents> list;

			if (this.updateList.ContainsKey(category) == true)
			{
				list = this.updateList[category];
			}
			else
			{
				list = new List<UpdatableContents>();
				this.updateList.Add(category, list);
			}

			return list;
		}


		/// <summary>
		/// コンテンツの数を取得します。
		/// </summary>
		/// <returns>全コンテンツ数</returns>
		private int getContentsCount()
		{
			int count = 0;

			Dictionary<Byte, List<UpdatableContents>>.KeyCollection keys
				= this.updateList.Keys;

			foreach (Byte key in keys)
			{
				count += this.updateList[key].Count;
			}

			return count;
		}


		/// <summary>
		/// 同期更新を行う内部クラスです。
		/// </summary>
		private class SyncronousUpdateExecuter
		{
			/// <summary>
			/// 更新対象から外すコンテンツのリスト
			/// </summary>
			private List<UpdatableContents> removeContents;

			/// <summary>
			/// 描画対象コンテンツのリスト
			/// </summary>
			private List<DrawableContents> drawableContents;

			/// <summary>
			/// ログ記録用インスタンス
			/// </summary>
			private Logger logger;

			/// <summary>
			/// コンストラクタです。
			/// </summary>
			public SyncronousUpdateExecuter()
			{
				this.logger = LoggerGetter.getInstance().getLogger(
								System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
				this.removeContents = new List<UpdatableContents>();
				this.drawableContents = new List<DrawableContents>();
			}

			/// <summary>
			/// 同期更新を行います。
			/// 更新対象がDrawableContentsであれば描画対象に加えます。
			/// </summary>
			/// <returns>更新対象から外すコンテンツのリスト</returns>
			public List<UpdatableContents> execute(List<UpdatableContents> contents)
			{
				// 引数チェック
				if (contents == null)
				{
					throw new ArgumentNullException("contents", "nullにできません。");
				}

				this.removeContents.Clear();

				foreach (UpdatableContents content in contents)
				{
					UpdatedStateEnum state = content.syncronousUpdate();
					if (state == UpdatedStateEnum.Remove)
					{
						this.removeContents.Add(content);
					}
					if (content is DrawableContents)
					{
						this.drawableContents.Add((DrawableContents)content);
					}
				}

				if (this.drawableContents.Count > 0)
				{
					AnimationGenerator.getInstance().setList(this.drawableContents);
					this.drawableContents.Clear();
				}

				return this.removeContents;
			}
		}

		/// <summary>
		/// 並列更新を行う内部クラスです。
		/// </summary>
		private class ParallelUpdateExecuter
		{
			/// <summary>
			/// 並列更新を行うタスクのリスト
			/// </summary>
			private List<Task> taskList;

			/// <summary>
			/// ログ記録用インスタンス
			/// </summary>
			private Logger logger;

			/// <summary>
			/// コンストラクタです。
			/// </summary>
			public ParallelUpdateExecuter()
			{
				this.taskList = new List<Task>();
				this.logger = LoggerGetter.getInstance().getLogger(
								System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
			}

			/// <summary>
			/// 並列更新を行います。
			/// </summary>
			public void execute(List<ParallelUpdateProcess> processes)
			{
				// 引数チェック
				if( processes == null )
				{
					throw new ArgumentNullException("processes", "nullにできません。");
				}
				if (processes.Count == 0)
				{
					return;
				}

				// 各プロセスを起動
				foreach (ParallelUpdateProcess process in processes)
				{
					Task task = Task.Factory.StartNew(
						() =>
						{
							process.update();
						});
					this.taskList.Add(task);

				}

				// 各プロセスの終了を待機
				foreach (Task task in this.taskList)
				{
					task.Wait();
				}

				this.taskList.Clear();
			}
		}


		/// <summary>
		/// 並列更新の内容を保持する内部クラスです。
		/// </summary>
		private class ParallelUpdateProcess
		{
			/// <summary>
			/// 更新対象のリスト
			/// </summary>
			private List<UpdatableContents> list;

			/// <summary>
			/// 更新結果
			/// </summary>
			private Exception throwedException;

			/// <summary>
			/// ログ記録用インスタンス
			/// </summary>
			private Logger logger;

			/// <summary>
			/// コンストラクタ
			/// </summary>
			/// <param name="list">更新対象リスト</param>
			public ParallelUpdateProcess(List<UpdatableContents> list)
			{
				// 引数チェック
				if (list == null)
				{
					throw new ArgumentNullException("list", "nullにすることはできません。");
				}

				this.logger = LoggerGetter.getInstance().getLogger(
								System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
				this.list = list;
				this.throwedException = null;
			}

			/// <summary>
			/// 更新します。
			/// </summary>
			public void update()
			{
				try
				{
					this.throwedException = null;
					foreach (UpdatableContents content in this.list)
					{
						content.parallelUpdate();
					}
				}
				catch (Exception e)
				{
					this.logger.error(
						"並列更新中に例外発生");
					this.throwedException = e;
				}
			}

			/// <summary>
			/// 更新中に発生した例外を取得します。
			/// 発生していない場合はnullになります。
			/// </summary>
			/// <returns>例外</returns>
			public Exception getThrowedException()
			{
				return this.throwedException;
			}

		}
	}
}