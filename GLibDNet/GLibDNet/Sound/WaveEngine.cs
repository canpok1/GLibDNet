using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Media;
using System.IO;

namespace GLibDNet.Sound
{
	/// <summary>
	/// Waveファイルを再生するクラスです。
	/// </summary>
	internal class WaveEngine : SoundEngine
	{
		/// <summary>
		/// Waveファイル用のプレイヤー
		/// </summary>
		private SoundPlayer player;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="filePath">サウンドファイルのパス</param>
		public WaveEngine(String filePath)
		{
			try
			{
				this.player = new SoundPlayer(filePath);
				this.player.Load();
			}
			catch (Exception e)
			{
				throw new ArgumentException(
					"Waveファイルの読み込みに失敗しました。(" + filePath + ")",
					e );
			}
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="filePath">サウンドファイルのストリーム</param>
		public WaveEngine(Stream stream)
		{
			try
			{
				this.player = new SoundPlayer(stream);
				this.player.Load();
			}
			catch (Exception e)
			{
				throw new ArgumentException(
					"Waveファイルの読み込みに失敗しました。(" + stream.ToString() + ")",
					e);
			}
		}

		/// <summary>
		/// Waveファイルを再生します。
		/// </summary>
		public void play()
		{
			this.player.Play();
		}

		/// <summary>
		/// Waveファイルを再生します。
		/// 最後まで再生したら最初から再生します。
		/// </summary>
		public void playLooping()
		{
			this.player.PlayLooping();
		}

		/// <summary>
		/// Waveファイルの再生を停止します。
		/// </summary>
		public void stop()
		{
			this.player.Stop();
		}

		/// <summary>
		/// リソースを解放します。
		/// </summary>
		public void cleanup()
		{
			this.player.Dispose();
		}
	}
}
