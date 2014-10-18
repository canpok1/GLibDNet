using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GLibDNet.Util;

namespace GLibDNet.Sound
{
	/// <summary>
	/// サウンドの再生等を行うクラスです。
	/// このクラスはシングルトンです。
	/// </summary>
	[CLSCompliantAttribute(true)]
	public class SoundManager
	{
		/// <summary>
		/// インスタンス
		/// </summary>
		private static SoundManager singleton = new SoundManager();

		/// <summary>
		/// SoundEngineのリスト
		/// </summary>
		private SoundEngineList engines;

		/// <summary>
		/// 読み込み済みのサウンドファイル
		/// </summary>
		private List<SoundProperty> loadedSound;

		/// <summary>
		/// ロガー
		/// </summary>
		private Logger logger;

		/// <summary>
		/// コンストラクタです。
		/// </summary>
		private SoundManager()
		{
			this.logger = LoggerGetter.getInstance().getLogger(
							System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
			this.engines = new SoundEngineList();
			this.loadedSound = new List<SoundProperty>();
		}

		/// <summary>
		/// インスタンスを取得します。
		/// </summary>
		/// <returns>インスタンス</returns>
		public static SoundManager getInstance()
		{
			return SoundManager.singleton;
		}

		/// <summary>
		/// サウンドを再生します。
		/// </summary>
		/// <param name="info">対象のサウンドファイル情報</param>
		public void play(SoundProperty info)
		{
			this.logger.debug("サウンド再生(" + info.getKey() + ")");

			SoundEngine engine = this.engines.get(info);
			engine.play();
		}

		/// <summary>
		/// サウンドを繰り返し再生します。
		/// </summary>
		/// <param name="info">対象のサウンドファイル情報</param>
		public void playLooping(SoundProperty info)
		{
			this.logger.debug("サウンド繰り返し再生(" + info.getKey() + ")");

			SoundEngine engine = this.engines.get(info);
			engine.playLooping();
		}

		/// <summary>
		/// サウンドを停止します。
		/// </summary>
		/// <param name="info">対象のサウンドファイル情報</param>
		public void stop(SoundProperty info)
		{
			this.logger.debug("サウンド停止(" + info.getKey() + ")");

			SoundEngine engine = this.engines.get(info);
			engine.stop();
		}

		/// <summary>
		/// サウンドファイルを読み込みます。
		/// </summary>
		/// <param name="info">対象のサウンドファイル情報</param>
		public void load(SoundProperty info)
		{
			this.logger.debug("サウンドファイル読み込み(" + info.getKey() + ")");

			this.engines.load(info);
			this.loadedSound.Add(info);
		}

		/// <summary>
		/// サウンドファイルを読み込みます。
		/// </summary>
		/// <param name="infoList">対象のサウンドファイル情報</param>
		public void load(List<SoundProperty> infoList)
		{
			this.engines.load(infoList);
			this.loadedSound.AddRange(infoList);
		}

		/// <summary>
		/// 読み込んだサウンドファイルを解放します。
		/// </summary>
		public void clear()
		{
			this.logger.debug("全サウンドファイル解放");

			this.engines.removeAll();
			this.loadedSound.Clear();
		}
	}
}
