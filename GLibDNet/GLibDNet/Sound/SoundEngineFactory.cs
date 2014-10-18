using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GLibDNet.Sound
{
	/// <summary>
	/// SoundEngineを生成するクラスです。
	/// </summary>
	internal class SoundEngineFactory
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		private SoundEngineFactory()
		{
		}

		/// <summary>
		/// サウンドファイルに対応するSoundEngineを生成します。
		/// </summary>
		/// <param name="info">サウンドファイルの情報</param>
		/// <returns>生成したSoundEngine</returns>
		public static SoundEngine create(SoundProperty info)
		{
			// 引数チェック
			if (info == null)
			{
				throw new ArgumentNullException("info", "nullにできません。");
			}

			if (info is SoundPathProperty)
			{
				SoundPathProperty pathInfo = (SoundPathProperty)info;
				return SoundEngineFactory.create(pathInfo);
			}
			if (info is SoundStreamProperty)
			{
				SoundStreamProperty streamInfo = (SoundStreamProperty)info;
				return SoundEngineFactory.create(streamInfo);
			}

			throw new ArgumentException("info", "対応していない型形式です。");
		}

		/// <summary>
		/// サウンドファイルに対応するSoundEngineを生成します。
		/// </summary>
		/// <param name="info">サウンドファイルの情報</param>
		/// <returns>生成したSoundEngine</returns>
		/// <exception cref="ArgumentException">未対応の形式の場合に発生</exception>
		public static SoundEngine create(SoundPathProperty info)
		{
			// 引数チェック
			if (info == null)
			{
				throw new ArgumentNullException("info", "nullにできません。");
			}

			SoundType type = info.getSoundType();
			switch (type)
			{
				case SoundType.WAVE:
					return new WaveEngine(info.getFilePath());
			}

			throw new ArgumentException("info", "対応するSoundEngineがありません。");
		}

		/// <summary>
		/// サウンドファイルに対応するSoundEngineを生成します。
		/// </summary>
		/// <param name="info">サウンドファイルの情報</param>
		/// <returns>生成したSoundEngine</returns>
		/// <exception cref="ArgumentException">未対応の形式の場合に発生</exception>
		public static SoundEngine create(SoundStreamProperty info)
		{
			// 引数チェック
			if (info == null)
			{
				throw new ArgumentNullException("info", "nullにできません。");
			}

			SoundType type = info.getSoundType();
			switch (type)
			{
				case SoundType.WAVE:
					return new WaveEngine(info.getStream());
			}

			throw new ArgumentException("info", "対応するSoundEngineがありません。");
		}

	}
}
