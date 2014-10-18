using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GLibDNet.Sound
{
	/// <summary>
	/// サウンドファイルの再生機能を提供するインターフェースです。
	/// </summary>
	interface SoundEngine
	{
		/// <summary>
		/// サウンドファイルを再生します。
		/// </summary>
		void play();

		/// <summary>
		/// サウンドファイルを再生します。
		/// 最後まで再生したら最初から再生します。
		/// </summary>
		void playLooping();

		/// <summary>
		/// サウンドファイルの再生を停止します。
		/// </summary>
		void stop();

		/// <summary>
		/// サウンドファイルのリソースを解放します。
		/// </summary>
		void cleanup();
	}
}
