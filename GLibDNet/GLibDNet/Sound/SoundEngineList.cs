using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GLibDNet.Sound
{
	/// <summary>
	/// SoundEngineを保持するクラスです。
	/// </summary>
	internal class SoundEngineList
	{
		/// <summary>
		/// 保持するSoundEngine
		/// </summary>
		private Dictionary<String, SoundEngine> engines;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public SoundEngineList()
		{
			this.engines = new Dictionary<string, SoundEngine>();
		}

		/// <summary>
		/// サウンドファイルに対応するSoundEngineを取得します。
		/// 保持しているものの中にない場合は、新規作成します。
		/// その場合、保持リストには追加されません。
		/// </summary>
		/// <param name="info">サウンドファイル情報</param>
		/// <returns>対応するSoundEngine</returns>
		public SoundEngine get(SoundProperty info)
		{
			// 引数チェック
			if (info == null)
			{
				throw new ArgumentNullException("info", "nllにできません。");
			}

			SoundEngine engine;
			String key = info.getKey();

			if( this.engines.ContainsKey(key) == true )
			{
				engine = this.engines[key];
			}
			else
			{
				engine = SoundEngineFactory.create(info);
			}

			return engine;
		}

		/// <summary>
		/// 保持している全てのSoundEngineのcleanupメソッドを実行し、
		/// 全ての参照を削除します。
		/// </summary>
		public void removeAll()
		{
			foreach (SoundEngine engine in this.engines.Values)
			{
				engine.stop();
				engine.cleanup();
			}
			this.engines.Clear();
		}

		/// <summary>
		/// 対象のSoundEngineのcleanupメソッドを実行し、
		/// 参照を削除します。
		/// </summary>
		/// <param name="info"></param>
		public void remove(SoundProperty info)
		{
			// 引数チェック
			if (info == null)
			{
				throw new ArgumentNullException("info", "nullにできません。");
			}

			String key = info.getKey();

			if (this.engines.ContainsKey(key) == true)
			{
				SoundEngine engine = this.engines[key];
				engine.cleanup();
				this.engines.Remove(key);
			}
		}

		/// <summary>
		/// 対象の全てのSoundEngineのcleanupメソッドを実行し、
		/// 参照を削除します。
		/// </summary>
		/// <param name="infoList"></param>
		public void remove(List<SoundProperty> infoList)
		{
			// 引数チェック
			if (infoList == null)
			{
				throw new ArgumentNullException("infoList", "nullにできません。");
			}

			foreach (SoundProperty info in infoList)
			{
				this.remove(info);
			}
		}

		/// <summary>
		/// 対象のサウンドファイルを読み込みます。
		/// </summary>
		/// <param name="info">サウンドファイル情報</param>
		public void load(SoundProperty info)
		{
			// 引数チェック
			if (info == null)
			{
				throw new ArgumentNullException("info", "nullにできません。");
			}

			String key = info.getKey();

			if (this.engines.ContainsKey(key) == true)
			{
				throw new ArgumentException("info", "すでに読み込み済みです。");
			}

			SoundEngine engine = SoundEngineFactory.create(info);
			this.engines.Add(key, engine);
		}

		/// <summary>
		/// 対象のサウンドファイルを読み込みます。
		/// </summary>
		/// <param name="infoList">サウンドファイル情報</param>
		public void load(List<SoundProperty> infoList)
		{
			// 引数チェック
			if (infoList == null)
			{
				throw new ArgumentNullException("infoList", "nullにできません。");
			}

			foreach (SoundProperty info in infoList)
			{
				this.load(info);
			}
		}
	}
}
